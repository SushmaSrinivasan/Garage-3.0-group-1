using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Exercise_12_Garage_2._0___part_1_Group1.Models
{
    public class Member
    {
        private long personnummer;
        [Key]
        [Range(0, 999999999999)]
        public long Personnummer {
            get => personnummer;
            set 
            {
                Personnummer = value;
                BirthDate = GetBirhtDateFromPersonnummer(value);
            }
        }

        [StringLength(30)]
        public string FirstName { get; set; } = default!;

        [StringLength(50)]
        public string LastName { get; set; } = default!;

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public Membership Membership { get; set; }

        private DateTime GetBirhtDateFromPersonnummer(long personnummer)
        {
            int year,
                month,
                day;
            int Modifiedpersonnummer;

            year = (int)(personnummer / 100000000);

            Modifiedpersonnummer = (int)(personnummer - (year * 100000000));

            month = Modifiedpersonnummer / 1000000;

            Modifiedpersonnummer -= month * 1000000;

            day = Modifiedpersonnummer / 10000;

            return new DateTime(year, month, day);

        }
    }
}
