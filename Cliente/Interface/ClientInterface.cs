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
            Console.WriteLine("Ingrese nombre de usuario/administrador");
            string name = Console.ReadLine();
            Console.WriteLine("Ingrese contrasena");
            string password = Console.ReadLine();
            string token = name + '|' + password;
            int response = client.connect(token, serverIP, port);
            switch (response)
            {
                case 1:
                    Console.WriteLine("ADMINISTRADOR CONECTADO");
                    mainAdminMenu();
                    return true;
                case 2:
                    Console.WriteLine("CLIENTE CONECTADO");
                    mainUserMenu();
                    return true;
                case 3:
                    Console.WriteLine("NO PUDO INICIAR SESIÓN");
                    return false;
                default:
                    return false;
            }
        }
        public void mainUserMenu()
        {
            int option = 0;
            while (true) {
                try
                {
                    showUserMainMenu();
                    option = int.Parse(Console.ReadLine());
                    if (option > 1 || option < 6)
                    {
                        throw new Exception("Comando invalido");
                    }
                    menuSwitch(option);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Comando invalido");
                }
                catch (Exception e){
                    Console.WriteLine(e.Message);
                }
            }
        }
        public void mainAdminMenu()
        {
            int option = 0;
            while (true)
            {
                try
                {
                    showAdminMainMenu();
                    option = int.Parse(Console.ReadLine());
                    menuSwitch(option);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Comando invalido");
                }
            }
        }
        private void showAdminMainMenu()
        {
            Console.WriteLine("*******************************\n");
            Console.WriteLine("1) LISTAR ARCHIVOS");
            Console.WriteLine("2) DESCARGAR ARCHIVO PARA EDITAR");
            Console.WriteLine("3) DESCARGAR ARCHIVO PARA LECTURA");
            Console.WriteLine("4) VER MIS ARCHIVOS");
            Console.WriteLine("5) DEVOLVER ARCHIVO LEIDO");
            Console.WriteLine("6) DEVOLVER ARCHIVO ACTUALIZADO");
            Console.WriteLine("7) CREAR ARCHIVO");
            Console.WriteLine("8) BORRAR ARCHIVO");
            Console.WriteLine("9) CAMBIAR NOMBRE DE ARCHIVO");
            Console.WriteLine("*******************************\n");

        }
        private void showUserMainMenu()
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
                string filename = "";
                switch (option)
                {
                    case 1:
                        client.listFiles();
                        break;
                    case 2:
                        Console.WriteLine("Ingrese el nombre del archivo que quiere editar");
                        filename = Console.ReadLine();
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
                    case 7://crear archivo
                        Console.WriteLine("Ingrese la ruta del archivo que quiere subir");
                        filename = Console.ReadLine();
                        client.returnUpdatedFile(filename);
                        break;
                    case 8://eliminar archivo
                        Console.WriteLine("Ingrese el nombre del archivo desea eliminar");
                        filename = Console.ReadLine();
                        client.returnUpdatedFile(filename);
                        break;
                    case 9://cambiar nombre de archivo
                        Console.WriteLine("Ingrese el nombre del archivo desea cambiar el nombre");
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
