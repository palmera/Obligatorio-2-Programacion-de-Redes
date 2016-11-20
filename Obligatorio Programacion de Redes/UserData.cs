using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    public class UserData
    {
        private static UserData userData;
        private static Object userDataLock = new Object();
        public List<Administrator> users { get; set; }

        public static UserData getInstance() {
            if (userData == null)
                userData = new UserData();
            return userData;
        }
        private UserData() {
            users = new List<Administrator>();
        }

        public bool UserLogin(Administrator  user)
        {
            return users.Any(a => a.Name.Equals(user.Name) && a.Password.Equals(user.Password));
        }
        public void Add(Administrator user) {
            lock (userDataLock)
            {
                users.Add(user);
            }
        }
        public bool Update(string name,Administrator user) {
            lock(userDataLock){
                int index = users.FindIndex((u => u.Name.Equals(name)));
                Administrator element = users.ElementAt(index);
                if (element != null)
                {
                    element.Name = user.Name;
                    element.Password = user.Password;
                    return true;
                }
                return false;
            }
        }
        public bool Delete(string name)
        {
            lock (userDataLock) { 
                int index = users.FindIndex((u => u.Name.Equals(name)));
                Administrator element = users.ElementAt(index);
                if (element != null)
                {
                    users.Remove(element);
                    return true;
                }
                return false;
            }

        }
        public bool Exists(string name) {
            return users.Exists(u => u.Name.Equals(name));
        }
    }
}
