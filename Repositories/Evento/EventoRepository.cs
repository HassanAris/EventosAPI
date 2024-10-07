using EventosAPI.Models;
using Microsoft.EntityFrameworkCore;

public class EventoRepository : IEventoRepository
{
    private readonly ApplicationDbContext _context;

    public EventoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TbEvento> CriarEvento(TbEvento evento)
    {
        _context.tbEvento.Add(evento);
        await _context.SaveChangesAsync();
        return evento;
    }

    public async Task<IEnumerable<TbEvento>> ObterEventos()
    {
        return await _context.tbEvento.ToListAsync();
    }

    public async Task<TbEvento> ObterEventoPorId(int id)
    {
        return await _context.tbEvento.FindAsync(id);
    }

    public async Task AtualizarEvento(TbEvento evento)
    {
        _context.tbEvento.Update(evento);
        await _context.SaveChangesAsync();
    }

    public async Task DeletarEvento(int id)
    {
        var evento = await _context.tbEvento.FindAsync(id);
        if (evento != null)
        {
            _context.tbEvento.Remove(evento);
            await _context.SaveChangesAsync();
        }
    }
}
