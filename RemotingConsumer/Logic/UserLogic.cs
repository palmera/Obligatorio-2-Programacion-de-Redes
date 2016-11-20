using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingConsumer.Logic
{
    public class UserLogic
    {
        private IUserService userService;

        public UserLogic() {
            userService = (IUserService)Activator.GetObject
                (typeof(Services.IUserService),
                "tcp://192.168.1.27:5000/RemotingUserService");
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
