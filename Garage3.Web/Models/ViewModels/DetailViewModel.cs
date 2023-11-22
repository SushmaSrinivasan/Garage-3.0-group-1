using System.ComponentModel.DataAnnotations;
namespace Garage3.Web.Models.ViewModels
{
    public class DetailViewModel
    {
        public int Id { get; set; }

        public string Owner { get; set; } = string.Empty; //Member FullName

        public int OwnerId { get; set; }


        [Display(Name = "Type")]
        public string VehicleType { get; set; } = string.Empty;

        [Display(Name = "Registration number")]
        public string RegistrationNumber { get; set; } = string.Empty;

        [Display(Name = "Parking time")]
        public DateTime ParkingDate { get; set; }

        [Display(Name = "Time Parked")]
        public string TimeParked { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        [Display(Name = "Number of wheels")]
        public int NumberOfWheels { get; set; }

        public IEnumerable<int> Spots { get; set; } = new List<int>();


    }
}
