using RemotingConsumer.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace UserMaintenance
{
    public class LogValidator
    {
        private AdminLogic adminLogic;

        public LogValidator()
        {
            this.adminLogic = new AdminLogic();
        }

        public bool AdminLogged(HttpRequestHeaders header)
        {
    
            if(header.Contains("name") && header.Contains("password"))
            {
                var name = header.GetValues("name").First();
                var password = header.GetValues("password").First();
                return adminLogic.Login(name, password);
            }
            return false;
        }
    }
}