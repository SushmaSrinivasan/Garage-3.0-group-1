using Garage3.Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Web.Models.ViewModels
{
    public class OverviewParkingLotViewModel
    {
        [Display(Name = "State")]
        public ParkingSpaceFilters StateFilter { get; set; }

        public IEnumerable<OverviewParkingSpaceViewModel> spots { get; set; }

        public string? State { get; set; }
        [Display(Name = "Parked Vehicle's type")]
        public string? VehicleType { get; set; }

        [Display(Name = "Parked Vehicle's Registration number")]
        public string? VehicleRegistrationNumber { get; set; }

        public string? Spot { get; set; }


    }
}
