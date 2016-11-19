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
            if (SD != null)
            {
                Administrator rootUser = new Administrator("admin", "admin");
                SD = new ServerData();
                SD.AddAdmin(rootUser);
                return SD;
            }
            else
            {
                return SD;
            }
        }

        public void AddAdmin(Administrator admin) {
            this.admins.Add(admin);
        }
        public bool AdminLogin(Administrator admin) {
            return admins.Any(a => a.Name.Equals(admin.Name)&&a.Password.Equals(admin.Password));
        }
    }
}
