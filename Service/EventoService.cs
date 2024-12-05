using EventosAPI.DTOs;
using EventosAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class EventoService
{
    private readonly IEventoRepository _eventoRepository;

    public EventoService(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }

    public async Task<bool> CriarEvento(CriarEventoDTO dto, int userId)
    {
        var resultado = await _eventoRepository.CriarEvento(dto, userId);
        return resultado;
    }

    public async Task<bool> EditarEvento(CriarEventoDTO dto, int userId)
    {
        var resultado = await _eventoRepository.EditarEvento(dto, userId);
        return resultado;
    }

    public async Task<IEnumerable<TbEvento>> ObterEventos()
    {
        return await _eventoRepository.ObterEventos();
    }

    public async Task<List<TbEvento>> ListarEventosPorUsuario(int usuarioId)
    {
        return await _eventoRepository.ListarEventosPorUsuario(usuarioId);
    }

    public async Task<TbEvento> ObterEventoPorId(int id)
    {
        return await _eventoRepository.ObterEventoPorId(id);
    }

    public async Task AtualizarEvento(TbEvento evento)
    {
        await _eventoRepository.AtualizarEvento(evento);
    }

    public async Task DeletarEvento(int id)
    {
        await _eventoRepository.DeletarEvento(id);
    }

    public async Task<bool> AceitarOuRecusarEvento(int eventoId, int usuarioId, string status)
    {
       return await _eventoRepository.AceitarOuRecusarEvento(eventoId, usuarioId, status);
    }
}
