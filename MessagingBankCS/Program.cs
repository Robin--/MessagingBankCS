using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MessagingBankCS
{
    class Program
    {
        static void Main(string[] args)
        {
            Boundary.RabbitMQ rabbit = new Boundary.RabbitMQ();
            rabbit.HandleMessages();
        }
    }
}
