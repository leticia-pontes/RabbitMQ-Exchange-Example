using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using DotNetEnv;

// Carrega variáveis de ambiente do .env
Env.Load();

var factory = new ConnectionFactory
{
    Uri = new Uri(Environment.GetEnvironmentVariable("RABBITMQ_URI")!)
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

// Nome da exchange
string exchangeName = "pedidos_exchange";

// Declara a exchange do tipo fanout
channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);

// Exemplo de pedido em JSON
var pedido = new
{
    PedidoId = Guid.NewGuid(),
    Cliente = "João da Silva",
    Itens = new[] { "Camisa", "Calça" },
    ValorTotal = 199.90
};

// Serializa para JSON e envia
string message = JsonSerializer.Serialize(pedido);
var body = Encoding.UTF8.GetBytes(message);

// Publica a mensagem na exchange
channel.BasicPublish(exchange: exchangeName, routingKey: "", basicProperties: null, body: body);

Console.WriteLine("✔️ Pedido enviado para a Exchange:");
Console.WriteLine(message);