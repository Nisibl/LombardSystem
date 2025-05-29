using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LombardSystem.Interfaces
{
    internal interface IAdminDB
    {
        void UpdateProductPrice(int productId, decimal newPrice);
    }
}
