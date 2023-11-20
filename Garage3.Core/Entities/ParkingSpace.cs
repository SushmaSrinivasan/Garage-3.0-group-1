namespace Garage3.Core.Entities
{
    public class ParkingSpace
    {
        public int Id { get; set; }
        public ParkVehicle ParkVehicle { get; set; }
        public string? Name { get; set; }
    }
}
