using Obligatorio_Programacion_de_Redes.Files;
using Protocols;
using Servidor.LoggerHelper;
using Servidor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servidor
{
    class Server
    {
        public static byte[] data = new byte[16];
        private static Protocol protocol;
        private static List<TcpClient> clientes;
        private static List<Files> serverFiles;
        private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Archivos";
        private static Object lock_object = new object();
        private static LoggerReceiver loggerReceiver;
        static void Main(string[] args)
        {
            Thread remotingAdminThread = new Thread(() => RemotingAdminServer.StartAdminRemoting());
            remotingAdminThread.Start();
            Thread remotingUserThread = new Thread(() => RemotingAdminServer.StartUserRemoting());
            remotingUserThread.Start();
            loggerReceiver = new LoggerReceiver();
            loggerReceiver.StartMessageQue();
            //RemotingAdminServer.StartUserRemoting();
            //RemotingAdminServer.StartAdminRemoting();
            serverFiles = getFilesList();
            
            Console.WriteLine(buildFileListing(serverFiles));
            
            protocol = new Protocol();
            clientes = new List<TcpClient>();
            aceptarConecciones();
        }
        private static List<Files>  getFilesList()
        {
            serverFiles = new List<Files>();
            string[] filesPath = Directory.GetFiles(startupPath);
            string[] fileNames = GetFileNames(startupPath);
            for (int i = 0; i < fileNames.Length; i++)
            {
                Files f = new Files();
                f.path = filesPath[i];
                f.name = fileNames[i];

                serverFiles.Add(f);
            }
            return serverFiles;
        }
        private static string[] GetFileNames(string path)
        {
            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
                files[i] = Path.GetFileName(files[i]);
            return files;
        }
        

        private static void aceptarConecciones()
        {
            TcpListener listener;
            IPAddress localAdd = IPAddress.Parse(GetLocalIPAddress());
            TcpClient clienteTcp;
            //---start listening for incoming connection---
            listener = new TcpListener(localAdd, 6000);
            Console.WriteLine("local ip address: "+ localAdd);
            listener.Start();
            while (true)
            {
                try
                {
                    clienteTcp = listener.AcceptTcpClient();
                    clientes.Add(clienteTcp);
                    Thread hiloCliente = new Thread(() => receiveData(clienteTcp));
                    hiloCliente.Start();
                }catch(Exception ex) { }
            }
        }

        

        private static void receiveData(TcpClient clientConnected)
        {
            string nombreUsuario = "";
            while (true)
            {
                try
                {
                    loggerReceiver.Receive();
                    byte[] header = new byte[9];

                    header = protocol.receiveData(clientConnected, 9);
                    if (header != null)
                        executeAction(header, clientConnected, ref nombreUsuario);
                }catch(IOException ex) {
                    try
                    {
                        sendError(clientConnected.GetStream(), ex.Message);

                    }
                    catch (IOException)
                    {

                    }
                    catch (InvalidOperationException)
                    {

                    }
                }
            }
        }

        private static void clientLogin(string data, NetworkStream nws)
        {
            string userName = data.Split('|')[0];
            string password = data.Split('|')[1];
            Administrator user = new Administrator(userName, password);
            byte[] userData;

            bool userExists = UserData.getInstance().UserLogin(user);
            bool adminExists = ServerData.getInstance().AdminLogin(user);
            if (adminExists)
            {
                Console.WriteLine("Logueado como administrador: "+userName);
                userData = protocol.authenticationResponse(1);
            }
            else if (userExists)
            {
                Console.WriteLine("Logueado como usuario: " + userName);
                userData = protocol.authenticationResponse(2);
            }
            else
            {
                userData = protocol.authenticationResponse(3);
            }
            string b = Encoding.ASCII.GetString(userData);
            nws.Write(userData, 0, userData.Length);
        }

        private static string buildFileListing(List<Files> files) {
            string ret = "";
            foreach (var item in files)
            {
                ret = ret + "file: " + item.name + "\n";
            }
            return ret;
        }

        private static void executeAction(byte[] header,TcpClient connection, ref string clientName)
        {
            NetworkStream nws = connection.GetStream();
            int largoDatos = protocol.dataLength(header);
            byte[] data = new byte[largoDatos];
            data = protocol.receiveData(connection, largoDatos);
            int comando = protocol.getAction(header);
            //string nombreUsuario = "";
            switch (comando)
            {
                case 0://cliente se conecta
                    string name = UnicodeEncoding.ASCII.GetString(data);
                    connection.ReceiveBufferSize = 32;
                    int bufferSize = data.Length;


                    clientLogin(name, nws);
                    /*byte[] a = protocol.authenticationResponse(clientesPermitidos.Contains(name));
                    string b = Encoding.ASCII.GetString(a);
                    nws.Write(protocol.authenticationResponse(clientesPermitidos.Contains(name)), 0, a.Length);*/

                    var response = new byte[12];


                    clientName = System.Text.Encoding.ASCII.GetString(data);
                    //connection.Send(protocol.ArmarTramaRespuestaAutenticacionCliente(UsuarioValido(nombreUsuario)));
                    break;
                case 10: //cliente pide listado de archivos
                    string[] files = Directory.GetFiles(startupPath);

                   // sendDataToClient(buildFileListing(files), nws);
                    byte[] a = protocol.headerSendListing(buildFileListing(getFilesList()));
                    string b = Encoding.ASCII.GetString(a);
                    nws.Write(a, 0, a.Length);

                    break;
                case 20://recibo del cliente el nombre del archivo que quiere editar
                    Console.WriteLine("usuario pidiendo archivo para edicion: "+ clientName);
                    string fileName = System.Text.Encoding.ASCII.GetString(data);
                    lock(lock_object){
                        Files selectedFile = findFile(fileName);
                        //verifico que el archivo existe, no esta siendo editado, y no esta siendo leido para poder mandarlo
                        if (selectedFile != null && !selectedFile.isBeingEdited() && !selectedFile.isBeingRead())
                        {

                            FileStream fs;
                            fs = new FileStream(selectedFile.path, FileMode.Open, FileAccess.Read);
                            byte[] bytesToSend = new byte[fs.Length];

                            fs.Read(bytesToSend, 0, bytesToSend.Length);

                            byte[] package = protocol.headerSendEditingFile(System.Text.Encoding.ASCII.GetString(bytesToSend), selectedFile.name);
                            nws.Write(package, 0, package.Length);

                            fs.Close();
                            selectedFile.writer = clientName;
                        }
                        else
                        {
                            sendError(nws, "El archivo no se encuentra disponible pare edición, intente nuevamente");
                        }
                    }
                    break;

                case 30:
                    sendFileForReading(nws,clientName,data);
                    break;

                case 40:
                    returnReadingPrivileges(nws, clientName, data);
                    break;
                case 50:
                    downloadUpdatedFile(nws, clientName, data);
                    break;
                case 60:
                    createFile(nws, data);
                    break;
                case 70:
                    deleteFile(nws, data);
                    break;
                case 80:
                    changeFileName(nws, data);
                    break;
                default:    
                    string hola = System.Text.Encoding.ASCII.GetString(header);
                    Console.WriteLine("cliente no conectado es: " + hola);
                    break;
            }
        }


        private static Files findFile(string fileName)
        {
            foreach (var item in serverFiles)
            {
                if (item.name == fileName)
                {
                    return item;
                }
            }
            return null;
        }

        private static void sendFileForReading(NetworkStream nws,string clientName,byte[] data)
        {
            Console.WriteLine("usuario pidiendo archivo para lectura: " + clientName);
            string fileName = System.Text.Encoding.ASCII.GetString(data);
            Files selectedFile = findFile(fileName);
                //verifico que el archivo existe, no esta siendo editado, y no esta siendo leido para poder mandarlo
            if (selectedFile != null)
            {
                FileStream fs;
                fs = new FileStream(selectedFile.path, FileMode.Open, FileAccess.Read);
                byte[] bytesToSend = new byte[fs.Length];

                fs.Read(bytesToSend, 0, bytesToSend.Length);

                byte[] package = protocol.headerSendReadingFile(System.Text.Encoding.ASCII.GetString(bytesToSend), selectedFile.name);
                nws.Write(package, 0, package.Length);

                fs.Close();
                selectedFile.readers.Add(clientName);
            }
            else
            {
                sendError(nws, "No se pudo encontrar el archivo, revise el nombre");
            }
        }

        private static void returnReadingPrivileges(NetworkStream nws,string clientName,byte[] data)
        {
            string fileName = System.Text.Encoding.ASCII.GetString(data);
            Files selectedFile = findFile(fileName);
            if (selectedFile != null)
            {
                if (selectedFile.readers.Contains(clientName))
                {
                    selectedFile.readers.Remove(clientName);
                }
                var response = protocol.returnReadingPrivilegesResponse(fileName);
                nws.Write(response, 0, response.Length);
            }
            else {
                sendError(nws, "No existe el archivo");
            }
        }

        private static void downloadUpdatedFile(NetworkStream nws, string clientName, byte[] data)
        {
            var response = Encoding.ASCII.GetString(data);
            var fileData = response.Split(new char[] { '|' }, 2);
            byte[] fileText;
            if (fileData.Length > 1)
                fileText = Encoding.ASCII.GetBytes(fileData[1]);
            else
                fileText = new byte[0];
            Files file = serverFiles.Find(f => f.name.Equals(fileData[0]) && f.writer.Equals(clientName));
            if (file != null)
            {
                file.writer = "";
                downloadFile(fileData[0], fileText);
                var serverResponse = protocol.returnUpdatedFileResponse(fileData[0]);
                nws.Write(serverResponse, 0, serverResponse.Length);
            }
            else {
                sendError(nws,"No tienes permiso para editar");
            }

           
        }

        private static void createFile(NetworkStream nws, byte[] data)
        {
            var response = Encoding.ASCII.GetString(data);
            var fileData = response.Split(new char[] { '|' }, 2);
            byte[] fileText;
            if (fileData.Length > 1)
                fileText = Encoding.ASCII.GetBytes(fileData[1]);
            else
                fileText = new byte[0];
            string[] files = GetFileNames(startupPath);

            if (files.Contains(fileData[0])) {
                var fileNameData = fileData[0].Split(new char[] { '.' }, 2);
                var fileName = fileNameData[0];
                var fileExtension = fileNameData[1];
                fileData[0] = fileName + "_copy." + fileExtension;
            }
            try
            {
                downloadNewFile(fileData[0], fileText);
            }catch(ArgumentException ex)
            {
                sendError(nws, ex.Message);
            }
            var serverResponse = protocol.createFileOkResponse("Archivo Creado");
            nws.Write(serverResponse, 0, serverResponse.Length);

        }

        private static void deleteFile(NetworkStream nws , byte[] data)
        {
            //dejar borrar si alguno lo esta leyendo/escribiendo?
            //si lo dejo, que hacer con los permisos otorgados?
            var fileName = Encoding.ASCII.GetString(data);
            if (File.Exists(startupPath + "\\" + fileName))
            {
                File.Delete(startupPath + "\\" + fileName);
                var serverResponse = protocol.createDeleteFileOkResponse("Archivo Borrado");
                nws.Write(serverResponse, 0, serverResponse.Length);
            }
            else {
                sendError(nws, "No existe el archivo");
            }
            
        }

        private static void changeFileName(NetworkStream nws,byte[] data)
        {
            var fileName = Encoding.ASCII.GetString(data);

            try
            {

                var oldName = fileName.Split(new char[] { '|' }, 2)[0];
                var newName = fileName.Split(new char[] { '|' }, 2)[1];

                if (File.Exists(startupPath + "\\" + oldName))
                {
                    var serverResponse = protocol.createChangeFileNameOkResponse("Nombre cambiado");
                    nws.Write(serverResponse, 0, serverResponse.Length);
                    File.Move(startupPath + "\\" + oldName, startupPath + "\\" + newName);
                }
                else
                {
                    sendError(nws, "No existe el archivo que quieres modificar");
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                sendError(nws, "Error de formato");
            }

        }
        private static void downloadFile(string name,byte[] fileData)
        {

            if (File.Exists(startupPath + "\\" + name))
            {
                File.Delete(startupPath + "\\" + name);
            }
            downloadNewFile(name, fileData);
        }
        private static void downloadNewFile(string name, byte[] fileData)
        {
            FileStream fs = new FileStream(startupPath + "\\" + name, FileMode.Append, FileAccess.Write);
            fs.Write(fileData, 0, fileData.Length);
            fs.Close();

        }
        private static void sendError(NetworkStream nws,string message) {
            byte[] errorData = protocol.serverErrorMessage(message);
            nws.Write(errorData, 0, errorData.Length);
        }
        public static string GetLocalIPAddress()
        {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("Local IP Address Not Found!");
        }
    }
}
