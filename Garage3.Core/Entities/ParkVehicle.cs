using Garage3.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
public class ParkVehicle
{
    [Key]
    public int Id { get; set; }

    public int VehicleTypeId { get; set; }

    // Foreign key for Member (Personnummer)
    [ForeignKey(nameof(Personnummer))]
    public Member Owner { get; set; }

    [Remote("IsRegistrationNumberExists", "ParkVehicles", ErrorMessage = "Registration Number already exists!")]
    public string RegistrationNumber { get; set; }

    // Foreign key for MemberId
    public int MemberId { get; set; }

    // Navigation property for MembershipType
    public Membership MembershipType { get; set; }

    public DateTime ParkingDate { get; set; }

    public VehicleType VehicleType { get; set; }

    // Actual Personnummer value
    public long Personnummer { get; set; }

    [StringLength(50)]
    public string Color { get; set; } = string.Empty;

    [StringLength(15)]
    public string Brand { get; set; } = string.Empty;

    [StringLength(20)]
    public string Model { get; set; } = string.Empty;

    [Range(0, 10, ErrorMessage = "Wheels value should be within 0 and 10")]
    public int NumberOfWheels { get; set; }

    // Set ParkingDate to the current date and time
    public ParkVehicle()
    {
        ParkingDate = DateTime.Now;
    }

    public ICollection<ParkingSpace> Spots { get; set; }
}