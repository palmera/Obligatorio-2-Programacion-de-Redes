using RemotingConsumer.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class WCFAdminMaintenance : IWCFAdminMaintenance
    {
        public void AddAdmin(string name, string password)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAdmin(string name, string password)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllAdmins()
        {
            return AdminLogic.GetAllAdmins();
        }

        public bool Login(string name, string password)
        {
            return AdminLogic.Login(name, password);
        }

        public bool ModifyAdmin(string name, string password, string newName)
        {
            throw new NotImplementedException();
        }

        public bool VerifyDeleteAdmin(string name, string password)
        {
            throw new NotImplementedException();
        }
    }
}
