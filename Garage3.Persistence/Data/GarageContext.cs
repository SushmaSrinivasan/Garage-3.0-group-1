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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<ParkVehicle>().HasData(
            //    new ParkVehicle { Id = 1, RegistrationNumber = "ABC 123", ParkingDate = DateTime.Parse("2023-10-25"), VehicleType = VehicleType.Airplane, Color = "White", Brand = "Boeing",Model="A124",NumberOfWheels=6 },
            //    new ParkVehicle { Id = 2, RegistrationNumber = "DEF 456", ParkingDate = DateTime.Parse("2023-10-30"), VehicleType = VehicleType.Boat, Color = "Yellow", Brand = "MasterCraft", Model = "011", NumberOfWheels = 0 },
            //    new ParkVehicle { Id = 3, RegistrationNumber = "GHI 789", ParkingDate = DateTime.Parse("2023-11-01"), VehicleType = VehicleType.Bus, Color = "Red", Brand = "Volvo", Model = "Scania", NumberOfWheels = 4 },
            //    new ParkVehicle { Id = 4, RegistrationNumber = "JKL 012", ParkingDate = DateTime.Parse("2023-11-02"), VehicleType = VehicleType.Car, Color = "Black", Brand = "Toyota", Model = "Corolla", NumberOfWheels = 4},
            //    new ParkVehicle { Id = 5, RegistrationNumber = "MNO 111", ParkingDate = DateTime.Parse("2023-11-03"),VehicleType = VehicleType.Motorcycle, Color = "Blue", Brand = "Cresent", Model = "92323", NumberOfWheels = 2 }
            //    );
        }
    }
}
