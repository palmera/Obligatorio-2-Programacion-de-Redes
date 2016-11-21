
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.LoggerHelper
{
    public class LoggerReceiver
    {
        private MessageQueue queue;
        public void CreateQueue()
        {
            string messageQueuePath = ".\\private$\\Logs";
            if (!MessageQueue.Exists(messageQueuePath))
            {
                queue = MessageQueue.Create(messageQueuePath);
            }
            else
            {
                queue = new MessageQueue(messageQueuePath);
            }
            BackgroundWorker work =null;
            BinaryMessageFormatter formatter = new BinaryMessageFormatter();
            queue.Formatter = formatter;
            work = new BackgroundWorker();
            work.DoWork += logQueWork;
            work.RunWorkerAsync();
            
        }
        private void logQueWork(object sender,DoWorkEventArgs e)
        {
            while (true)
            {
                Message message = queue.Receive();
                if(message != null)
                {
                    string logLine = message.Body.ToString();
                    LoggerPersistance.getInstance().AddLine(logLine);
                }
            }
        }

    }
}
