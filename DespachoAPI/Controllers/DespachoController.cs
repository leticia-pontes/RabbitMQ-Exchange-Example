using Microsoft.AspNetCore.Mvc;
using DespachoApi.Data;
using DespachoApi.Models;

namespace DespachoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DespachoController : ControllerBase
{
    private readonly AppDbContext _context;

    public DespachoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Despacho pedido)
    {
        if (pedido == null)
            return BadRequest();

        pedido.DataRecebimento = DateTime.Now;
        _context.Despachos.Add(pedido);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Post), new { id = pedido.Id }, pedido);
    }
}