using Demo.SOLID;

Console.WriteLine("Hello, World!");

NotificationSenderAfter notificationSenderAfter = new NotificationSenderAfter();
notificationSenderAfter.Send(new SMSNotifier(), "xxx@yy.com", "Deneme");
notificationSenderAfter.Send(new EMailNotifier(), "xxx@yy.com", "Deneme");
notificationSenderAfter.Send(new PushNotifier(), "xxx@yy.com", "Deneme");