using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Models;

namespace StudentAPI.Data;

public partial class StudentDbContext : DbContext
{
    public StudentDbContext()
    {
    }

    public StudentDbContext(DbContextOptions<StudentDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class10> Class10s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultSQLConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class10>(entity =>
        {
            entity.HasKey(e => e.AdmissionNum);

            entity.ToTable("Class10");

            entity.Property(e => e.AdmissionNum).HasColumnName("Admission Num");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RollNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Section)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
