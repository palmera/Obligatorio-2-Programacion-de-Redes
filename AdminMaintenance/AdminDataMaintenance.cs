using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF;

namespace AdminMaintenance
{
    class AdminDataMaitenance
    {
        WCFAdminMaintenance service = new WCFAdminMaintenance();
        public bool Login(string name,string password) {
            return service.Login(name, password);
        }

        public List<string> GetAllAdmins() {
            return service.GetAllAdmins();
        }
    }
}
