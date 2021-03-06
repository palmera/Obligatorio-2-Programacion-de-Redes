﻿using System;
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
        IWCFAdminMaintenance service = new WCFAdminMaintenance();
        public bool Login(string name,string password) {
            return service.Login(name, password);
        }

        public List<string> GetAllAdmins() {
            return service.GetAllAdmins();
        }

        public bool AddAdmin(string name, string password) {
            return service.AddAdmin(name, password);
        }

        public bool DeleteAdmin(string name) {
            return service.DeleteAdmin(name);
        }
        public bool ModifyAdmin(string name, string newName)
        {
            return service.ModifyAdmin(name, newName);
        }
        public bool AdminExists(string name) {
            return service.AdminExists(name);
        }
    }
}
