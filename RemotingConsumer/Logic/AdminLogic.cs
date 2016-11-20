using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingConsumer.Logic
{
    public class AdminLogic
    {
        private IRemotingAdminService.IRemotingAdminService adminService;

        public AdminLogic() {
            adminService = (IRemotingAdminService.IRemotingAdminService)Activator.GetObject
                (typeof(IRemotingAdminService.IRemotingAdminService),
                "tcp://192.168.1.18:5000/RemotingAdminService");
        }

        public bool Login(string name, string password)
        {
            return adminService.LoginAdmin(name, password);
        }

        public List<string> GetAllAdmins()
        {
            return adminService.GetAllAdmins();
        }

        public bool AddAdmin(string name, string password) {
            return adminService.AddAdmin(name, password);
        }

        public bool DeleteAdmin(string name)
        {
            return adminService.DeleteAdmin(name);
        }

        public bool ModifyAdmin(string name, string newName) {
            return adminService.ModifyAdmin(name, newName);
        }

        public bool AdminExists(string name)
        {
            return adminService.AdminExists(name);
        }
    }
}
