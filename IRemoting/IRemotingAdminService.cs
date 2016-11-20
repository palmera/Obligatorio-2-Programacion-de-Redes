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
        void AddAdmin(string name, string password);
        bool ModifyAdmin(string name, string password, string newName);
        bool DeleteAdmin(string name, string password);
        List<string> GetAllAdmins();
        bool VerifyDeleteAdmin(string name, string password);

    }
}
