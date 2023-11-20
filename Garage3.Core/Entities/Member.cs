using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Garage3.Core.Entities
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        private long personnummer;
        
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Personnummer
        {
            get => personnummer;
            set
            {
                personnummer = value;
                BirthDate = GetBirhtDateFromPersonnummer(value);
            }
        }

        [StringLength(30)]
        public string FirstName { get; set; } = default!;

        [StringLength(50)]
        public string LastName { get; set; } = default!;

        public string FullName { get => $"{FirstName} {LastName}"; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public Membership Membership { get; set; }

        public ICollection<ParkVehicle>? Vehicles { get; set; }

        private DateTime GetBirhtDateFromPersonnummer(long personnummer)
        {
            int year,
                month,
                day;
            int Modifiedpersonnummer;

            year = (int)(personnummer / 100000000);

            Modifiedpersonnummer = (int)(personnummer - year * 100000000);

            month = Modifiedpersonnummer / 1000000;

            Modifiedpersonnummer -= month * 1000000;

            day = Modifiedpersonnummer / 10000;

            return DateTime.Now;

        }
    }
}
