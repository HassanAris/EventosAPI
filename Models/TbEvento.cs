using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventosAPI.Models;

public partial class TbEvento
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Titulo { get; set; } = null!;

    public string? Descricao { get; set; }

    [Required]
    public int OrganizadorId { get; set; }

    public string? Status { get; set; }

    public DateTime? Data { get; set; }

    [Required]
    public int? InstituicaoId { get; set; }

    //public virtual TbInstituicao? Instituicao { get; set; }

    //public virtual TbUsuario Organizador { get; set; } = null!;

    //public virtual ICollection<TbEquipe> TbEquipes { get; set; } = new List<TbEquipe>();

    //public virtual ICollection<TbParticipante> TbParticipantes { get; set; } = new List<TbParticipante>();
}
