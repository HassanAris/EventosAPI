using System;
using System.Collections.Generic;

namespace EventosAPI.Models;

public partial class TbEvento
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descricao { get; set; }

    public DateTime Data { get; set; }

    //public DateTime DataFim { get; set; }

    public int OrganizadorId { get; set; }

    public string? Status { get; set; }

    //public virtual TbUsuario Organizador { get; set; } = null!;

    //public virtual ICollection<TbEquipe> TbEquipes { get; set; } = new List<TbEquipe>();

    //public virtual ICollection<TbParticipante> TbParticipantes { get; set; } = new List<TbParticipante>();
}
