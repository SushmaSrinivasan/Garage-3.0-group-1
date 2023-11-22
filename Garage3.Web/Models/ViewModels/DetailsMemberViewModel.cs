
using Garage3.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Web.Models.ViewModels
{
    public class DetailsMemberViewModel
    {
        public int Id { get; set; }

        public long Personnummer { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; } = default!;

        [Display(Name = "Last name")]
        public string LastName { get; set; } = default!;

        public int? Type { get; set; }

        [Display(Name = "Registration Number")]
        public string? RegistrationNumber { get; set; }

        [Display(Name = "Parking Time")]
        public DateTime? ParkTime { get; set; }

        [Display(Name = "Time Parked")]
        public string? TimeParked { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public Membership Membership { get; set; }

        public IEnumerable<OverviewVehicleItemListViewModel> Vehicles { get; set; } = new List<OverviewVehicleItemListViewModel>();
    }

}
