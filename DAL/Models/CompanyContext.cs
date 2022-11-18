using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class CompanyContext : DbContext
{
    public CompanyContext()
    {
    }

    public CompanyContext(DbContextOptions<CompanyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=company;User Id=postgres;Password=altair");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Areas_pkey");

            entity.ToTable("Areas", "main");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Area1)
                .HasColumnType("character varying")
                .HasColumnName("area");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Employees_pkey");

            entity.ToTable("Employees", "main");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EmployeeArea).HasColumnName("employee_area");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasOne(d => d.EmployeeAreaNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeeArea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_area_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
