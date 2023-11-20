using Garage3.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage3.Persistence.Configurations
{
    public class ParkVehicleConfigurations : IEntityTypeConfiguration<ParkVehicle>
    {
        public void Configure(EntityTypeBuilder<ParkVehicle> builder)
        {
            throw new NotImplementedException();
        }
    }
}
