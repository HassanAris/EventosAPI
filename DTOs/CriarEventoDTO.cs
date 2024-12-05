using EventosAPI.Models;

namespace EventosAPI.DTOs
{
    public class CriarEventoDTO
    {
        public EventoDTO Evento { get; set; }  // Dados do Evento
        public List<UsuarioDTO> Usuarios { get; set; }  // Lista de Usuários
    }

    public class EventoDTO
    {
        public string Titulo { get; set; }  // Título do evento
        public DateTime Data { get; set; }  // Data do evento
        public string Descricao { get; set; }  // Descrição do evento
        public string Status { get; set; }  // Status do evento (ex: "ok")
        public int OrganizadorId { get; set; }
    }

    public class UsuarioDTO
    {
        public int Id { get; set; }  // ID do usuário
        public string Nome { get; set; }  // Nome do usuário
    }

}
