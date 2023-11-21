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

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Fullname { get; set; }

        public DateTime BirthDate { get; set; }

        public Membership Membership { get; set; }

        public int VehicleCount { get; set; }

        public static SortParam SortParams { get; private set; } = new SortParam();

        public string Sort { get; set; } = SortParams.FirstName;

        public string? By { get; set; }

        public string? Search { get; set; }

        public IEnumerable<SelectListItem> VehicleTypes { get; set; } = new List<SelectListItem>();


        public string GetSortSymbol(string sortParam)
        {
            if (sortParam == null || !Sort.StartsWith(sortParam))
            {
                return "";
            }
            else if (!Sort.EndsWith(SortParams.DescendingSuffix))
            {
                return " - a";
            }
            else
            {
                return " - d";
            }
        }

        public class SortParam
        {
            public string FirstName { get => "FirstName"; }
            public string LastName { get => "LastName"; }
            public string Membership { get => "Membership"; }
            public string BirthDate { get => "BirthDate"; }
            public string DescendingSuffix { get => "_Desc"; }

        }
    }
}
