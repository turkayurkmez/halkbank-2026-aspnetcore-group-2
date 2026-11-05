using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.SOLID
{
    public record Order(int Id, string Email, decimal Amount);
   public class OrderProcessorBefore
    {
        public bool IsValid(Order order)
        {
            return order.Amount > 0;
        }

        public decimal DiscountedPrice(Order order) => order.Amount >= 1000 ? order.Amount * 0.9m : order.Amount;

        public void Register(Order order) => Console.WriteLine($"Sipariş {order.Id} kaydedildi: {DiscountedPrice(order)} TL ");

        public void SendEmail(Order order) => Console.WriteLine($"[Mail] {order.Email} siparişiniz alındı");

        public void Process(Order order)
        {
            if (IsValid(order))
            {
                var price = DiscountedPrice(order);
                Register(order);
                SendEmail(order);
            }
        }
    }

    public class OrderValidator
    {
        public bool IsValid(Order order)
        {
            return order.Amount > 0;
        }
    } 

    public class PriceCaluculator
    {
        public decimal DiscountedPrice(Order order) => order.Amount >= 1000 ? order.Amount * 0.9m : order.Amount;
    }

    public class OrderRepository
    {
        public void Register(Order order, decimal price) => Console.WriteLine($"Sipariş {order.Id} kaydedildi: {price} TL ");
    }

    public class MailSender
    {
        public void SendEmail(string email, string body) => Console.WriteLine($"[Mail] {email} {body}");
    }


    public class OrderProcessorAfter
    {
        //SRP: Her sınıf tek bir sorumluluğa sahip olmalı. Bir sınıfta değişiklik yapmak için birden fazla sebebiniz varsa prensibi ihlal ediyorsunuz demektir.
        private OrderValidator validator = new();
        private PriceCaluculator caluculator = new();
        private OrderRepository repository = new();
        private MailSender mailSender = new MailSender();

        public void Process(Order order)
        {
            if (validator.IsValid(order))
            {
                var total = caluculator.DiscountedPrice(order);
                repository.Register(order, total);
                mailSender.SendEmail(order.Email,"Siparişiniz alındı");
            }
        }

    }

}
