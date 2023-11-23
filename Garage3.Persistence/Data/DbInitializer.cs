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
        public async Task Initialize(GarageContext context)
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
                new Member{Personnummer=186805051141, FirstName="Victoriano", LastName="Wisham",Membership=Membership.Pro},
                new Member{Personnummer=196107071348, FirstName="Wanetta", LastName="Foulton",Membership=Membership.Pro},
                new Member{Personnummer=199006068112, FirstName="Ari", LastName="von Mellin",Membership=Membership.Pro},
                new Member{Personnummer=190801010537, FirstName="Benita", LastName="Friskeney",Membership=Membership.Free},
                new Member{Personnummer=190007072887, FirstName="Avent", LastName="Puckering",Membership=Membership.Free},
                new Member{Personnummer=200110103942, FirstName="Wille", LastName="Begbie",Membership=Membership.Free},
                new Member{Personnummer=190804045697, FirstName="Octavia", LastName="Causey",Membership=Membership.Free},
                new Member{Personnummer=195612129994, FirstName="Marlene", LastName="Horn af Kanckas",Membership=Membership.Pro},
                new Member{Personnummer=191505057060, FirstName="Glen", LastName="Spin",Membership=Membership.Free},
                new Member{Personnummer=193104041720, FirstName="Manón", LastName="Almeras",Membership=Membership.Pro}
                };

                await context.AddRangeAsync(member);

                var parkVehicles = new ParkVehicle[]
                {
                new ParkVehicle{VehicleType = vehicleType[0],Owner = member[0], RegistrationNumber="ABC 123",ParkingDate=DateTime.Parse("2023-11-11"),Color="Black",Model="Q3",Brand="Audi",NumberOfWheels=4},
                new ParkVehicle{VehicleType = vehicleType[1],Owner = member[1], RegistrationNumber="DEF 111",ParkingDate=DateTime.Parse("2023-11-12"),Color="Blue",Model="Corolla",Brand="Toyota",NumberOfWheels=4},
                new ParkVehicle{VehicleType = vehicleType[2],Owner = member[2], RegistrationNumber="GHI 222",ParkingDate=DateTime.Parse("2023-11-13"),Color="Red",Model="A123",Brand="Boeing",NumberOfWheels=6},
                new ParkVehicle{VehicleType = vehicleType[3],Owner = member[3], RegistrationNumber="JKL 333",ParkingDate=DateTime.Parse("2023-11-14"),Color="Yellow",Model="TTT",Brand="Cresent",NumberOfWheels=2},
                new ParkVehicle{VehicleType = vehicleType[4],Owner = member[4], RegistrationNumber="MNO 444",ParkingDate=DateTime.Parse("2023-11-15"),Color="Orange",Model="YYY",Brand="XXX",NumberOfWheels=6},
                new ParkVehicle{VehicleType = vehicleType[0],Owner = member[1], RegistrationNumber="EWT 679",ParkingDate=new DateTime(2023 , 11, 21, 3, 38, 54),Color="Bazaar",Model="Model X",Brand="Tesla",NumberOfWheels=2},
                new ParkVehicle{VehicleType = vehicleType[4],Owner = member[7], RegistrationNumber="XEP 681",ParkingDate=new DateTime(2023 , 11, 19, 23, 51, 24),Color="Amber",Model="i3",Brand="BMW",NumberOfWheels=10},
                new ParkVehicle{VehicleType = vehicleType[5],Owner = member[8], RegistrationNumber="ZCW 889",ParkingDate=new DateTime(2023 , 11, 19, 21, 47, 34),Color="Amaranth",Model="Civic",Brand="Honda",NumberOfWheels=1},
                new ParkVehicle{VehicleType = vehicleType[0],Owner = member[4], RegistrationNumber="WUY 511",ParkingDate=new DateTime(2023 , 11, 16, 14, 32, 27),Color="Beaver",Model="ID.4",Brand="Volkswagen",NumberOfWheels=7},
                new ParkVehicle{VehicleType = vehicleType[1],Owner = member[10], RegistrationNumber="FUN 911",ParkingDate=new DateTime(2023 , 11, 20, 17, 35, 41),Color="White",Model="Equinox",Brand="Chevrolet",NumberOfWheels=9},
                new ParkVehicle{VehicleType = vehicleType[4],Owner = member[5], RegistrationNumber="QNR 989",ParkingDate=new DateTime(2023 , 11, 19, 8, 26, 57),Color="Beaver",Model="Explorer",Brand="Ford",NumberOfWheels=5},
                new ParkVehicle{VehicleType = vehicleType[4],Owner = member[8], RegistrationNumber="MAG 684",ParkingDate=new DateTime(2023 , 11, 21, 1, 3, 44),Color="Dandelion",Model="Civic",Brand="Honda",NumberOfWheels=7},
                new ParkVehicle{VehicleType = vehicleType[0],Owner = member[14], RegistrationNumber="SOE 361",ParkingDate=new DateTime(2023 , 11, 15, 23, 23, 31),Color="Bazaar",Model="ID.4",Brand="Volkswagen",NumberOfWheels=0},
                new ParkVehicle{VehicleType = vehicleType[5],Owner = member[14], RegistrationNumber="DPV 205",ParkingDate=new DateTime(2023 , 11, 21, 3, 18, 19),Color="Amber",Model="Corolla",Brand="Toyota",NumberOfWheels=2},
                new ParkVehicle{VehicleType = vehicleType[3],Owner = member[4], RegistrationNumber="MUO 539",ParkingDate=new DateTime(2023 , 11, 20, 14, 11, 34),Color="Beige",Model="F-150",Brand="Ford",NumberOfWheels=10},
                new ParkVehicle{VehicleType = vehicleType[2],Owner = member[9], RegistrationNumber="TEZ 286",ParkingDate=new DateTime(2023 , 11, 15, 7, 7, 32),Color="Chocolate",Model="Model X",Brand="Tesla",NumberOfWheels=0},
                new ParkVehicle{VehicleType = vehicleType[5],Owner = member[7], RegistrationNumber="WVK 501",ParkingDate=new DateTime(2023 , 11, 17, 14, 40, 22),Color="Green",Model="Camaro",Brand="Chevrolet",NumberOfWheels=4},
                new ParkVehicle{VehicleType = vehicleType[5],Owner = member[10], RegistrationNumber="ADW 873",ParkingDate=new DateTime(2023 , 11, 19, 6, 20, 12),Color="Chestnut",Model="Silverado",Brand="Chevrolet",NumberOfWheels=10},
                new ParkVehicle{VehicleType = vehicleType[1],Owner = member[12], RegistrationNumber="XBP 388",ParkingDate=new DateTime(2023 , 11, 17, 12, 5, 44),Color="Amaranth",Model="E-Class",Brand="Mercedes-Benz",NumberOfWheels=9},
                new ParkVehicle{VehicleType = vehicleType[3],Owner = member[14], RegistrationNumber="LGQ 423",ParkingDate=new DateTime(2023 , 11, 16, 12, 50, 26),Color="Cyan",Model="Model Y",Brand="Tesla",NumberOfWheels=7}
                };

                await context.AddRangeAsync(parkVehicles);


                var parkingSpace = new List<ParkingSpace>(50);
                
                for (int i = 0; i < 50; i++)
                {
                    parkingSpace.Add(new ParkingSpace());
                }

                await context.AddRangeAsync(parkingSpace);
                await context.SaveChangesAsync();


                parkingSpace[43].Vehicle = parkVehicles[18];
                parkingSpace[44].Vehicle = parkVehicles[18];

                parkingSpace[13].Vehicle = parkVehicles[12];

                parkingSpace[38].Vehicle = parkVehicles[13];
                parkingSpace[39].Vehicle = parkVehicles[13];
                parkingSpace[40].Vehicle = parkVehicles[13];

                parkingSpace[25].Vehicle = parkVehicles[7];
                parkingSpace[26].Vehicle = parkVehicles[7];
                parkingSpace[27].Vehicle = parkVehicles[7];

                parkingSpace[4].Vehicle = parkVehicles[0];

                parkingSpace[30].Vehicle = parkVehicles[2];
                parkingSpace[31].Vehicle = parkVehicles[2];
                parkingSpace[32].Vehicle = parkVehicles[2];

                parkingSpace[41].Vehicle = parkVehicles[9];
                parkingSpace[42].Vehicle = parkVehicles[9];

                parkingSpace[20].Vehicle = parkVehicles[19];

                parkingSpace[10].Vehicle = parkVehicles[15];
                parkingSpace[11].Vehicle = parkVehicles[15];
                parkingSpace[12].Vehicle = parkVehicles[15];

                parkingSpace[33].Vehicle = parkVehicles[5];

                parkingSpace[6].Vehicle = parkVehicles[1];
                parkingSpace[7].Vehicle = parkVehicles[1];

                parkingSpace[22].Vehicle = parkVehicles[4];
                parkingSpace[23].Vehicle = parkVehicles[4];

                parkingSpace[0].Vehicle = parkVehicles[6];
                parkingSpace[1].Vehicle = parkVehicles[6];

                parkingSpace[36].Vehicle = parkVehicles[10];
                parkingSpace[37].Vehicle = parkVehicles[10];

                parkingSpace[18].Vehicle = parkVehicles[8];

                parkingSpace[14].Vehicle = parkVehicles[16];
                parkingSpace[15].Vehicle = parkVehicles[16];
                parkingSpace[16].Vehicle = parkVehicles[16];

                parkingSpace[9].Vehicle = parkVehicles[3];

                parkingSpace[5].Vehicle = parkVehicles[14];

                parkingSpace[45].Vehicle = parkVehicles[11];
                parkingSpace[46].Vehicle = parkVehicles[11];

                context.UpdateRange(parkingSpace);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
