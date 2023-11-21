using Garage3.Core.Entities;
using Garage3.Web.Validations;
using System.ComponentModel.DataAnnotations;

namespace Garage3.Web.Models.ViewModels
{
    public class AddMemberViewModel
    {
        public long Personnummer { get; set; }

        [StringLength(30)]
        public string FirstName { get; set; } = default!;

        [StringLength(50)]
        [CheckFnameLname(ErrorMessage ="First name and last name can't be the same")]
        public string LastName { get; set; } = default!;


        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public Membership Membership { get; set; }

    }
}
