using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LombardSystem.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ContactInfo { get; set; }
        public string PassportData { get; set; }
        public List<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
