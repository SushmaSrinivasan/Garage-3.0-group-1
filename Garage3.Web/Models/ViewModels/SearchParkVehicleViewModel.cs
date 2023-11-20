using Garage3.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Web.Models.ViewModels
{
    public partial class SearchParkVehicleViewModel
    { 
        public string? Owner { get; set; } //Member FullName
        public string? Membership { get; set; }
        public string? Type { get; set; }
        
        [Display(Name = "Registration Number")]
        public string? RegistrationNumber { get; set; }

        [Display(Name = "Parking Time")]
        public DateTime? ParkTime { get; set; }

        [Display(Name = "Time Parked")]
        public string? TimeParked;
        public string? Sort { get; set; }

        public IEnumerable<ListViewModel> Vehicles { get; set; } = new List<ListViewModel>();

        public static SortParam SortParams { get; private set; } = new SortParam();

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
