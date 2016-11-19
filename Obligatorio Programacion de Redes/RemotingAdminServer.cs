using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Servidor
{
    class RemotingAdminServer
    {
        public static void StartAdminRemoting()
        {
            System.Collections.IDictionary dict = new System.Collections.Hashtable();
            dict["name"] = "RemotingAdminServerTcp";
            dict["port"] = 5000;
            dict["authenticationMode"] = "IdentifyCallers";

            // Set up the server channel.
            var serverChannel = new TcpChannel(dict, null, null);
            try
            {
                //RemotingConfiguration.CustomErrorsMode = CustomErrorsModes.Off;
                // Specify the properties for the server channel.
                ChannelServices.RegisterChannel(serverChannel, false);
                RemotingConfiguration.RegisterWellKnownServiceType(
                    typeof(RemotingAdminService),
                    "RemotingAdminService",
                    WellKnownObjectMode.SingleCall);
                Console.WriteLine("Remoting admin server started...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                //log exception
            }
            finally
            {
                ChannelServices.UnregisterChannel(serverChannel);
            }

        }
    }
}
