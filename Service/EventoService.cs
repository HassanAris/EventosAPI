using EventosAPI.Models;

public class EventoService
{
    private readonly IEventoRepository _eventoRepository;

    public EventoService(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }

    public async Task<TbEvento> CriarEvento(TbEvento evento)
    {
        return await _eventoRepository.CriarEvento(evento);
    }

    public async Task<IEnumerable<TbEvento>> ObterEventos()
    {
        return await _eventoRepository.ObterEventos();
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
}
