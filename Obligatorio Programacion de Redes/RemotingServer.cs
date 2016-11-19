using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Obligatorio_Programacion_de_Redes
{
    class RemotingServer
    {
        static void Main(string[] args)
        {
            System.Collections.IDictionary dict = new System.Collections.Hashtable();
            dict["name"] = "CalculatorServerTcp";
            dict["port"] = 5000;
            dict["authenticationMode"] = "IdentifyCallers";

            // Set up the server channel.
            var serverChannel = new TcpChannel(dict, null, null);
            try
            {
                // Specify the properties for the server channel.
                ChannelServices.RegisterChannel(serverChannel, false);
                RemotingConfiguration.RegisterWellKnownServiceType(
                    typeof(CalculatorService.CalculatorService),
                    "CalculatorService",
                    WellKnownObjectMode.SingleCall);
                Console.WriteLine("Remoting server started...");
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
