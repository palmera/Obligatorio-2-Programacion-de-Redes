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
        public static void Log(string log)
        {
            string logqueName = ConfigurationManager.AppSettings["messageQueueRemoteName"];//".\\private$\\Logs";
            MessageQueue que = new MessageQueue(logqueName);
            BinaryMessageFormatter bmf = new BinaryMessageFormatter();
            que.Formatter = bmf;
            que.Send(log);

            //    System.Messaging.Message msg = new System.Messaging.Message();
            //    msg.Label = "Log";
            //    msg.Body = log;
            //    msg.UseDeadLetterQueue = true;
            //    MessageQueue msgQ = new MessageQueue(".\\private$\\Logs");
            //    if (!MessageQueue.Exists(msgQ.Path))
            //    {
            //        MessageQueue.Create(msgQ.Path);
            //    }
            //    msgQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });
            //    msgQ.ReceiveCompleted += new ReceiveCompletedEventHandler(ReceiveCompleted);
            //    msgQ.Send(msg);
            //    msgQ.BeginReceive();
            //    msgQ.Close();
            //}
            //private static void ReceiveCompleted(Object source,
            //      ReceiveCompletedEventArgs asyncResult)
            //{
            //    MessageQueue mq = (MessageQueue)source;
            //    Message m = mq.EndReceive(asyncResult.AsyncResult);

            //    mq.BeginReceive();
            //    return;
            //}
        }

    }
}
