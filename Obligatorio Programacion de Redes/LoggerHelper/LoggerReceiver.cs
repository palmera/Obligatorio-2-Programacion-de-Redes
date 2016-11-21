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
            messageQue = new MessageQueue(queName);
        }
        public void Receive()
        {
            Message message = messageQue.Receive();
            if (message != null)
            {
                string logLine = message.Body.ToString();
                LoggerPersistance loggerPersistance = LoggerPersistance.getInstance();
                loggerPersistance.AddLine(logLine);
            }
        }
    }
}
