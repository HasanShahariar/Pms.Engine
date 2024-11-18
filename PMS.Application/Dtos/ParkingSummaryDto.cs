using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Dtos
{
    public class ParkingSummaryDto
    {
        public int TotalCarsParked { get; set; }
        public int EmptySlots { get; set; }
        public Dictionary<string, int> VehicleTypeInfo { get; set; }
    }
}
