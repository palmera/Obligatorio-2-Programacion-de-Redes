using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;


namespace Servidor
{
    public class ServerData
    {
        private static ServerData SD;
        //public static string rootUser = "admin";
        //public static string rootPass = "admin";
        

        private List<Administrator> admins;

        private ServerData() { }

        public static ServerData getInstance() {
            if (SD == null)
            {
                Administrator rootUser = new Administrator("admin", "admin");
                Administrator rootUser1 = new Administrator("admin1", "admin1");
                Administrator rootUser2 = new Administrator("admin2", "admin2");
                Administrator rootUser3 = new Administrator("admin3", "admin3");
                SD = new ServerData();
                SD.admins = new List<Administrator>();
                SD.AddAdmin(rootUser);
                SD.AddAdmin(rootUser1);
                SD.AddAdmin(rootUser2);
                SD.AddAdmin(rootUser3);
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
        public bool ModifyAdmin(Administrator admin, string newName) {
            var adminToModify = admins.Find(x => x.Name.Contains(admin.Name)&&x.Password.Contains(admin.Password));
            if (adminToModify != null)
            {
                adminToModify.Name = newName;
                return true;
            } else return false;
        }
        public bool DeleteAdmin(Administrator admin) {
            var adminToDelete = admins.Find(x => x.Name.Contains(admin.Name) && x.Password.Contains(admin.Password));
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
    }
}
