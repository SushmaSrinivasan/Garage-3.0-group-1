using Garage3.Core.Entities;

namespace Garage3.Web.Models.ViewModels
{
    internal class MemberIndexViewModel
    {
        public int Id { get; set; }
        public long Personnummer { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public Membership Membership { get; set; }
    }
}