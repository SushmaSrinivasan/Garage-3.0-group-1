using System.ComponentModel.DataAnnotations;

namespace Exercise_12_Garage_2._0___part_1_Group1.Models
{
    public class VehicleType
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int Spaces { get; set; }
    }
}
