using Garage3.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Web.Models.ViewModels
{
    public class ParkVehicleViewModel
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public int VehicleTypeId { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        [Range(0, 10, ErrorMessage = "Wheels value should be within 0 and 10")]
        public int NumberOfWheels { get; set; }
        public long Personnummer { get; set; }

        public IEnumerable<ParkVehicleTypeViewModel> VehicleTypes { get; set; } = new List<ParkVehicleTypeViewModel>();
    }
}
