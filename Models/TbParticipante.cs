using System;
using System.Collections.Generic;

namespace EventosAPI.Models;

public partial class TbParticipante
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public int EventoId { get; set; }

    public string? Status { get; set; }

    public virtual TbEvento Evento { get; set; } = null!;

    public virtual TbUsuario Usuario { get; set; } = null!;
}
