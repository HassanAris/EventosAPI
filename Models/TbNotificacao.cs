using System;
using System.Collections.Generic;

namespace EventosAPI.Models;

public partial class TbNotificacao
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string Mensagem { get; set; } = null!;

    public DateTime? Data { get; set; }

    public bool? Lida { get; set; }

    public virtual TbUsuario Usuario { get; set; } = null!;
}
