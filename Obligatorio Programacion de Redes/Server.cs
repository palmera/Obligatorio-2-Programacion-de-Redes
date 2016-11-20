﻿using Obligatorio_Programacion_de_Redes.Files;
using Protocols;
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
        private static List<String> clientesPermitidos;
        private static List<Files> serverFiles;
        private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Archivos";
        private static Object lock_object = new object();

        static void Main(string[] args)
        {
            Thread remotingThread = new Thread(() => RemotingAdminServer.StartAdminRemoting());
            remotingThread.Start();
            RemotingAdminServer.StartUserRemoting();
            //RemotingAdminServer.StartAdminRemoting();

            serverFiles = new List<Files>();
            string[] filesPath = Directory.GetFiles(startupPath);
            string[] fileNames = GetFileNames(startupPath);
            for (int i = 0; i<fileNames.Length; i++)
            {
                Files f = new Files();
                f.path = filesPath[i];
                f.name = fileNames[i];
                
                serverFiles.Add(f);
            }
            Console.WriteLine(buildFileListing(serverFiles));

            cargarClientesPermitidos();
            protocol = new Protocol();
            clientes = new List<TcpClient>();
            aceptarConecciones();
        }
        private static string[] GetFileNames(string path)
        {
            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
                files[i] = Path.GetFileName(files[i]);
            return files;
        }

        private static void cargarClientesPermitidos()
        {
            clientesPermitidos = new List<string>();
            clientesPermitidos.Add("Juan");
            clientesPermitidos.Add("Agus");
            clientesPermitidos.Add("Pepe");
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
                    byte[] header = new byte[9];

                    header = protocol.receiveData(clientConnected, 9);
                    if (header != null)
                        executeAction(header, clientConnected, ref nombreUsuario);
                }catch(IOException) { }
            }
        }

        private static void sendDataToClient(string data, NetworkStream nws)
        {
            byte[] a = protocol.authenticationResponse(clientesPermitidos.Contains(data));
            string b = Encoding.ASCII.GetString(a);
            nws.Write(protocol.authenticationResponse(clientesPermitidos.Contains(data)), 0, a.Length);
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


                    sendDataToClient(name, nws);
                    /*byte[] a = protocol.authenticationResponse(clientesPermitidos.Contains(name));
                    string b = Encoding.ASCII.GetString(a);
                    nws.Write(protocol.authenticationResponse(clientesPermitidos.Contains(name)), 0, a.Length);*/

                    var response = new byte[12];


                    clientName = System.Text.Encoding.ASCII.GetString(data);
                    Console.WriteLine("cliente conectado es: "+ clientName);
                    //connection.Send(protocol.ArmarTramaRespuestaAutenticacionCliente(UsuarioValido(nombreUsuario)));
                    break;
                case 10: //cliente pide listado de archivos
                    string[] files = Directory.GetFiles(startupPath);

                   // sendDataToClient(buildFileListing(files), nws);
                    byte[] a = protocol.headerSendListing(buildFileListing(serverFiles));
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
        private static void downloadFile(string name,byte[] fileData)
        {

            if (File.Exists(startupPath + "\\" + name))
            {
                File.Delete(startupPath + "\\" + name);
            }
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
