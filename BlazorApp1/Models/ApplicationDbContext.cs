using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calificaciones> Calificaciones { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Grado> Grados { get; set; }

    public virtual DbSet<Maestro> Maestros { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calificaciones>(entity =>
        {
            entity.HasKey(e => e.IdCalificacion).HasName("PK__Califica__40E4A75179A16669");

            entity.Property(e => e.ExamenFinal)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("Examen_Final");
            entity.Property(e => e.NotaFinal)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("Nota_Final");
            entity.Property(e => e.Zona).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.IdCurso)
                .HasConstraintName("FK__Calificac__IdCur__693CA210");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.IdEstudiante)
                .HasConstraintName("FK__Calificac__IdEst__68487DD7");
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("PK__Curso__085F27D6388385A5");

            entity.ToTable("Curso");

            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.IdGradoNavigation).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.IdGrado)
                .HasConstraintName("FK__Curso__IdGrado__656C112C");

            entity.HasOne(d => d.IdMaestroNavigation).WithMany(p => p.Cursos)
                .HasForeignKey(d => d.IdMaestro)
                .HasConstraintName("FK__Curso__IdMaestro__6477ECF3");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("PK__Estudian__B5007C249BF4DD87");

            entity.ToTable("Estudiante");

            entity.HasOne(d => d.IdGradoNavigation).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.IdGrado)
                .HasConstraintName("FK__Estudiant__IdGra__619B8048");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Estudiant__IdUsu__60A75C0F");
        });

        modelBuilder.Entity<Grado>(entity =>
        {
            entity.HasKey(e => e.IdGrado).HasName("PK__Grado__393DFCB880D0605F");

            entity.ToTable("Grado");

            entity.Property(e => e.Nivel)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Maestro>(entity =>
        {
            entity.HasKey(e => e.IdMaestro).HasName("PK__Maestro__66B8F1892F899309");

            entity.ToTable("Maestro");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Maestros)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Maestro__IdUsuar__5BE2A6F2");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97F8F136F4");

            entity.ToTable("Usuario");

            entity.Property(e => e.Apellido)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Contraseña)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnName("Fecha_Nacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
