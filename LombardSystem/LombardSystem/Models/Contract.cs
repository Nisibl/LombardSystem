using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LombardSystem.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public DateTime SignDate { get; set; } = DateTime.UtcNow;
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddMonths(1);
        public decimal LoanAmount { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
