using System;
using System.Text;
using System.Net.Sockets;

namespace Protocols
{
    public class Protocol
    {
        public byte[] stringToByte(string text)
        {
            return Encoding.ASCII.GetBytes(text);
        }
        public byte[] makeAuthorizationHeader(string name)
        {
            string dataSize = name.Length.ToString();
            string trama = "REQ" + "00" + makeSizeText(dataSize) + name;
            return stringToByte(trama);
        }
        public int dataLength(byte[] dataString)
        {
            string result = System.Text.Encoding.ASCII.GetString(dataString);
            string length = result.Substring(5, 4);
            int largoInt = Int32.Parse(length);
            return largoInt;
        }
        public int getAction(byte[] trama)
        {
            string result = System.Text.Encoding.ASCII.GetString(trama);
            string largo = result.Substring(3, 2);
            int aDevolver = Int32.Parse(largo);
            return aDevolver;
        }
        public string makeSizeText(string size)
        {
            while (size.Length < 4)
            {
                size = "0" + size;
            }
            return size;
        }

        public byte[] receiveData(TcpClient connection, int size)
        {
            try
            {
                int bufferSize = connection.ReceiveBufferSize;
                NetworkStream nws = connection.GetStream();
                byte[] cabezal = new byte[size];
                int bytesReceived = 0;
                int roundData = 0;

                while (bytesReceived < size)
                {
                    try
                    {
                        roundData = nws.Read(cabezal, bytesReceived, size - bytesReceived);
                        bytesReceived += roundData;
                        if (roundData == 0)
                        {
                            connection.Close();
                            cabezal = null;
                        }
                    }
                    catch (ObjectDisposedException)
                    {
                        connection.Close();
                        return null;
                    }
                }
                return cabezal;
            }
            catch (InvalidOperationException)
            {
                connection.Close();
                return null;

            }
        }
        public int checkIfLogged(byte[] response)
        {
            string responseText = Encoding.ASCII.GetString(response);
            string result = responseText.Substring(9, 3);
            if (result.Equals("200"))
            {
                Console.WriteLine("connected as admin");
                return 1;
            }
            else if (result.Equals("201")) {
                Console.WriteLine("connected as user");
                return 2;
            }
            else
            {
                Console.WriteLine("not connected");
                return 3;
            }
        }

        //Request del cliente
        public byte[] makeFileListRequestHeader()
        {
            string trama = "REQ" + "10" + makeSizeText("0");
            return stringToByte(trama);
        }

        public byte[] makeDownloadFileForEditHeader(string filename)
        {
            string trama = "REQ" + "20" + makeSizeText(filename.Length+"")+ filename;
            return stringToByte(trama);
        }
        public byte[] makeDownloadFileForReadingHeader(string filename)
        {
            string trama = "REQ" + "30" + makeSizeText(filename.Length + "") + filename;
            return stringToByte(trama);
        }
        public byte[] makeReturnReadingPrivilegesHeader(string filename)
        {
            string trama = "REQ" + "40" + makeSizeText(filename.Length + "") + filename;
            return stringToByte(trama);
        }
        public byte[] makeReturnUpdatedFileHeader(string data,string filename)
        {
            string trama = "REQ" + "50" + makeSizeText((filename.Length + data.Length + 1)+"") +filename+"|"+ data;
            return stringToByte(trama);
        }
        public byte[] authenticationResponse(int isAuthentified)
        {
            string response;
            switch (isAuthentified)
            {
                case 1:
                    response = "RES" + "00" + makeSizeText("3") + "200";
                    return stringToByte(response);
                case 2:
                    response = "RES" + "00" + makeSizeText("3") + "201";
                    return stringToByte(response);
                case 3:
                    response = "RES" + "00" + makeSizeText("3") + "400";
                    return stringToByte(response);
                default:
                    response = "RES" + "00" + makeSizeText("3") + "400";
                    return stringToByte(response);
            }

        }
        public byte[] headerSendListing(string data)
        {
            string response;
            response = "RES" + "11" + makeSizeText(data.Length + "") + data;
            return stringToByte(response);

        }

        public byte[] headerSendEditingFile(string data, string fileName)
        {
            string response;
            response = "RES" + "21" + makeSizeText((data.Length + fileName.Length +1) + "") + fileName + "|" + data;
            return stringToByte(response);

        }
        public byte[] headerSendReadingFile(string data,string fileName)
        {
            string response;
            response = "RES" + "31" + makeSizeText((data.Length + fileName.Length + 1) + "") + fileName + "|" + data;
            return stringToByte(response);
        }

        public byte[] returnReadingPrivilegesResponse(string response)
        {
            var responseData = "RES" + "41" + makeSizeText(response.Length+"") + response;
            return stringToByte(responseData);
        }
        public byte[] returnUpdatedFileResponse(string response)
        {
            var responseData = "RES" + "51" + makeSizeText(response.Length + "") + response;
            return stringToByte(responseData);
        }
        public byte[] serverErrorMessage(string message)
        {
            string response;
            response = "RES" + "99" + makeSizeText(message.Length + "") + message;
            return stringToByte(response);
        }
        public byte[] createFileOkResponse(string response) {
            response = "RES" + "61" + makeSizeText(response.Length + "") + response;
            return stringToByte(response);
        }
    }
}


