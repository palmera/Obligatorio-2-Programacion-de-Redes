using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.LoggerHelper
{
    public class LoggerReceiver
    {
        private MessageQueue messageQue;
        public  void StartMessageQue()
        {
            string queName = ConfigurationManager.AppSettings["messageQueueName"];

            if (!MessageQueue.Exists(queName))
                messageQue = MessageQueue.Create(queName, true);
            else
                messageQue = new MessageQueue(queName);
            BinaryMessageFormatter bmf = new BinaryMessageFormatter();
            messageQue.Formatter = bmf;
        }
        public void Receive()
        {
            while (true)
            {
                try
                {
                    Message message = messageQue.Receive();
                    if (message != null)
                    {
                        string logLine = message.Body.ToString();
                        LoggerPersistance loggerPersistance = LoggerPersistance.getInstance();
                        loggerPersistance.AddLine(logLine);
                    }
                }
                catch (Exception)
                {
                }
                
            }
            
        }
    }
}
