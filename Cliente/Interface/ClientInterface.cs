using Cliente.Exceptions;
using Cliente.Files;
using Cliente.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Interface
{
    public class ClientInterface
    {
        public static int OPTION_EXIT = 5;

        private Client client;
        private string serverIP;
        private int port;

        public ClientInterface()
        {
            client = new Client();
        }
        public void start()
        {
            bool connected = false;
            Console.WriteLine("Ingrese Ip del servidor: ");
            this.serverIP= Console.ReadLine();
            this.port = 6000;
            while (!connected)
            {
                try
                {
                    connected = showLoginMenu();
                }
                catch (SocketException ex) {
                    Console.WriteLine(ex.Message);
                    connected = false;
                    start();
                }
            }
                    
        }
        private bool showLoginMenu()
        {
            Console.WriteLine("Ingrese nombre de usuario");
            string name = Console.ReadLine();
            if (client.connect(name, serverIP, port))
            {
                Console.WriteLine("CLIENTE CONECTADO");
                mainMenu();
                return true;
            }
            else {
                Console.WriteLine("NO PUDO INICIAR SESIÓN");
                return false;
            }
        }
        public void mainMenu()
        {
            int option = 0;
            while (true) {
                try
                {
                    showMainMenu();
                    option = int.Parse(Console.ReadLine());
                    menuSwitch(option);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Comando invalido");
                }
            }
        }
        private void showMainMenu()
        {
            Console.WriteLine("*******************************\n");
            Console.WriteLine("1) LISTAR ARCHIVOS");
            Console.WriteLine("2) DESCARGAR ARCHIVO PARA EDITAR");
            Console.WriteLine("3) DESCARGAR ARCHIVO PARA LECTURA");
            Console.WriteLine("4) VER MIS ARCHIVOS");
            Console.WriteLine("5) DEVOLVER ARCHIVO LEIDO");
            Console.WriteLine("6) DEVOLVER ARCHIVO ACTUALIZADO");
            Console.WriteLine("*******************************\n");

        }
        private void menuSwitch(int option)
        {
            try
            {
                switch (option)
                {
                    case 1:
                        client.listFiles();
                        break;
                    case 2:
                        Console.WriteLine("Ingrese el nombre del archivo que quiere editar");
                        string filename = Console.ReadLine();
                        client.downloadFileForEdit(filename);
                        break;
                    case 3:
                        Console.WriteLine("Ingrese el nombre del archivo que quiere leer");
                         filename = Console.ReadLine();
                        client.downloadFileForReading(filename);
                        break;
                    case 4:
                        Console.WriteLine("MIS ARCHIVOS: \n");
                        showMyFiles();
                        break;
                    case 5:
                        Console.WriteLine("Ingrese el nombre del archivo que termino de leer");
                        filename = Console.ReadLine();
                        client.returnReadingPrivileges(filename);
                        break;
                    case 6:
                        Console.WriteLine("Ingrese el nombre del archivo que termino de escribir");
                        filename = Console.ReadLine();
                        client.returnUpdatedFile(filename);
                        break;
                    default:
                        Console.WriteLine("Comando invalido");
                        break;
                }
            }
            catch (ClientException ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private void showMyFiles()
        {
            foreach(ClientFile clientFile in client.myFiles)
            {
                Console.WriteLine(clientFile.name + "  - " + clientFile.getStatus());           
            }
        }
    }
}
