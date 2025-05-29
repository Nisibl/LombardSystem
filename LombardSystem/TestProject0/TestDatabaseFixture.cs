using LombardSystem.Models;
using Microsoft.EntityFrameworkCore;
using LombardSystem.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject0
{
    public class TestDatabaseFixture : IDisposable
    {
        public DatabaseContext Context { get; }

        public TestDatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "LombardTestDB")
                .Options;

            Context = new DatabaseContext();
            SeedTestData();
        }

        private void SeedTestData()
        {
            // Добавляем тестовых пользователей
            Context.Users.Add(new User { Username = "admin", Password = "1", Role = "Owner" });
            Context.Users.Add(new User { Username = "employee", Password = "1", Role = "Employee" });

            // Добавляем тестовых клиентов
            Context.Clients.Add(new Client { FullName = "Иванов Иван", ContactInfo = "+79111234567", PassportData = "4510 123456" });

            // Добавляем тестовые предметы
            Context.Items.Add(new Item { Description = "Золотое кольцо", EstimatedValue = 15000, ReceiptDate = DateTime.UtcNow });

            Context.SaveChanges();
        }

        public void Dispose() => Context.Dispose();
    }
}
