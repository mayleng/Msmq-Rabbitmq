using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace rabbitmq
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //send
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.Port = AmqpTcpEndpoint.UseDefaultPort;
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.Protocol = Protocols.DefaultProtocol;

        
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "hello",
                                             durable: false,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

                        string message = "Hello World!";
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "",
                                             routingKey: "hello",
                                             basicProperties: null,
                                             body: body);
                        // Console.WriteLine(" [x] Sent {0}", message);
                    }
                }
            
           

            //receive
            var factory2 = new ConnectionFactory();
            factory2.HostName = "localhost";
            factory2.Port = AmqpTcpEndpoint.UseDefaultPort;
            factory2.UserName = "guest";
            factory2.Password = "guest";
            factory2.Protocol = Protocols.DefaultProtocol;


            using (var connection2 = factory2.CreateConnection())
            {
                using (var channel = connection2.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                    };
                    channel.BasicConsume(queue: "hello",
                                         autoAck: true,
                                         consumer: consumer);


                }
            }
            

           
            






        }
    }
}