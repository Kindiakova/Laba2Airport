using Microsoft.EntityFrameworkCore;
using Laba2.Models;
namespace Laba2
{
    public class Laba2Context : DbContext
    {
        public Laba2Context()
        {

        }

        public Laba2Context(DbContextOptions<Laba2Context> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Plane> Planes { get; set; }
        public virtual DbSet<Direction> Directions { get; set; }
        public virtual DbSet<Pilot> Pilots { get; set; }
        public virtual DbSet<Licence> Licences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Flight>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.TakeOffTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Plane>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.Model).HasMaxLength(255).IsRequired();
                entity.Property(e => e.MaxPassAmount).IsRequired();
                entity.HasMany(d => d.Flights)
                    .WithOne(e => e.Plane)
                    .HasForeignKey(d => d.PlaneId)
                    .OnDelete(DeleteBehavior.SetNull);

            });
            modelBuilder.Entity<Pilot>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.Name).HasMaxLength(255).IsRequired();
                entity.Property(e => e.BirthDate).HasColumnType("datetime");
                entity.HasMany(d => d.Flights)
                    .WithOne(e => e.Pilot)
                    .HasForeignKey(d => d.PilotId)
                    .OnDelete(DeleteBehavior.SetNull);               
                entity.HasMany(d => d.Licences)
                   .WithOne(e => e.Pilot)
                   .HasForeignKey(p => p.PilotId)
                   .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Direction>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.CountryTo).HasMaxLength(255);
                entity.Property(e => e.CountryFrom).HasMaxLength(255);
                entity.Property(e => e.CityTo).HasMaxLength(255);
                entity.Property(e => e.CityFrom).HasMaxLength(255);
                entity.Property(e => e.TerminalTo).HasMaxLength(50);
                entity.Property(e => e.TerminalFrom).HasMaxLength(50);
                entity.HasMany(d => d.Flights)
                  .WithOne(e => e.Direction)
                  .HasForeignKey(d => d.DirectionId)
                  .OnDelete(DeleteBehavior.SetNull);
            });
            modelBuilder.Entity<Licence>(entity =>
            {
                entity.Property(e => e.Id).UseIdentityColumn();
                entity.Property(e => e.EndDate).HasColumnType("datetime");
            });
        }
    }
}
