using EventosAPI.Models;

public interface IEventoRepository
{
    Task<TbEvento> CriarEvento(TbEvento evento);
    Task<IEnumerable<TbEvento>> ObterEventos();
    Task<TbEvento> ObterEventoPorId(int id);
    Task AtualizarEvento(TbEvento evento);
    Task DeletarEvento(int id);
}
