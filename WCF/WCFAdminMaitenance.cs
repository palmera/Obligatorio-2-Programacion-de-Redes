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
        private AdminLogic adminLogic;
        public WCFAdminMaintenance() {
            adminLogic = new AdminLogic();
        }
        public bool AddAdmin(string name, string password)
        {
            return adminLogic.AddAdmin(name, password);
        }

        public bool DeleteAdmin(string name, string password)
        {
            return adminLogic.DeleteAdmin(name, password);
        }

        public List<string> GetAllAdmins()
        {
            return adminLogic.GetAllAdmins();
        }

        public bool Login(string name, string password)
        {
            return adminLogic.Login(name, password);
        }

        public bool ModifyAdmin(string name, string password, string newName)
        {
            throw new NotImplementedException();
        }
    }
}
