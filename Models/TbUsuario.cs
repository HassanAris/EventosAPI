using System.ComponentModel.DataAnnotations;

public class TbUsuario
{
    public int Id { get; set; }

    [StringLength(100)]
    public string Nome { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [StringLength(255)]
    public string Senha { get; set; }

    public DateTime DataCriacao { get; set; }
}
