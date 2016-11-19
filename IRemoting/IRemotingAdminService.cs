using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace IRemotingAdminService
{
    public interface IRemotingAdminService
    {
        bool LoginAdmin(string name, string password);
        void AddAdmin(string name, string password);

    }
}
