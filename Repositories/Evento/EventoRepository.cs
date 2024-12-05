using EventosAPI.DTOs;
using EventosAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

public class EventoRepository : IEventoRepository
{
    private readonly ApplicationDbContext _context;

    public EventoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CriarEvento(CriarEventoDTO dto, int userId)
    {
        try
        {
            // Converte a lista de usuários para o formato JSON
            var usuariosJson = JsonConvert.SerializeObject(dto.Usuarios);

            // Parametriza os dados para a stored procedure
            var parametros = new SqlParameter[]
            {
                new SqlParameter("@Titulo", SqlDbType.NVarChar) { Value = dto.Evento.Titulo },
                new SqlParameter("@Descricao", SqlDbType.NVarChar) { Value = dto.Evento.Descricao },
                new SqlParameter("@Data", SqlDbType.DateTime) { Value = dto.Evento.Data },
                new SqlParameter("@Status", SqlDbType.NVarChar) { Value = dto.Evento.Status },
                new SqlParameter("@Usuarios", SqlDbType.NVarChar) { Value = usuariosJson },
                new SqlParameter("@OrganizadorId", SqlDbType.Int) { Value = userId }
            };

            // Executa a stored procedure
            var resultado = await _context.Database.ExecuteSqlRawAsync(
                "EXEC CriarEventoEAssociarParticipantes @Titulo, @Descricao, @Data, @Status, @Usuarios, @OrganizadorId", parametros);

            return resultado > 0;
        }
        catch (Exception ex)
        {
            // Aqui você pode adicionar log de erros
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> EditarEvento(CriarEventoDTO dto, int userId)
    {
        try
        {
            // Converte a lista de usuários para o formato JSON
            var usuariosJson = JsonConvert.SerializeObject(dto.Usuarios);

            // Parametriza os dados para a stored procedure
            var parametros = new SqlParameter[]
            {
                new SqlParameter("@IdEvento", SqlDbType.Int) { Value = dto.Evento.Id },
                new SqlParameter("@Titulo", SqlDbType.NVarChar) { Value = dto.Evento.Titulo },
                new SqlParameter("@Descricao", SqlDbType.NVarChar) { Value = dto.Evento.Descricao },
                new SqlParameter("@Data", SqlDbType.DateTime) { Value = dto.Evento.Data },
                new SqlParameter("@Status", SqlDbType.NVarChar) { Value = dto.Evento.Status },
                new SqlParameter("@Usuarios", SqlDbType.NVarChar) { Value = usuariosJson },
                new SqlParameter("@OrganizadorId", SqlDbType.Int) { Value = userId }
            };

            // Executa a stored procedure
            var resultado = await _context.Database.ExecuteSqlRawAsync(
                "EXEC EditarEvento @IdEvento, @Titulo, @Descricao, @Data, @Status, @Usuarios, @OrganizadorId", parametros);

            return resultado > 0;
        }
        catch (Exception ex)
        {
            // Aqui você pode adicionar log de erros
            Console.WriteLine(ex.Message);
            return false;
        }
    }


    public async Task<IEnumerable<TbEvento>> ObterEventos()
    {
        return await _context.tbEvento
            .FromSqlInterpolated($"EXEC ObterEventosAposHoje")
            .ToListAsync();
    }

    public async Task<List<TbEvento>> ListarEventosPorUsuario(int usuarioId)
    {
        try
        {
            // Parametriza os dados para a stored procedure
            var parametros = new SqlParameter[]
            {
                new SqlParameter("@UsuarioId", SqlDbType.Int) { Value = usuarioId }
            };

            // Executa a stored procedure
            var eventos = await _context.tbEvento
                .FromSqlRaw("EXEC [dbo].[ListarEventosPorUsuario] @UsuarioId", parametros)
                .ToListAsync();

            return eventos;
        }
        catch (Exception ex)
        {
            // Aqui você pode adicionar log de erros
            Console.WriteLine(ex.Message);
            return new List<TbEvento>(); // Retorna uma lista vazia em caso de erro
        }
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

    public async Task<bool> DeletarEvento(int id)
    {
        try
        {
            var parametros = new SqlParameter[]
            {
            new SqlParameter("@EventoId", SqlDbType.Int) { Value = id }
            };

            // Executa a stored procedure ExcluirEventoEParticipantes
            var resultado = await _context.Database.ExecuteSqlRawAsync(
                "EXEC ExcluirEventoEParticipantes @EventoId", parametros);

            return resultado > 0; // Retorna true se a exclusão for bem-sucedida
        }
        catch (Exception ex)
        {
            // Log de erro
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> AceitarOuRecusarEvento(int eventoId, int usuarioId, string status)
    {
        try
        {
            // Parametriza os dados para a stored procedure
            var parametros = new SqlParameter[]
            {
                new SqlParameter("@EventoId", SqlDbType.Int) { Value = eventoId },
                new SqlParameter("@UsuarioId", SqlDbType.Int) { Value = usuarioId },
                new SqlParameter("@Status", SqlDbType.NVarChar) { Value = status }
            };

            // Executa a stored procedure para aceitar ou recusar o evento
            var resultado = await _context.Database.ExecuteSqlRawAsync(
                "EXEC [dbo].[AceitarOuRecusarEvento] @EventoId, @UsuarioId, @Status",
                parametros);

            return resultado == -1; // Retorna verdadeiro se a atualização foi bem-sucedida
        }
        catch (Exception ex)
        {
            // Log de erro
            Console.WriteLine($"Erro ao aceitar ou recusar evento: {ex.Message}");
            return false;
        }
    }


}
