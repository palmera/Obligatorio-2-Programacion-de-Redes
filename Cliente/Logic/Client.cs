﻿using Cliente.Exceptions;
using Cliente.Files;
using Protocols;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Logic
{
    public class Client
    {
        public  TcpClient tcpClient { get; set; }
        public  Protocol protocol { get; set; }

        public List<ClientFile> myFiles { get; set; }
        private static string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Archivos";


        public Client ()
        {
            protocol = new Protocol();
            myFiles = new List<ClientFile>();
        }

        public  bool connect(string name,string serverIP,int port)
        {

            if (makeConnection(name, serverIP, port)) {
                return true;
            }return false;
        }

        private  bool makeConnection(string name,string serverIP, int port)
        {
             tcpClient = new TcpClient(serverIP, port);
            return authenticateClient(tcpClient, name);

        }
        private  bool authenticateClient(TcpClient client, string name)
        {
            NetworkStream nws = client.GetStream();
            byte[] data = protocol.makeAuthorizationHeader(name);
            nws.Write(data, 0, data.Length);
            var response = new byte[12];
            response = protocol.receiveData(client, 12);
            if (response != null)
                return protocol.checkIfLogged(response);
            return false;
        }

        public void listFiles()
        {
            NetworkStream nws = tcpClient.GetStream();
            byte[] data = protocol.makeFileListRequestHeader();
            nws.Write(data, 0, data.Length);
            receiveData();

        }

        private  void receiveData()
        {
            try
            {
                byte[] header = new byte[9];
                header = protocol.receiveData(tcpClient, 9);
                if (header != null)
                    executeAction(header);
            }
            catch (SocketException ex) {
                throw new ClientException(ex.Message);
            }
        }

        private  void executeAction(byte[] header)
        {
            NetworkStream nws = tcpClient.GetStream();
            int largoDatos = protocol.dataLength(header);
            byte[] data = new byte[largoDatos];
            data = protocol.receiveData(tcpClient, largoDatos);
            int action = protocol.getAction(header);
            switch (action)
            {
                case 11: //servidor devuelve listado de archivos
                    Console.WriteLine(Encoding.ASCII.GetString(data));
                    break;
                case 21://servidor devuelve un archivo para lectura
                    downloadEditableFile(data);
                    break;
                case 31:
                    downloadReadableFile(data);
                    break;
                case 41:
                    removeReadingFile(data);
                    break;
                case 51:
                    removeEditableFile(data);
                    break;
                case 99:
                    serverError(data);
                    break;

                default:
                    string hola = System.Text.Encoding.ASCII.GetString(header);
                    Console.WriteLine("cliente no conectado es: " + hola);
                    break;
            }
        }

        public void downloadFileForEdit(string filename)
        {
            NetworkStream nws = tcpClient.GetStream();
            var data = protocol.makeDownloadFileForEditHeader(filename);
            nws.Write(data, 0, data.Length);
            receiveData();
        }

        private void downloadEditableFile(byte[] data)
        {
            ClientFile clientFile = downloadFile(data);
            clientFile.editable = true;
            if (myFiles.Contains(clientFile))
            {
                int index = myFiles.FindIndex(c => c.name.Equals(clientFile.name));
                myFiles[index].path = clientFile.path;
                myFiles[index].readable = clientFile.editable;
            }
            else
            {
                myFiles.Add(clientFile);
            }
        }
        public void downloadFileForReading(string filename)
        {
            NetworkStream nws = tcpClient.GetStream();
            var data = protocol.makeDownloadFileForReadingHeader(filename);
            nws.Write(data, 0, data.Length);
            receiveData();
        }

        private void downloadReadableFile(byte[] data)
        {
            ClientFile clientFile = downloadFile(data);
            clientFile.readable = true;
            if (myFiles.Contains(clientFile))
            {
                int index = myFiles.FindIndex(c => c.name.Equals(clientFile.name));
                myFiles[index].path = clientFile.path;
                myFiles[index].readable = clientFile.readable;
            }
            else
            {
                myFiles.Add(clientFile);
            }
        }
        private ClientFile downloadFile(byte[] data)
        {
            var response = Encoding.ASCII.GetString(data);
            var fileData = response.Split(new char[] { '|' }, 2);
            byte[] fileText;
            if (fileData.Length > 1)
                fileText = Encoding.ASCII.GetBytes(fileData[1]);
            else
                fileText = new byte[0];
            if (File.Exists(startupPath + "\\" + fileData[0]))
            {
                File.Delete(startupPath+"\\" + fileData[0]);
            }
            FileStream fs = new FileStream(startupPath+"\\" + fileData[0], FileMode.Append, FileAccess.Write);
            fs.Write(fileText, 0, fileText.Length);
            ClientFile file = new ClientFile(fileData[0], startupPath + "\\" + fileData[0]);
            fs.Close();
            return file;
        }


        public void returnReadingPrivileges(string filename)
        {
            NetworkStream nws = tcpClient.GetStream();
            var data = protocol.makeReturnReadingPrivilegesHeader(filename);
            nws.Write(data,0,data.Length);
            receiveData();

        }

        private void removeReadingFile(byte[] data)
        {
            var filename = Encoding.ASCII.GetString(data);
            ClientFile clientFile = myFiles.Find(c=>c.name.Equals(filename) && c.readable);
            if (clientFile != null) {
                if (myFiles.Contains(clientFile)) {
                    if (!clientFile.editable)
                        myFiles.Remove(clientFile);
                    else
                        clientFile.readable = false;
                }
            }
        }

        private void removeEditableFile(byte[] data)
        {
            var filename = Encoding.ASCII.GetString(data);
            ClientFile clientFile = myFiles.Find(c => c.name.Equals(filename) && c.editable);
            if (clientFile != null)
            {
                if (myFiles.Contains(clientFile))
                {
                    if (!clientFile.readable)
                        myFiles.Remove(clientFile);
                    else
                        clientFile.editable = false;
                }
            }
        }

        public void returnUpdatedFile(string filename)
        {
            ClientFile clientFile = myFiles.Find(c => c.name.Equals(filename) && c.editable);
            if (clientFile == null)
                clientFile = new ClientFile(filename,startupPath+"\\"+filename);
            sendFileToServer(clientFile);
            receiveData();

        }

        private void sendFileToServer(ClientFile clientFile)
        {
            try
            {
                NetworkStream nws = tcpClient.GetStream();
                FileStream fs;
                fs = new FileStream(clientFile.path, FileMode.Open, FileAccess.Read);
                byte[] bytesToSend = new byte[fs.Length];

                fs.Read(bytesToSend, 0, bytesToSend.Length);

                byte[] package = protocol.makeReturnUpdatedFileHeader(System.Text.Encoding.ASCII.GetString(bytesToSend), clientFile.name);
                nws.Write(package, 0, package.Length);

                fs.Close();
            }catch(Exception ex)
            {
                throw new ClientException(ex.Message);
            }
        }
        private void serverError(byte[] data)
        {
            throw new ClientException(Encoding.ASCII.GetString(data));
        }
    }
}