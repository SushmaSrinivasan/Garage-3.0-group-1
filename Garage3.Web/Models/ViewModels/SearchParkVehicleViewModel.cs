﻿using Garage3.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Web.Models.ViewModels
{
    public class SearchParkVehicleViewModel
    {
        [Display(Name = "Registration Number")]
        public string? RegistrationNumber { get; set; }

        [Display(Name = "Arrival")]
        public DateTime ParkingDate { get; set; }

        [Display(Name = "Type")]
        public VehicleType? VehicleType { get; set; }

        public string? Color { get; set; } = string.Empty;

        public string? Brand { get; set; } = string.Empty;

        public string? Model { get; set; } = string.Empty;

        [Display(Name = "Wheels")]
        public int? NumberOfWheels { get; set; }

        public string? SortOrder { get; set; }

        public IEnumerable<ParkVehicle> Vehicles { get; set; } = new List<ParkVehicle>();
    }
}
