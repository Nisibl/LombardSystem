using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LombardSystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime ListingDate { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
