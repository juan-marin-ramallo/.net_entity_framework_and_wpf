using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EntidadEntityFramework;

namespace DatosEntityFramework
{
    public partial class ReservaCochesContext : DbContext
    {
        public ReservaCochesContext()
        {
        }

        public ReservaCochesContext(DbContextOptions<ReservaCochesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Coche> Coches { get; set; } = null!;
        public virtual DbSet<Reserva> Reservas { get; set; } = null!;
        public virtual DbSet<ReservaCoche> ReservaCoches { get; set; } = null!;
        public virtual DbSet<UbicacionCocheReserva> UbicacionCocheReservas { get; set; } = null!;
        public virtual DbSet<VwClienteReservaCoche> VwClienteReservaCoches { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-4M1UU7M\\MSSQLSERVER2019;Initial Catalog=reserva_coches;User ID=reserva_coches;Password=reserva_coches");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.CodCliente);

                entity.ToTable("Cliente");

                entity.HasIndex(e => e.Cedula, "IX_Cedula")
                    .IsUnique();

                entity.Property(e => e.CodCliente).HasColumnName("codCliente");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cedula");

                entity.Property(e => e.CodGarante).HasColumnName("codGarante");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.CodGaranteNavigation)
                    .WithMany(p => p.InverseCodGaranteNavigation)
                    .HasForeignKey(d => d.CodGarante)
                    .HasConstraintName("FK_Cliente_Cliente");
            });

            modelBuilder.Entity<Coche>(entity =>
            {
                entity.HasKey(e => e.Placa);

                entity.ToTable("Coche");

                entity.Property(e => e.Placa)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("placa");

                entity.Property(e => e.Color)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("color");

                entity.Property(e => e.Marca)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("marca");

                entity.Property(e => e.Modelo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("modelo");

                entity.Property(e => e.PrecioHoraAlquiler)
                    .HasColumnType("money")
                    .HasColumnName("precioHoraAlquiler");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.NumeroReserva);

                entity.ToTable("Reserva");

                entity.Property(e => e.NumeroReserva)
                    .ValueGeneratedNever()
                    .HasColumnName("numeroReserva");

                entity.Property(e => e.CodCliente).HasColumnName("codCliente");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaFin");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaInicio");

                entity.Property(e => e.PrecioTotalReserva)
                    .HasColumnType("money")
                    .HasColumnName("precioTotalReserva");

                entity.HasOne(d => d.CodClienteNavigation)
                    .WithMany(p => p.Reservas)
                    .HasForeignKey(d => d.CodCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reserva_Cliente");
            });

            modelBuilder.Entity<ReservaCoche>(entity =>
            {
                entity.HasKey(e => new { e.NumeroReserva, e.Placa });

                entity.ToTable("ReservaCoche");

                entity.Property(e => e.NumeroReserva).HasColumnName("numeroReserva");

                entity.Property(e => e.Placa)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("placa");

                entity.Property(e => e.GalonesGasolina).HasColumnName("galonesGasolina");

                entity.HasOne(d => d.NumeroReservaNavigation)
                    .WithMany(p => p.ReservaCoches)
                    .HasForeignKey(d => d.NumeroReserva)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservaCoche_Reserva");

                entity.HasOne(d => d.PlacaNavigation)
                    .WithMany(p => p.ReservaCoches)
                    .HasForeignKey(d => d.Placa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReservaCoche_Coche");
            });

            modelBuilder.Entity<UbicacionCocheReserva>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UbicacionCocheReserva");

                entity.Property(e => e.FechaHora)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaHora");

                entity.Property(e => e.Latitud)
                    .HasColumnType("numeric(10, 7)")
                    .HasColumnName("latitud");

                entity.Property(e => e.Longitud)
                    .HasColumnType("numeric(10, 7)")
                    .HasColumnName("longitud");

                entity.Property(e => e.NumeroReserva).HasColumnName("numeroReserva");

                entity.Property(e => e.Placa)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("placa");

                entity.HasOne(d => d.ReservaCoche)
                    .WithMany()
                    .HasForeignKey(d => new { d.NumeroReserva, d.Placa })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UbicacionCocheReserva_ReservaCoche");
            });

            modelBuilder.Entity<VwClienteReservaCoche>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwClienteReservaCoche");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cedula");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaFin");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaInicio");

                entity.Property(e => e.GalonesGasolina).HasColumnName("galonesGasolina");

                entity.Property(e => e.Marca)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("marca");

                entity.Property(e => e.Modelo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("modelo");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NumeroReserva).HasColumnName("numeroReserva");

                entity.Property(e => e.Placa)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("placa");

                entity.Property(e => e.PrecioHoraAlquiler)
                    .HasColumnType("money")
                    .HasColumnName("precioHoraAlquiler");

                entity.Property(e => e.PrecioTotalReserva)
                    .HasColumnType("money")
                    .HasColumnName("precioTotalReserva");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
