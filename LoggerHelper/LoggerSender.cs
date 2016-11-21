using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace LoggerHelper
{
    public class LoggerSender
    {
        public static void sendLog(string message)
        {
            string logQueueName = ConfigurationManager.AppSettings["messageQueueName"];
            MessageQueue queue = new MessageQueue(logQueueName);
            BinaryMessageFormatter formatter = new BinaryMessageFormatter();
            queue.Formatter = formatter;
            queue.Send(message);
        }
    }
}
