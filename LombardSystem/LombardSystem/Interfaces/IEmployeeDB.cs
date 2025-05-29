using LombardSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LombardSystem.Interfaces
{
    internal interface IEmployeeDB
    {
        void AddClient(string fullName, string contactInfo, string passportData);
        void AddItem(string description, decimal estimatedValue);
        void CreateContract(int clientId, int itemId, DateTime signDate, DateTime expiryDate, decimal loanAmount);
        void SellProduct(int productId);
        List<Client> GetClients();
        List<Item> GetItems();
        List<Contract> GetContracts();
        List<Product> GetProducts();
    }
}
