using System;
using System.Collections.Generic;

namespace EventosAPI.Models;

public partial class TbEquipe
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public int EventoId { get; set; }

    public virtual TbEvento Evento { get; set; } = null!;

    public virtual ICollection<TbMembroEquipe> TbMembroEquipes { get; set; } = new List<TbMembroEquipe>();
}
