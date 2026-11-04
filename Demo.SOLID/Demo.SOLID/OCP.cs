using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.SOLID
{
    public enum NotificationTypes
    {
        Email,
        SMS,
        Telegram
    }

    public class NotificationSenderBefore
    {
        public void Send(NotificationTypes type, string to, string message)
        {
            switch (type)
            {
                case NotificationTypes.Email:
                    Console.WriteLine("Eposta gönderildi");
                    break;
                case NotificationTypes.SMS:
                    Console.WriteLine("SMS gönderildi");
                    break;
                case NotificationTypes.Telegram:
                    break;
                default:
                    break;
            }
        }
    }

    public interface INotifier
    {
       void Send(string to,  string message);
    }

    public class EMailNotifier : INotifier
    {
        public void Send(string to, string message)
        {
            Console.WriteLine("Email gönderildi");
        }
    }

    public class SMSNotifier : INotifier
    {
        public void Send(string to, string message)
        {
            Console.WriteLine("SMS gönderildi");
        }
    }

    public class PushNotifier : INotifier
    {
        public void Send(string to, string message)
        {
            Console.WriteLine("Push gönderildi");
        }
    }

    public class NotificationSenderAfter
    {
        public void Send(INotifier notifier, string to, string message) 
        {
            notifier.Send(to, message);
        }
    }
}
