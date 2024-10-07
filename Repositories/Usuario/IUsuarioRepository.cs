using EventosAPI.Models;
using System.Threading.Tasks;

namespace EventosAPI.Repositories
{
    public interface IUsuarioRepository
    {
        Task<TbUsuario> GetUsuarioByEmail(string email);
        Task AddUsuario(TbUsuario usuario);
        Task<bool> EmailExists(string email);
        Task<TbUsuario> GetUsuarioById(int id); 
    }
}
