using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Ejercicio1_5.Domain;

namespace Ejercicio1_5.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articulo> Articulos { get; set; }

    public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<FormaPago> FormaPagos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=FacturacionDB;User ID=sa;Password=Admin123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articulo>(entity =>
        {
            entity.HasKey(e => e.IdArticulo).HasName("PK__Articulo__F8FF5D526A344584");

            entity.ToTable("Articulo");

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK__DetalleF__E43646A57DE4AC06");

            entity.ToTable("DetalleFactura");

            entity.HasIndex(e => new { e.NroFactura, e.IdArticulo }, "UQ_Factura_Articulo").IsUnique();

            entity.HasOne(d => d.IdArticuloNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.IdArticulo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleFa__IdArt__412EB0B6");

            entity.HasOne(d => d.NroFacturaNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.NroFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleFa__NroFa__403A8C7D");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.NroFactura).HasName("PK__Factura__54177A8461FAAEF2");

            entity.ToTable("Factura");

            entity.Property(e => e.Cliente).HasMaxLength(100);

            entity.HasOne(d => d.IdFormaPagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdFormaPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Factura__IdForma__3B75D760");
        });

        modelBuilder.Entity<FormaPago>(entity =>
        {
            entity.HasKey(e => e.IdFormaPago).HasName("PK__FormaPag__C777CA686BADB113");

            entity.ToTable("FormaPago");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
