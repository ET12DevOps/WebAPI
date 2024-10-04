using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Api.Models;

public partial class EscuelaContext : DbContext
{
    public EscuelaContext()
    {
    }

    public EscuelaContext(DbContextOptions<EscuelaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Alumnocurso> Alumnocursos { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.Idalumno).HasName("alumno_pkey");

            entity.ToTable("alumno");

            entity.Property(e => e.Idalumno).HasColumnName("idalumno");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
        });

        modelBuilder.Entity<Alumnocurso>(entity =>
        {
            entity.HasKey(e => e.Idalumnocurso).HasName("alumnocurso_pkey");

            entity.ToTable("alumnocurso");

            entity.Property(e => e.Idalumno).HasColumnName("idalumno");
            entity.Property(e => e.Idalumnocurso)
                .ValueGeneratedOnAdd()
                .HasColumnName("idalumnocurso");
            entity.Property(e => e.Idcurso).HasColumnName("idcurso");

            entity.HasOne(d => d.IdalumnoNavigation).WithMany()
                .HasForeignKey(d => d.Idalumno)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_alumno");

            entity.HasOne(d => d.IdcursoNavigation).WithMany()
                .HasForeignKey(d => d.Idcurso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_curso");
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.Idcurso).HasName("curso_pkey");

            entity.ToTable("curso");

            entity.Property(e => e.Idcurso)
                .ValueGeneratedNever()
                .HasColumnName("idcurso");
            entity.Property(e => e.Anio).HasColumnName("anio");
            entity.Property(e => e.Ciclolectivo).HasColumnName("ciclolectivo");
            entity.Property(e => e.Division).HasColumnName("division");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
