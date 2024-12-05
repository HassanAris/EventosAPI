using System;
using System.Collections.Generic;

namespace EventosAPI.Models;

public partial class TbUsuario
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public DateTime? DataCriacao { get; set; }

    public string Tipo { get; set; } = null!;

    public int? InstituicaoId { get; set; }

    //public virtual TbInstituicao? Instituicao { get; set; }

    //public virtual ICollection<TbEvento> TbEventos { get; set; } = new List<TbEvento>();

    //public virtual ICollection<TbMembroEquipe> TbMembroEquipes { get; set; } = new List<TbMembroEquipe>();

    //public virtual ICollection<TbNotificacao> TbNotificacaos { get; set; } = new List<TbNotificacao>();

    //public virtual ICollection<TbParticipante> TbParticipantes { get; set; } = new List<TbParticipante>();
}
