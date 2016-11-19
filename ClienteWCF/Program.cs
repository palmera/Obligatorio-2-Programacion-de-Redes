using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Logic;

namespace ClienteWCF
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("starting http service");
            using (var host = new ServiceHost(typeof(Logic.CalculatorService)))
            {
                //host.Open();
                Console.WriteLine("press return to exit");
                
                host.Close();
            }

            /*var cliente = new ServiceReference1.Service1Client();
            var result = cliente.Add(10, 40);
            Console.WriteLine(result.ToString());*/
            Console.ReadLine();

        }
    }
}
