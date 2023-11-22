using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

public class EditParkVehicleViewModel
{
    [Key]
    public int Id { get; set; }

    [Remote("IsRegistrationNumberExists", "ParkVehicles", AdditionalFields = "ExistingRegistrationNumber", ErrorMessage = "Registration Number already exists!")]
    public string RegistrationNumber { get; set; }

    public string ExistingRegistrationNumber { get; set; }
    public DateTime ParkingDate { get; set; }

    public int VehicleTypeId { get; set; }

    public string Color { get; set; } = string.Empty;

    [StringLength(15)]
    public string Brand { get; set; } = string.Empty;

    [StringLength(20)]
    public string Model { get; set; } = string.Empty;

    [Range(0, 10, ErrorMessage = "Wheels value should be within 0 and 10")]
    public int NumberOfWheels { get; set; }
}