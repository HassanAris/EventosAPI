using EventosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EventosAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TbUsuario> GetUsuarioByEmail(string email)
        {

            return await _context.tbUsuario.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<TbUsuario> GetUsuarioById(int id)
        {

            return await _context.tbUsuario.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task AddUsuario(TbUsuario usuario)
        {
            _context.tbUsuario.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.tbUsuario.AnyAsync(u => u.Email == email);
        }
    }
}
