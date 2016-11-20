using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRemotingAdminService;
using Models;

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
        public void AddAdmin(string name, string password) {
            var admin = new Administrator(name, password);
            SD.AddAdmin(admin);
        }

        public bool ModifyAdmin(string name, string password, string newName) {
            var adminToModify = new Administrator(name, password);
            return SD.ModifyAdmin(adminToModify, newName);
        }

        public bool DeleteAdmin(string name, string password) {
            var admin = new Administrator(name, password);
            return SD.DeleteAdmin(admin);
        }

        public List<string> GetAllAdmins()
        {
            return SD.GetAllAdmins();
        }

        public bool VerifyDeleteAdmin(string name, string password)
        {
            throw new NotImplementedException();
        }
    }
}
