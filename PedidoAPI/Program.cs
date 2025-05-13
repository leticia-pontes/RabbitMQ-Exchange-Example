using PedidoApi.Data;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Recupera a string de conexão do appsettings
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        // Configura o contexto para usar SQL Server
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers();

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API Pedido v1");
                options.RoutePrefix = "swagger";
            });
        }

        app.UseRouting();
        app.MapControllers();
        app.Run();
    }
}