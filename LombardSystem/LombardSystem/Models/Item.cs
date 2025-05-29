using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LombardSystem.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal EstimatedValue { get; set; }
        public DateTime ReceiptDate { get; set; }
        public Contract Contract { get; set; }
        public Product Product { get; set; }
    }
}
