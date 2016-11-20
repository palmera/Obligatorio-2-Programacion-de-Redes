using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRemotingAdminService
{
    public interface IRemotingAdminService
    {
        bool LoginAdmin(string name, string password);
        bool AddAdmin(string name, string password);
        bool ModifyAdmin(string name, string password, string newName);
        bool DeleteAdmin(string name, string password);
        List<string> GetAllAdmins();

    }
}
