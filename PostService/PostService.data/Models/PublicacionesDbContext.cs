using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PostService.data.Models;

public partial class PublicacionesDbContext : DbContext
{
    public PublicacionesDbContext()
    {
    }

    public PublicacionesDbContext(DbContextOptions<PublicacionesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Publicacion> Publicacions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Publicacion>(entity =>
        {
            entity.HasKey(e => e.PublicacionId).HasName("PK__publicac__823709471AE28D95");

            entity.ToTable("publicacion");

            entity.Property(e => e.PublicacionId).HasColumnName("publicacion_id");
            entity.Property(e => e.Contenido)
                .HasColumnType("text")
                .HasColumnName("contenido");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("titulo");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
