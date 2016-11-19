using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RemotingConsumer.Logic.WCFAdminServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWCFAdminService" in both code and config file together.
    [ServiceContract]
    public interface IWCFAdminService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        bool Login(string name, string password);
    }
}
