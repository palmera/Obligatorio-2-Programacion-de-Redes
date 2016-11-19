using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingConsumer.Logic
{
    public class AdminLogic
    {
        public static bool Login(string name, string password)
        {
            IRemotingAdminService.IRemotingAdminService adminService;
            adminService = (IRemotingAdminService.IRemotingAdminService)Activator.GetObject
                (typeof(IRemotingAdminService.IRemotingAdminService),
                "tcp://192.168.1.47:5000/RemotingAdminService");
            return adminService.LoginAdmin(name, password);
        }
    }
}
