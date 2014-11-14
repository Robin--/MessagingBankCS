using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MessagingBankCS.Boundary
{
    class RabbitMQ
    {
        private Control.Controller controller;

        public RabbitMQ()
        {
            controller = new Control.Controller();
        }

        public void HandleMessages()
        {
            var factory = new ConnectionFactory() { HostName = "datdb.cphbusiness.dk" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("rh11_rpc_queue", false, false, false, null);
                    channel.BasicQos(0, 1, false);
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume("rh11_rpc_queue", false, consumer);
                    Console.WriteLine(" [X] Awaiting RPC requests");

                    while (true)
                    {
                        string loanResponseJSON = null;
                        var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                        var body = ea.Body;
                        var props = ea.BasicProperties;
                        var replyProps = channel.CreateBasicProperties();
                        replyProps.CorrelationId = props.CorrelationId;

                        try
                        {
                            var loanRequestJSON = Encoding.UTF8.GetString(body);
                            Console.WriteLine(" [.] Received loan request JSON: {0}", loanRequestJSON);
                            loanResponseJSON = controller.CalculateLoanResponse(loanRequestJSON);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(" [.] " + e.Message);
                            loanResponseJSON = "";
                        }
                        finally
                        {
                            var responseBytes = Encoding.UTF8.GetBytes(loanResponseJSON);
                            channel.BasicPublish("", props.ReplyTo, replyProps, responseBytes);
                            channel.BasicAck(ea.DeliveryTag, false);
                        }
                    }
                }
            }
        }
    }
}
