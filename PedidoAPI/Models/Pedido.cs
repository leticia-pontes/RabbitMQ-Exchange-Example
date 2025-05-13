namespace PedidoApi.Models;

public class Pedido
{
    public int Id { get; set; }
    public Guid PedidoId { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string[] Itens { get; set; } = [];
    public double ValorTotal { get; set; }
    public DateTime DataRecebimento { get; set; }
}