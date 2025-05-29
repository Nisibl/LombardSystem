using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LombardSystem.Models;

namespace LombardSystem.Interfaces
{
    public interface IUserDB
    {
        User Authenticate(string username, string password);
    }
}
