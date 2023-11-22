using Garage3.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Garage3.Persistence.Data
{
    public class GarageContext : DbContext
    {
        public GarageContext(DbContextOptions<GarageContext> options)
            : base(options)
        {
        }

        public GarageContext()
        {
            
        }


        public DbSet<ParkVehicle> ParkVehicle => Set<ParkVehicle>();
        
        public DbSet<Member> Member => Set<Member>();

        public DbSet<ParkingSpace> ParkingSpaces => Set<ParkingSpace>();

        public DbSet<VehicleType> VehicleTypes => Set<VehicleType>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ParkVehicle>()
                .HasOne(pv => pv.Owner)
                .WithMany(m => m.Vehicles)
                .HasForeignKey(pv => pv.MemberId);  

            modelBuilder.Entity<Member>()
                .HasMany(m => m.Vehicles)
                .WithOne(pv => pv.Owner)
                .HasForeignKey(pv => pv.MemberId);

            modelBuilder.Entity<ParkingSpace>()
                .HasOne(ps => ps.Vehicle)
                .WithMany(v => v.Spots)
                .HasForeignKey(ps => ps.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }


    }
}
