using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
   public interface IUserService
    {
        bool Login(string name, string password);
        void AddUser(string name,string password);
        bool Update(string originalName, string newName, string password);
        bool Delete(string name);
    }
}
