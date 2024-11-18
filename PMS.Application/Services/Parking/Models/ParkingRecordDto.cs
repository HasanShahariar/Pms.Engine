using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Application.Services.Parking.Models
{
    public class ParkingRecordDto
    {
        public int Id { get; set; }
        public string LicenseNumber { get; set; }
        public string VehicleType { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }
        public string OwnerAddress { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public decimal ParkingCharge { get; set; }
        public string Status { get; set; }
    }
}
