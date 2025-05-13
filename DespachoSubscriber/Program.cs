using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using DotNetEnv;
using System.Net.Http;

Env.Load();

var factory = new ConnectionFactory
{
    Uri = new Uri(Environment.GetEnvironmentVariable("RABBITMQ_URI")!)
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

string exchangeName = "pedidos_exchange";
string queueName = "fila_despacho";

// Declara exchange do tipo fanout
channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);

// Declara a fila
channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

// Vincula fila à exchange
channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: "");

// Configura o consumer
var consumer = new EventingBasicConsumer(channel);
consumer.Received += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"🚚 Despacho recebido: {message}");

    using var httpClient = new HttpClient();
    var url = "http://localhost:5287/api/despacho";

    var content = new StringContent(message, Encoding.UTF8, "application/json");
    var response = await httpClient.PostAsync(url, content);

    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("✅ Despacho enviado para API com sucesso.");
    }
    else
    {
        Console.WriteLine("❌ Falha ao enviar despacho para API.");
    }
};

channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

Console.WriteLine("🚀 Aguardando despachos... Pressione [Enter] para sair.");
Console.ReadLine();