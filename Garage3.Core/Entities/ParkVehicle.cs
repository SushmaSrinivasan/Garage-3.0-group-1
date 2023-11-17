﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Core.Entities
{
    public class ParkVehicle
    {
        [Key]
        public int Id { get; set; }
        //public int VehicleTypeId { get; set; }
        public int ParkingSpaceId { get; set; }

        [Remote("IsRegistrationNumberExists", "ParkVehicles", ErrorMessage = "Registration Number already exists!")]
        public string RegistrationNumber { get; set; }
        public DateTime ParkingDate { get; set; }
        public VehicleType VehicleType { get; set; }

        [StringLength(50)]
        public string Color { get; set; } = string.Empty;

        [StringLength(15)]
        public string Brand { get; set; } = string.Empty;

        [StringLength(20)]
        public string Model { get; set; } = string.Empty;

        [Range(0, 10, ErrorMessage = "Wheels value should be within 0 and 10")]
        public int NumberOfWheels { get; set; }

        // Set ParkingDate to the current date and time
        public ParkVehicle()
        {
            ParkingDate = DateTime.Now;
        }
    }
}
