﻿using LoggerHelper;
using Services;
using Servidor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Exceptions;

namespace Servidor
{
    public class UserService : MarshalByRefObject, IUserService
    {
        private UserData userData;
        public UserService() {
            userData = UserData.getInstance();
        }
        public void AddUser(string name, string password)
        {
            Administrator user = new Administrator(name, password);
            if (!userData.Exists(name))
            {
                LoggerSender.Log("Se agrega un usuario \n");
                userData.Add(user);
            }
            else
            {
                throw new UserException("Ya existe un usuario con ese nombre");
            }
        }

        public bool Login(string name, string password)
        {
            Administrator user = new Administrator(name, password);
            return userData.UserLogin(user);
        }

        public bool Update(string originalName,string newName,string password)
        {
            if (!userData.Exists(newName))
            {
                Administrator user = new Administrator(newName, password);
                LoggerSender.Log("Se Actualiza al usuario "+newName+" \n");
                return userData.Update(originalName, user);
            }
            else
            {
                throw new UserException("Ya existe un usuario con ese nombre");
            }
        }

        public bool Delete(string name)
        {
            LoggerSender.Log("Se Borrar al usuario " + name + " \n");
            return userData.Delete(name);
        }

    }
}
