using Microsoft.AspNetCore.Mvc;
using OrderManager.Application.DTOs;
using OrderManager.Application.Interfaces;

namespace OrderManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController(IPedidoAppService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PedidoRequest request)
    {
        var result = await service.ProcessarAsync(request);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await service.ObterPorIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetByStatus([FromQuery] string status) => Ok(await service.ListarPorStatusAsync(status));
}