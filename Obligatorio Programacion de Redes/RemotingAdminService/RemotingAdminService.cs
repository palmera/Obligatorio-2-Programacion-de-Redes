using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Servidor
{
    public class RemotingAdminService: MarshalByRefObject, IRemotingAdminService.IRemotingAdminService
    {
        public bool LoginAdmin(string name, string password) {
            var SD = ServerData.getInstance();
            var admin = new Administrator(name, password);
            return SD.AdminLogin(admin);
        }
        public void AddAdmin(string name, string password) {
            var SD = ServerData.getInstance();
            var admin = new Administrator(name, password);
            SD.AddAdmin(admin);
        }
    }
}
