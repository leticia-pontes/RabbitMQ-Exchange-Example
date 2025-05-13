namespace DespachoApi.Models;

public class Despacho
{
    public int Id { get; set; }
    public Guid PedidoId { get; set; }
    public string Transportadora { get; set; } = string.Empty;
    public DateTime DataDespacho { get; set; }
    public DateTime DataRecebimento { get; set; }
}