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
        IWCFAdminMaintenance wcf = new WCFAdminMaintenance();
        public bool Login(string name,string password) {
            return wcf.Login(name, password);
        }
    }
}
