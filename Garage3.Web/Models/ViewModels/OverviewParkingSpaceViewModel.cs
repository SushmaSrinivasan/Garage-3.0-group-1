using System.ComponentModel.DataAnnotations;

namespace Garage3.Web.Models.ViewModels
{
    public class OverviewParkingSpaceViewModel
    {
        public int MinSpot { get; set; }

        public int MaxSpot { get; set; }

        public string Spot {
            get
            {
                return (MinSpot == MaxSpot ? MinSpot.ToString() : $"{MinSpot} - {MaxSpot}");
            }
        }
        public int VehicleId { get; set; }

        public string State {
            get 
            {
                return (string.IsNullOrWhiteSpace(VehicleRegistrationNumber) ? "Free" : "Occupied");
            } 
        }

        public string VehicleType { get; set; } = string.Empty;

        public string VehicleRegistrationNumber { get; set; } = string.Empty;
    }
}
