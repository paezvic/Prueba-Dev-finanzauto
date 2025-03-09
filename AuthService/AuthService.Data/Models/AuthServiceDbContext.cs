using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data.Models;

public partial class AuthServiceDbContext : DbContext
{
    public AuthServiceDbContext()
    {
    }

    public AuthServiceDbContext(DbContextOptions<AuthServiceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Token> Tokens { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Token>(entity =>
        {
            entity.HasKey(e => e.TokenId).HasName("PK__tokens__CB3C9E172EAE4873");

            entity.ToTable("tokens");

            entity.Property(e => e.TokenId).HasColumnName("token_id");
            entity.Property(e => e.Expiracion)
                .HasColumnType("datetime")
                .HasColumnName("expiracion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.NuevoToken)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nuevo_token");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Tokens)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__tokens__usuario___3E52440B");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__usuarios__2ED7D2AF36628FEE");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Correo, "UQ__usuarios__2A586E0BB5BA062F").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.ContrasenaHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contrasena_hash");
            entity.Property(e => e.Correo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("primer_nombre");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("segundo_nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
