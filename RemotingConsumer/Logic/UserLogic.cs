using Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingConsumer.Logic
{
    public class UserLogic
    {
        private IUserService userService;

        public UserLogic() {
            string remoteRoute = ConfigurationManager.AppSettings["userRemotingRoute"];
            userService = (IUserService)Activator.GetObject
                (typeof(Services.IUserService),
                remoteRoute+"/RemotingUserService");
        }
        public bool LoginUser(string name, string password) {
            return userService.Login(name, password);
        }
        public void AddUser(string name,string password)
        {
            userService.AddUser(name, password);
        }

        public bool UpdateUser(string name,string newName,string password)
        {
            return userService.Update(name, newName, password);
        }
        public bool DeleteUser(string name)
        {
            return userService.Delete(name);
        }

    }
}
