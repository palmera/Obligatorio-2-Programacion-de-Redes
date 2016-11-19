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
        public static string rootUser = "admin";
        public static string rootPass = "admin";

        private List<Administrator> admins;

        private ServerData() { }

        public static ServerData getInstance() {
            if (SD != null)
            {
                SD = new ServerData();
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
    }
}
