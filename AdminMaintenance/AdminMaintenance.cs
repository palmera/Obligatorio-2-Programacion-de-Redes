using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AdminMaintenance
{
    class AdminMaintenance
    {
        public bool Login(string name,string password) {
            using (var host = new ServiceHost(typeof(IAdminWCFService)))
            {    
                return host.Login(name, password);
            }
        }
    }
}
