using System.ComponentModel.DataAnnotations;

namespace Garage3.Core.Entities
{
    public class VehicleType
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int Spaces { get; set; }

        [StringLength(15)]
        public string Name { get; set; }

        public virtual ParkingSpace ParkingSpace { get; set; }

    }
}
