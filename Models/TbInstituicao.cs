using System;
using System.Collections.Generic;

namespace EventosAPI.Models;

public partial class TbInstituicao
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<TbEvento> TbEventos { get; set; } = new List<TbEvento>();

    public virtual ICollection<TbUsuario> TbUsuarios { get; set; } = new List<TbUsuario>();
}
