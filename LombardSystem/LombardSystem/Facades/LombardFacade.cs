using LombardSystem.Database;
using LombardSystem.Interfaces;
using LombardSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;


namespace LombardSystem.Facades
{
    public class LombardFacade : IUserDB, IEmployeeDB, IAdminDB
    {
        private readonly DatabaseContext _context;

        public LombardFacade()
        {
            _context = new DatabaseContext();
        }

        // IUserDB реализация
        public User Authenticate(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        // IEmployeeDB реализация
        public void AddClient(string fullName, string contactInfo, string passportData)
        {
            var client = new Client
            {
                FullName = fullName,
                ContactInfo = contactInfo,
                PassportData = passportData
            };

            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public void AddItem(string description, decimal estimatedValue)
        {
            var item = new Item
            {
                Description = description,
                EstimatedValue = estimatedValue,
                ReceiptDate = DateTime.UtcNow,
            };

            _context.Items.Add(item);
            _context.SaveChanges();
        }

        public void CreateContract(int clientId, int itemId, DateTime signDate, DateTime expiryDate, decimal loanAmount)
        {
            try
            {
                // Конвертируем даты в UTC
                var utcSignDate = signDate.Kind == DateTimeKind.Unspecified
                    ? DateTime.SpecifyKind(signDate, DateTimeKind.Utc)
                    : signDate.ToUniversalTime();

                var utcExpiryDate = expiryDate.Kind == DateTimeKind.Unspecified
                    ? DateTime.SpecifyKind(expiryDate, DateTimeKind.Utc)
                    : expiryDate.ToUniversalTime();

                // Проверка валидности дат
                if (utcExpiryDate <= utcSignDate)
                {
                    throw new ArgumentException("Дата окончания договора должна быть позже даты подписания");
                }

                var contract = new Contract
                {
                    ClientId = clientId,
                    ItemId = itemId,
                    SignDate = utcSignDate,
                    ExpiryDate = utcExpiryDate,
                    LoanAmount = loanAmount
                };

                _context.Contracts.Add(contract);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                // Специфичная обработка ошибок PostgreSQL
                if (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "22008")
                {
                    throw new ArgumentException("Некорректный формат даты. Используйте UTC даты");
                }
                throw;
            }
        }

        public void SellProduct(int productId)
        {
            using (var context = new DatabaseContext())
            {
                var product = context.Products
                    .Include(p => p.Item)
                    .ThenInclude(i => i.Contract)
                    .FirstOrDefault(p => p.Id == productId);

                if (product == null) return;

                // Удаление в транзакции
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (product.Item?.Contract != null)
                        {
                            context.Contracts.Remove(product.Item.Contract);
                        }

                        if (product.Item != null)
                        {
                            context.Items.Remove(product.Item);
                        }

                        context.Products.Remove(product);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<Client> GetClients() => _context.Clients.ToList();
        public List<Item> GetItems() => _context.Items.ToList();
        public List<Contract> GetContracts() => _context.Contracts.Include(c => c.Client).Include(c => c.Item).ToList();
        public List<Product> GetProducts() => _context.Products.Include(p => p.Item).ToList();

        // IAdminDB реализация
        public void UpdateProductPrice(int productId, decimal newPrice)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                product.SalePrice = newPrice;
                _context.SaveChanges();
            }
        }

        // Метод для обработки истечения сроков договоров
        public void ProcessExpiredContracts()
        {
            // Получаем текущую дату в том же формате, что и в базе данных
            var currentDate = DateTime.UtcNow;

            // Получаем просроченные договоры с включением связанных данных
            var expiredContracts = _context.Contracts
                .Include(c => c.Item)  // Исправлено: было "Ttem", должно быть "Item"
                .ThenInclude(i => i.Product) // Включаем связанный продукт
                .Where(c => c.ExpiryDate < currentDate && c.Item.Product == null)
                .ToList();

            // Создаем продукты для просроченных договоров
            foreach (var contract in expiredContracts)
            {
                var product = new Product
                {
                    ItemId = contract.Item.Id,
                    SalePrice = contract.Item.EstimatedValue * 1.2m, // 20% наценка
                    ListingDate = currentDate
                };

                _context.Products.Add(product);
            }

            // Сохраняем изменения
            _context.SaveChanges();
        }
    }
}