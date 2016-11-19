using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Models;

namespace RemotingConsumer.Logic.WCFAdminServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WCFAdminService" in both code and config file together.
    public class WCFAdminService : IWCFAdminService
    {
        public void DoWork()
        {
        }

        
        public bool Login(string name, string password)
        {
            IRemotingAdminService.IRemotingAdminService adminService;
            adminService = (IRemotingAdminService.IRemotingAdminService)Activator.GetObject
                (typeof(IRemotingAdminService.IRemotingAdminService),
                "tcp://localhost:5000/RemotingAdminService");
            return adminService.LoginAdmin(name, password);
        }
    }
}
