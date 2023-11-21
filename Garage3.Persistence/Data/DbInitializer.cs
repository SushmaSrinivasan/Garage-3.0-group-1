using Garage3.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage3.Persistence.Data
{
    public class DbInitializer
    {
        public async Task Initialize (GarageContext context)
        {
            try
            {
                context.Database.EnsureCreated();

                //Look for any vehicles
                if (context.ParkVehicle.Any())
                {
                    return; //DB has been seeded
                }

                var vehicleType = new VehicleType[]
              {
                new VehicleType{Name = "Car",Spaces=1},
                new VehicleType{Name = "Bus", Spaces=2 },
                new VehicleType{Name = "AirPlane",Spaces=3},
                new VehicleType{Name = "Motorcycle",Spaces=1},
                new VehicleType{Name="Truck",Spaces = 2},
                new VehicleType{Name="Boat", Spaces=3 }
              };

                await context.AddRangeAsync(vehicleType);

                var member = new Member[]
                {
                new Member{Personnummer=198712154221, FirstName="Kalle", LastName="Karlsson",Membership=Membership.Free},
                new Member{Personnummer=198010151226, FirstName="Johan", LastName="David",Membership=Membership.Free},
                new Member{Personnummer=197512123772, FirstName="Anna", LastName="Andersson",Membership=Membership.Pro},
                new Member{Personnummer=196208104221, FirstName="Mohammed", LastName="Mokthar",Membership=Membership.Free},
                new Member{Personnummer=197106064221, FirstName="Fredrik", LastName="Svensson",Membership=Membership.Pro},
                };

                await context.AddRangeAsync(member);



                var parkVehicles = new ParkVehicle[]
                {
                new ParkVehicle{VehicleType = vehicleType[0],Owner = member[0], RegistrationNumber="ABC 123",ParkingDate=DateTime.Parse("2023-11-11"),Color="Black",Model="Q3",Brand="Audi",NumberOfWheels=4},
                new ParkVehicle{VehicleType = vehicleType[1],Owner = member[1], RegistrationNumber="DEF 111",ParkingDate=DateTime.Parse("2023-11-12"),Color="Blue",Model="Corolla",Brand="Toyota",NumberOfWheels=4},
                new ParkVehicle{VehicleType = vehicleType[2],Owner = member[2], RegistrationNumber="GHI 222",ParkingDate=DateTime.Parse("2023-11-13"),Color="Red",Model="A123",Brand="Boeing",NumberOfWheels=6},
                new ParkVehicle{VehicleType = vehicleType[3],Owner = member[3], RegistrationNumber="JKL 333",ParkingDate=DateTime.Parse("2023-11-14"),Color="Yellow",Model="TTT",Brand="Cresent",NumberOfWheels=2},
                new ParkVehicle{VehicleType = vehicleType[4],Owner = member[4], RegistrationNumber="MNO 444",ParkingDate=DateTime.Parse("2023-11-15"),Color="Orange",Model="YYY",Brand="XXX",NumberOfWheels=6}
                };

                await context.AddRangeAsync(parkVehicles);


                var parkingSpace = new ParkingSpace[]
                {
                new ParkingSpace{Vehicle = parkVehicles[0] },
                new ParkingSpace{Vehicle = parkVehicles[0] },
                new ParkingSpace{Vehicle = parkVehicles[0] },
                new ParkingSpace{Vehicle = parkVehicles[0] },
                new ParkingSpace{Vehicle = parkVehicles[0] }
                };
                await context.AddRangeAsync(parkingSpace);
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
    }
}
