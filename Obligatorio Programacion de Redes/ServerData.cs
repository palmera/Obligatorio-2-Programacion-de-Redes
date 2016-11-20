using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obligatorio_Programacion_de_Redes.Models;

namespace Servidor
{
    public class ServerData
    {
        private static ServerData SD;
        
        private List<Administrator> admins;

        private ServerData() { }

        public static ServerData getInstance() {
            if (SD == null)
            {
                Administrator rootUser = new Administrator("admin", "admin");
                SD = new ServerData();
                SD.admins = new List<Administrator>();
                SD.AddAdmin(rootUser);
                return SD;
            }
            else
            {
                return SD;
            }
        }

        public bool AddAdmin(Administrator admin) {
            if (admins.Any(a => a.Name.Equals(admin.Name)))
            {
                return false;
            }
            else {
                admins.Add(admin);
                return true;
            }
            
        }
        public bool AdminLogin(Administrator admin) {
            return admins.Any(a => a.Name.Equals(admin.Name)&&a.Password.Equals(admin.Password));
        }
        public bool ModifyAdmin(string oldName, string newName) {
            var adminToModify = admins.Find(x => x.Name.Contains(oldName));
            if (adminToModify != null)
            {
                adminToModify.Name = newName;
                return true;
            } else return false;
        }
        public bool DeleteAdmin(string name) {
            var adminToDelete = admins.Find(x => x.Name.Contains(name));
            return admins.Remove(adminToDelete);
        }
        public List<string> GetAllAdmins() {
            List<string> ret = new List<string>();
            foreach (var admin in admins)
            {
                ret.Add(admin.Name);
            }
            return ret;
        }
        public bool AdminExists(string name)
        {
            return admins.Find(x => x.Name.Contains(name))!=null;
        }

    }
}
