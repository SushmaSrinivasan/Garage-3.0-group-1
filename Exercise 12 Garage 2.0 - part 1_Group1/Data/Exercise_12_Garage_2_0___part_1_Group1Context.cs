using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Exercise_12_Garage_2._0___part_1_Group1.Models;

namespace Exercise_12_Garage_2._0___part_1_Group1.Data
{
    public class Exercise_12_Garage_2_0___part_1_Group1Context : DbContext
    {
        public Exercise_12_Garage_2_0___part_1_Group1Context (DbContextOptions<Exercise_12_Garage_2_0___part_1_Group1Context> options)
            : base(options)
        {
        }

        public DbSet<Exercise_12_Garage_2._0___part_1_Group1.Models.ParkVehicle> ParkVehicle { get; set; } = default!;
    }
}
