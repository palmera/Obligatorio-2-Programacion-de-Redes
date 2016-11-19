using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AdminMaintenance
{
    class Maintenance
    {
        public void Login(string name,string password) {
            using (var host = new ServiceHost(typeof(IAdminWCFService)))
            {
                host.Login(name, password);
                Console.WriteLine("press return to exit");

                host.Close();
            }
        }
    }
}
