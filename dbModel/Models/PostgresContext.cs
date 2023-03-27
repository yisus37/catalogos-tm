using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dbModel.Models;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Paise> Paises { get; set; }

/*     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=postgres;Database=postgres;Username=a;Password=password");
 */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("paises_pk");

            entity.ToTable("paises");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Acronimo)
                .HasColumnType("character varying")
                .HasColumnName("acronimo");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying")
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
