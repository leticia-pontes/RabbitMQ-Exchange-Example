# RabbitMQ Exchange Example

Este projeto demonstra a utilização do RabbitMQ para gerenciar filas e exchanges em uma aplicação distribuída. Ele inclui um Publisher, dois Subscribers (Despacho e Pedido), e duas APIs REST (Despacho e Pedido) para simulação de um fluxo de pedidos.

## Tecnologias e Ferramentas
- C#
- RabbitMQ
- ASP.NET Core
- SQL Server

### Bibliotecas
- DotNetEnv
- Microsoft.EntityFrameworkCore
- RabbitMQ.Client

## Pré-requisitos
- RabbitMQ configurado (local ou cloud)
- .NET 8
- SQL Server

## Instalação

1. Clona o repositório
```bash
git clone https://github.com/leticia-pontes/RabbitMQ-Exchange-Example
```

2. Acessa o projeto
```bash
cd RabbitMQ-Exchange-Example
```

3. Instala as dependências
```bash
dotnet restore
```
Obs.: os projetos estão incluídos no arquivo `.sln` da solução. Caso o `dotnet restore` não funcione, tente executar manualmente para cada projeto.

Exemplo:
```bash
dotnet restore Publisher/Publisher.csproj
```

## Configuração

### Banco de dados
As APIs (DespachoAPI e PedidoAPI) foram configuradas para rodar com SQL Server. Não é preciso incluir usuário e senha pois está usando TrustedConnection.

### .env
Nos projetos `Publisher`, `DespachoSubscriber` e `PedidoSubscriber`, na mesma pasta dos arquivos `Program.cs`, crie um arquivo `.env`. Ele deve conter a URI de acesso do servidor do RabbitMQ.
A palavra-chave da URI é `RABBITMQ_URI`.

Formato do `.env`:
```env
RABBITMQ_URI=amqps://usuario:senha@servidor.rmq.cloudamqp.com/vhost
```

## Execução

Ordem correta de execução:

1. APIs (rodar em terminais diferentes)
```bash
# API de Pedido
dotnet run --project PedidoAPI/PedidoAPI.csproj
```

```bash
# API de Despacho
dotnet run --project DespachoAPI/DespachoAPI.csproj
```

2. Subscribers (rodar em terminais diferentes)
```bash
# Subscriber do Pedido
dotnet run --project PedidoSubscriber/PedidoSubscriber.csproj
```

```bash
# Subscriber do Despacho
dotnet run --project DespachoSubscriber/DespachoSubscriber.csproj
```

3. Publisher
```bash
dotnet run --project Publisher/Publisher.csproj
```
