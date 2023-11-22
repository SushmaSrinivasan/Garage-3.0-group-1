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

           

            modelBuilder.Entity<ParkingSpace>()
                .HasOne(ps => ps.Vehicle)
                .WithMany(v => v.Spots)
                .HasForeignKey(ps => ps.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }


    }
}
