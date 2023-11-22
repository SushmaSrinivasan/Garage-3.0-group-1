using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Garage3.Core.Entities
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        // Navigation property for ParkVehicles
        public ICollection<ParkVehicle> Vehicles { get; set; }

        public long Personnummer { get; set; }

        public int Age { get; set; }


        [StringLength(30)]
        public string FirstName { get; set; } = default!;

        [StringLength(50)]
        public string LastName { get; set; } = default!;

        public string FullName { get => $"{FirstName} {LastName}"; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public Membership Membership { get; set; }


        
    }
}
