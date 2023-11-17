using Garage3.Core.Entities;
using System.ComponentModel.DataAnnotations;
namespace Garage3.Web.Models.ViewModels
{
    public class DetailViewModel
    {
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }

        public DateTime ParkingDate { get; set; }

        public VehicleType VehicleType { get; set; }

        public string Color { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int NumberOfWheels { get; set; }


    }
}
