namespace Garage3.Web.Models.ViewModels
{
    public class ListViewModel
    {
        public int ParkVehicleId { get; set; }
        public string Owner { get; set; } = default!;

        public long OwnerPersonnummer { get; set; }
        public string Membership { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string RegistrationNumber { get; set; } = default!;
        public DateTime ParkTime { get; set; } = default!;

        public string TimeParked
        {
            get
            {
                var timePassed = DateTime.Now - ParkTime; // Gets the amount of time by comparing current date with date of parking.
                var hoursRoundedDown = (int)Math.Floor(timePassed.TotalHours); // Converts timePassed and rounds down its Hours to a full number.
                var minutesRoundedDown = (int)Math.Floor((timePassed.TotalMinutes - (hoursRoundedDown * 60))); // Rounds down and Resets Minutes every 60 minutes
                return $"{hoursRoundedDown} hours {minutesRoundedDown} minutes";
            }
        }
    }
}
