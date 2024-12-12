namespace EventosAPI.Models
{
    public class RevokedToken
    {
        public int Id { get; set; } // Ou outro tipo de chave primária
        public string Token { get; set; }
        public DateTime RevokedAt { get; set; }
        // Outros campos conforme necessário
    }

}
