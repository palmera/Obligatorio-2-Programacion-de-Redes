using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Servidor
{
    public class RemotingAdminService: MarshalByRefObject, IRemotingAdminService.IRemotingAdminService
    {
        public void AddAdmin(int admin) {
            var SD = ServerData.getInstance();
            //SD.AddAdmin(admin);
        }
    }
}
