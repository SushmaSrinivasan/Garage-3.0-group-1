using Garage3.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Web.Models.ViewModels
{
    public partial class OverviewMemberViewModel
    {
        public int Id { get; set; }

        public long Personnummer { get; set; }
        [Display(Name = "First name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last name")]
        public string? LastName { get; set; }

        public string? Fullname { get; set; }

        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }

        public Membership? Membership { get; set; }

        [Display(Name = "Vehicles")]
        public int VehicleCount { get; set; }

        public IEnumerable<MemberIndexViewModel> Members { get; set; } = new List<MemberIndexViewModel>();

        public static SortParam SortParams { get; private set; } = new SortParam();

        public string Sort { get; set; } = SortParams.FirstName;

        public string? By { get; set; }

        public string? Search { get; set; }

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
            public string Personnummer { get => "Pnr"; }
            public string FirstName { get => "FirstName"; }
            public string LastName { get => "LastName"; }
            public string Membership { get => "Membership"; }
            public string BirthDate { get => "BirthDate"; }
            public string VehicleCount { get => "Vehicles"; }
            public string DescendingSuffix { get => "_Desc"; }

        }
    }
}
