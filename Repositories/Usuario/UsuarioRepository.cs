using EventosAPI.DTOs;
using EventosAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
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

        public async Task<Tuple<EventoDTO, List<UsuarioEventoDTO>>> ListarUsuariosPorEvento(int eventoId)
        {
            try
            {
                // Parametriza os dados para a stored procedure
                var parametros = new SqlParameter[]
                {
                    new SqlParameter("@EventoId", SqlDbType.Int) { Value = eventoId }
                };

                // Criação de objetos para armazenar os dados do evento e da lista de usuários
                EventoDTO eventoDTO = null;
                List<UsuarioEventoDTO> listaUsuariosDTO = new List<UsuarioEventoDTO>();

                // Executa a stored procedure
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "EXEC [dbo].[ListarUsuariosPorEvento] @EventoId";
                    command.Parameters.AddRange(parametros);
                    command.CommandType = CommandType.Text;

                    // Abre a conexão
                    if (command.Connection.State != ConnectionState.Open)
                        command.Connection.Open();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // Lê o primeiro conjunto de resultados (informações do evento)
                        if (await reader.ReadAsync())
                        {
                            eventoDTO = new EventoDTO
                            {
                                Titulo = reader.GetString(reader.GetOrdinal("NomeEvento")),
                                Descricao = reader.GetString(reader.GetOrdinal("Descricao")),
                                Data = reader.GetDateTime(reader.GetOrdinal("Data"))
                            };
                        }

                        // Move para o próximo conjunto de resultados (informações dos usuários)
                        if (await reader.NextResultAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var usuarioDTO = new UsuarioEventoDTO
                                {
                                    Nome = reader.GetString(reader.GetOrdinal("NomeUsuario")),
                                    StatusParticipacao = reader.GetString(reader.GetOrdinal("StatusParticipacao")),
                                    Id = reader.GetInt32(reader.GetOrdinal("Id"))
                                };
                                listaUsuariosDTO.Add(usuarioDTO);
                            }
                        }
                    }
                }

                // Retorna os dados do evento e a lista de usuários
                return Tuple.Create(eventoDTO, listaUsuariosDTO);
            }
            catch (Exception ex)
            {
                // Log de erro
                Console.WriteLine(ex.Message);
                return Tuple.Create<EventoDTO, List<UsuarioEventoDTO>>(null, new List<UsuarioEventoDTO>());
            }
        }


        public async Task<IEnumerable<TbUsuario>> GetAllUsuario()
        {
            return await _context.tbUsuario.ToListAsync();
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

        internal class UsuarioEventoResultado
        {
            public CriarEventoDTO Evento { get; set; }
            public string StatusParticipacao { get; set; }
        }
    }
}
