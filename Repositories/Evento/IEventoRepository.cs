using EventosAPI.DTOs;
using EventosAPI.Models;

public interface IEventoRepository
{
    Task<bool> CriarEvento(CriarEventoDTO dto, int userId);
    Task<bool> EditarEvento(CriarEventoDTO dto, int userId);
    Task<IEnumerable<TbEvento>> ObterEventos();
    Task<TbEvento> ObterEventoPorId(int id);
    Task AtualizarEvento(TbEvento evento);
    Task<bool> DeletarEvento(int id);
    Task<bool> AceitarOuRecusarEvento(int eventoId, int usuarioId, string status);
    Task<List<TbEvento>> ListarEventosPorUsuario(int usuarioId);
    Task<IEnumerable<TbEvento>> ObterEventosInativo();
}
