using System;
using System.Collections.Generic;
using Datos.Models;
using Microsoft.EntityFrameworkCore;

namespace Datos.Context;

public partial class PruebaTecnicaContext : DbContext
{
    public PruebaTecnicaContext()
    {
    }

    public PruebaTecnicaContext(DbContextOptions<PruebaTecnicaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Imagene> Imagenes { get; set; }

    public virtual DbSet<PubliImagen> PubliImagens { get; set; }

    public virtual DbSet<Publicacione> Publicaciones { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sqlserver,1433;Database=PruebaTecnica;User Id=sa;Password=Password!12345;TrustServerCertificate=True;");



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__8A3D240CEFD5FAB6");

            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(100)
                .HasColumnName("nombreCategoria");
        });

        modelBuilder.Entity<Imagene>(entity =>
        {
            entity.HasKey(e => e.IdImagen);

            entity.Property(e => e.IdImagen).HasColumnName("idImagen");
        });

        modelBuilder.Entity<PubliImagen>(entity =>
        {
            entity.HasKey(e => new { e.IdImagen, e.IdPublicacion });

            entity.ToTable("PubliImagen");

            entity.Property(e => e.IdImagen).HasColumnName("idImagen");
            entity.Property(e => e.IdPublicacion).HasColumnName("idPublicacion");

            entity.HasOne(d => d.IdImagenNavigation).WithMany(p => p.PubliImagens)
                .HasForeignKey(d => d.IdImagen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PubliImagen_Imagenes");

            entity.HasOne(d => d.IdPublicacionNavigation).WithMany(p => p.PubliImagens)
                .HasForeignKey(d => d.IdPublicacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PubliImagen_Publicaciones");
        });

        modelBuilder.Entity<Publicacione>(entity =>
        {
            entity.HasKey(e => e.IdPublicacion);

            entity.Property(e => e.IdPublicacion).HasColumnName("idPublicacion");
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.IdAutor).HasColumnName("idAutor");
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.IdImagenPublicacion).HasColumnName("idImagenPublicacion");
            entity.Property(e => e.Titulo).HasMaxLength(200);

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Publicaciones)
                .HasForeignKey(d => d.IdAutor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Publicaciones_Usuarios");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Publicaciones)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Publicaciones_Categorias");

            entity.HasOne(d => d.IdImagenPublicacionNavigation).WithMany(p => p.Publicaciones)
                .HasForeignKey(d => d.IdImagenPublicacion)
                .HasConstraintName("FK_Publicaciones_Imagenes");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.HasIndex(e => e.Email, "UQ_Usuarios_Email").IsUnique();

            entity.HasIndex(e => e.Username, "UQ_Usuarios_Username").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario").ValueGeneratedOnAdd();
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .HasColumnName("password");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
