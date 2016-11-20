using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRemotingAdminService;
using Servidor.Models;

namespace Servidor
{
    public class RemotingAdminService: MarshalByRefObject, IRemotingAdminService.IRemotingAdminService
    {
        //iniciar SD en cada metodo en el peor caso posible
        private ServerData SD = ServerData.getInstance();
        public bool LoginAdmin(string name, string password) {
            var admin = new Administrator(name, password);
            return SD.AdminLogin(admin);
        }
        public bool AddAdmin(string name, string password) {
            var admin = new Administrator(name, password);
            return SD.AddAdmin(admin);
        }

        public bool ModifyAdmin(string name, string newName) {
            return SD.ModifyAdmin(name, newName);
        }

        public bool DeleteAdmin(string name) {
            return SD.DeleteAdmin(name);
        }

        public List<string> GetAllAdmins()
        {
            return SD.GetAllAdmins();
        }

        public bool AdminExists(string name)
        {
            return SD.AdminExists(name);
        }
    }
}
