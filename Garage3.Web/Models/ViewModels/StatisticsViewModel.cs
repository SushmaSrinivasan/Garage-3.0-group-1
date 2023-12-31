﻿using Garage3.Core.Entities;

public class StatisticsViewModel
{
    public int TotalWheels { get; set; }
    public double TotalRevenue { get; set; }
    public Dictionary<string, int> VehicleTypeAmount { get; set; }
    public int NumberOfMembers { get; set; }
    public int OccupiedParkingSpots { get; set; }
    public int TotalParkingSpots { get; set; }
}