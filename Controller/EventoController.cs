using EventosAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly EventoService _eventoService;

    public EventosController(EventoService eventoService)
    {
        _eventoService = eventoService;
    }


    [HttpPost("CriarEvento")]
    public async Task<IActionResult> CriarEvento([FromBody] TbEvento evento)
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)); // Pega o ID do usuário logado
        evento.OrganizadorId = userId;
        var novoEvento = await _eventoService.CriarEvento(evento);
        return CreatedAtAction(nameof(ObterEventoPorId), new { id = novoEvento.Id }, novoEvento);
    }

    [HttpGet("ObterEventos")]
    public async Task<IActionResult> ObterEventos()
    {
        var eventos = await _eventoService.ObterEventos();
        return Ok(eventos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterEventoPorId(int id)
    {
        var evento = await _eventoService.ObterEventoPorId(id);
        if (evento == null) return NotFound();
        return Ok(evento);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarEvento(int id, [FromBody] TbEvento evento)
    {
        if (id != evento.Id) return BadRequest();
        await _eventoService.AtualizarEvento(evento);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarEvento(int id)
    {
        await _eventoService.DeletarEvento(id);
        return NoContent();
    }
}
