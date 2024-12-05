using EventosAPI.DTOs;
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
    public async Task<IActionResult> CriarEvento([FromBody] CriarEventoDTO dto)
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

        if (userId == null)
        {
            return Unauthorized();
        }
        var resultado = await _eventoService.CriarEvento(dto, userId);

        if (resultado)
        {
            return Ok();
        }

        return BadRequest("Erro ao criar evento.");
    }


    [HttpGet("ObterEventos")]
    public async Task<IActionResult> ObterEventos()
    {
        var eventos = await _eventoService.ObterEventos();
        return Ok(eventos);
    }

    [HttpGet("ObterEventosUsuario")]
    public async Task<IActionResult> ObterEventosUsuario(int id)
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var eventos = await _eventoService.ListarEventosPorUsuario(userId);
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


    [HttpPost("EditarEvento")]
    public async Task<IActionResult> EditarEvento([FromBody] CriarEventoDTO dto)
    {
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

        if (userId == null)
        {
            return Unauthorized();
        }
        var resultado = await _eventoService.EditarEvento(dto, userId);

        if (resultado)
        {
            return Ok();
        }

        return BadRequest("Erro ao criar evento.");
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarEvento(int id)
    {
        await _eventoService.DeletarEvento(id);
        return NoContent();
    }

    [HttpPost("AceitarOuRecusarEvento/{id}")]
    public async Task<IActionResult> AceitarOuRecusarEvento(int id, [FromQuery] string status)
    {
        // Garantir que o usuário esteja autenticado e obter o ID do usuário
        var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

        if (userId == 0)
        {
            return Unauthorized(new { message = "Usuário não autenticado." });
        }

        // Verificar o status
        if (string.IsNullOrEmpty(status) || !(status.Equals("Aceito", StringComparison.OrdinalIgnoreCase) || status.Equals("Recusado", StringComparison.OrdinalIgnoreCase)))
        {
            return BadRequest(new { message = "Status inválido. Use 'Aceito' ou 'Recusado'." });
        }

        // Chamar o serviço para aceitar ou recusar o evento
        bool sucesso = await _eventoService.AceitarOuRecusarEvento(id, userId, status);

        if (sucesso)
        {
            return Ok(new { message = "Evento atualizado com sucesso!" });
        }
        else
        {
            return BadRequest(new { message = "Falha ao atualizar o evento." });
        }
    }


}
