using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RemotingConsumer.Logic
{
    public class AdminLogic
    {
        private IRemotingAdminService.IRemotingAdminService adminService;

        public AdminLogic() {
            string remoteRoute = ConfigurationManager.AppSettings["adminRemotingRoute"];
            adminService = (IRemotingAdminService.IRemotingAdminService)Activator.GetObject
                (typeof(IRemotingAdminService.IRemotingAdminService),
                remoteRoute+"/RemotingAdminService");
        }

        public bool Login(string name, string password)
        {
            try
            {
                return adminService.LoginAdmin(name, password);
            }catch(SocketException ex)
            {
                return false;
            }
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
