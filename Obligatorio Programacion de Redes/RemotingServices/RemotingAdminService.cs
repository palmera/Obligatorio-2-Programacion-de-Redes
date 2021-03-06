﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRemotingAdminService;
using Servidor.Models;
using LoggerHelper;

namespace Servidor
{
    public class RemotingAdminService: MarshalByRefObject, IRemotingAdminService.IRemotingAdminService
    {
        //iniciar SD en cada metodo en el peor caso posible
        private ServerData SD = ServerData.getInstance();
        public bool LoginAdmin(string name, string password) {
            var admin = new Administrator(name, password);
            var a = SD.AdminLogin(admin);
            LoggerSender.Log("Admin login: Username: "+name+" Password: " +password);
            return a;
        }
        public bool AddAdmin(string name, string password) {
            var admin = new Administrator(name, password);
            LoggerSender.Log("Create Admin:Username: " + name + "Password: " + password);
            return SD.AddAdmin(admin);
        }
        public bool ModifyAdmin(string name, string newName) {
            LoggerSender.Log("Modify Admin:Old name: " + name + "New name: " + newName);
            return SD.ModifyAdmin(name, newName);
        }

        public bool DeleteAdmin(string name) {
            LoggerSender.Log("Delete Admin: Username: " + name);
            return SD.DeleteAdmin(name);
        }

        public List<string> GetAllAdmins()
        {
            return SD.GetAllAdmins();
        }

        public bool AdminExists(string name)
        {
            return SD.AdminExists(name);
        }
    }
}
