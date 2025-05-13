using Microsoft.AspNetCore.Mvc;
using PedidoApi.Data;
using PedidoApi.Models;

namespace PedidoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PedidoController : ControllerBase
{
    private readonly AppDbContext _context;

    public PedidoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Pedido pedido)
    {
        if (pedido == null)
            return BadRequest();

        pedido.DataRecebimento = DateTime.Now;
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Post), new { id = pedido.Id }, pedido);
    }
}