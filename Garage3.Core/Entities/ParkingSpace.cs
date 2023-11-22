namespace Garage3.Core.Entities
{
    public class ParkingSpace
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public ParkVehicle Vehicle { get; set; }
        //public int SpotNumber { get; set; }
        //public bool IsOccupied { get; set; }

    }
}
