using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Exceptions;

namespace Servidor
{
    public class UserService : IUserService
    {
        private UserData userData;
        public UserService() {
            userData = UserData.getInstance();
        }
        public void AddUser(string name, string password)
        {
            Administrator user = new Administrator(name, password);
            if (!userData.Exists(name))
                userData.Add(user);
            else
                throw new UserException("Ya existe un usuario con ese nombre");
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
                return userData.Update(originalName, user);
            }
            else
            {
                throw new UserException("Ya existe un usuario con ese nombre");
            }
        }

        public bool Delete(string name)
        {
            return userData.Delete(name);
        }

    }
}
