using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EventosAPI.Models;

public partial class DbEventosContext : DbContext
{
    public DbEventosContext()
    {
    }

    public DbEventosContext(DbContextOptions<DbEventosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbEquipe> TbEquipes { get; set; }

    public virtual DbSet<TbEvento> TbEventos { get; set; }

    public virtual DbSet<TbInstituicao> TbInstituicaos { get; set; }

    public virtual DbSet<TbMembroEquipe> TbMembroEquipes { get; set; }

    public virtual DbSet<TbNotificacao> TbNotificacaos { get; set; }

    public virtual DbSet<TbParticipante> TbParticipantes { get; set; }

    public virtual DbSet<TbUsuario> TbUsuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=HASSAN\\SQLEXPRESS;Database=dbEventos;Trusted_Connection=True;\nTrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TbEquipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbEquipe__3214EC07FEB0554C");

            entity.ToTable("tbEquipe");

            entity.Property(e => e.Nome).HasMaxLength(100);

            //entity.HasOne(d => d.Evento).WithMany(p => p.TbEquipes)
            //    .HasForeignKey(d => d.EventoId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__tbEquipe__Evento__59063A47");
        });

        modelBuilder.Entity<TbEvento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbEvento__3214EC0715677D85");

            entity.ToTable("tbEvento");

            entity.Property(e => e.Data).HasColumnType("datetime");
            entity.Property(e => e.Descricao).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("ativo");
            entity.Property(e => e.Titulo).HasMaxLength(100);

            //entity.HasOne(d => d.Instituicao).WithMany(p => p.TbEventos)
            //    .HasForeignKey(d => d.InstituicaoId)
            //    .OnDelete(DeleteBehavior.SetNull)
            //    .HasConstraintName("FK_tbEvento_tbInstituicao");

            //entity.HasOne(d => d.Organizador).WithMany(p => p.TbEventos)
            //    .HasForeignKey(d => d.OrganizadorId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__tbEvento__Organi__5165187F");
        });

        modelBuilder.Entity<TbInstituicao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbInstit__3214EC07C65E4509");

            entity.ToTable("tbInstituicao");

            entity.Property(e => e.Nome).HasMaxLength(100);
        });

        modelBuilder.Entity<TbMembroEquipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbMembro__3214EC0724B3F9F0");

            entity.ToTable("tbMembroEquipe");

            entity.HasOne(d => d.Equipe).WithMany(p => p.TbMembroEquipes)
                .HasForeignKey(d => d.EquipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tbMembroE__Equip__5CD6CB2B");

            //entity.HasOne(d => d.Usuario).WithMany(p => p.TbMembroEquipes)
            //    .HasForeignKey(d => d.UsuarioId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__tbMembroE__Usuar__5BE2A6F2");
        });

        modelBuilder.Entity<TbNotificacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbNotifi__3214EC0716ACC0AD");

            entity.ToTable("tbNotificacao");

            entity.Property(e => e.Data)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Lida).HasDefaultValue(false);
            entity.Property(e => e.Mensagem).HasMaxLength(255);

            //entity.HasOne(d => d.Usuario).WithMany(p => p.TbNotificacaos)
            //    .HasForeignKey(d => d.UsuarioId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__tbNotific__Usuar__619B8048");
        });

        modelBuilder.Entity<TbParticipante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbPartic__3214EC07FC5E6648");

            entity.ToTable("tbParticipante");

            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("pendente");

            //entity.HasOne(d => d.Evento).WithMany(p => p.TbParticipantes)
            //    .HasForeignKey(d => d.EventoId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__tbPartici__Event__5629CD9C");

            //entity.HasOne(d => d.Usuario).WithMany(p => p.TbParticipantes)
            //    .HasForeignKey(d => d.UsuarioId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__tbPartici__Usuar__5535A963");
        });

        modelBuilder.Entity<TbUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbUsuari__3214EC07815D3690");

            entity.ToTable("tbUsuario");

            entity.HasIndex(e => e.Email, "UQ__tbUsuari__A9D10534A54D50F3").IsUnique();

            entity.Property(e => e.DataCriacao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Nome).HasMaxLength(100);
            entity.Property(e => e.Senha).HasMaxLength(255);
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .HasDefaultValue("participante");

            //entity.HasOne(d => d.Instituicao).WithMany(p => p.TbUsuarios)
            //    .HasForeignKey(d => d.InstituicaoId)
            //    .OnDelete(DeleteBehavior.SetNull)
            //    .HasConstraintName("FK_tbUsuario_tbInstituicao");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
