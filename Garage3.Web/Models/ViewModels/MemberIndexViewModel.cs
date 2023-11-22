using Garage3.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Web.Models.ViewModels
{
    public class MemberIndexViewModel
    {
        public int Id { get; set; }
        public long Personnummer { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int VehicleCount { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public Membership Membership { get; set; }
    }
}