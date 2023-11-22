using Garage3.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Web.Models.ViewModels
{
    public partial class OverviewVehicleViewModel
    {
        public string? Owner { get; set; } //Member FullName
        public string? Membership { get; set; }
        public int Type { get; set; }

        [Display(Name = "Registration Number")]
        public string? RegistrationNumber { get; set; }

        [Display(Name = "Parking Time")]
        public DateTime? ParkTime { get; set; }

        [Display(Name = "Time Parked")]
        public string? TimeParked;
        public static SortParam SortParams { get; private set; } = new SortParam();

        public string Sort { get; set; } = SortParams.Owner;

        public string? By { get; set; }

        public string? Search { get; set; }

        public IEnumerable<OverviewVehicleItemListViewModel> Vehicles { get; set; } = new List<OverviewVehicleItemListViewModel>();

        public IEnumerable<SelectListItem> VehicleTypes { get; set; } = new List<SelectListItem>();


        public string GetSortSymbol(string sortParam)
        {
            if (sortParam == null || !Sort.StartsWith(sortParam))
            {
                return "d-none";
            }
            else if (!Sort.EndsWith(SortParams.DescendingSuffix))
            {
                return "bi bi-caret-up-fill";
            }
            else
            {
                return "bi bi-caret-down-fill";
            }
        }

        public class SortParam
        {
            public string Owner { get => "Owner"; }
            public string Membership { get => "Membership"; }
            public string Type { get => "Type"; }
            public string RegistrationNumber { get => "RegNum"; }
            public string ParkTime { get => "ParkTime"; }
            public string DescendingSuffix { get => "_Desc"; }

        }
    }
}
