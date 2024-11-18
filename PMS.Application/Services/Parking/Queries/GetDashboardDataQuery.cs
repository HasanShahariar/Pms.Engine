using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Common.Interfaces;

namespace PMS.Application.Services.Parking.Queries
{
    public class GetDashboardDataQuery : IRequest<DashboardDataDto> 
    {
        public DateTime? Date { get; set; }
    }

    public class GetDashboardDataQueryHandler : IRequestHandler<GetDashboardDataQuery, DashboardDataDto>
    {
        private readonly IAppDbContext _context;

        public GetDashboardDataQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDataDto> Handle(GetDashboardDataQuery request, CancellationToken cancellationToken)
        {
            var date = request.Date.Value.Date;

            var totalCarsParked = await _context.ParkingRecords.CountAsync(record => record.EntryTime.Date == date, cancellationToken);
            var emptySlots = 100 - totalCarsParked; // Assuming a fixed total of 100 slots
            var vehicleTypeInfo = await _context.ParkingRecords
                .Where(record => record.EntryTime.Date == date)
                .GroupBy(record => record.VehicleType)
                .Select(group => new { VehicleType = group.Key, Count = group.Count() })
                .ToListAsync(cancellationToken);

            var vehiclesOverTwoHours = await _context.ParkingRecords
                .Where(record => record.Status == "In" && record.EntryTime.AddHours(2) < DateTime.Now)
                .ToListAsync(cancellationToken);

            return new DashboardDataDto
            {
                TotalCarsParked = totalCarsParked,
                EmptySlots = emptySlots,
                VehicleTypeInfo = vehicleTypeInfo,
                VehiclesOverTwoHours = vehiclesOverTwoHours.Count
            };
        }
    }

    public class DashboardDataDto
    {
        public int TotalCarsParked { get; set; }
        public int EmptySlots { get; set; }
        public object VehicleTypeInfo { get; set; }
        public int VehiclesOverTwoHours { get; set; }
    }

}
