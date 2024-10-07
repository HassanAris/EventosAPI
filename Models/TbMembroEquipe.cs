using System;
using System.Collections.Generic;

namespace EventosAPI.Models;

public partial class TbMembroEquipe
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int EquipeId { get; set; }

    public virtual TbEquipe Equipe { get; set; } = null!;

    public virtual TbUsuario Usuario { get; set; } = null!;
}
