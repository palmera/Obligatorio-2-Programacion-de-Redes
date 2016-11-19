using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace RemotingAdminService
{
    public class RemotingAdminService: MarshalByRefObject, IRemotingAdminService.IRemotingAdminService
    {
        public void AddAdmin(Administrator admin) {
            var SD = ServerData.getInstance();
            SD.AddAdmin(admin);
        }
    }
}
