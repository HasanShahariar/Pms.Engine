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
    public class GetAllParkingRecordsQuery : IRequest<List<ParkingRecordDto>>
    {
        public DateTime? Date { get; set; }
    }

    public class GetAllParkingRecordsQueryHandler : IRequestHandler<GetAllParkingRecordsQuery, List<ParkingRecordDto>>
    {
        private readonly IAppDbContext _context;

        public GetAllParkingRecordsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ParkingRecordDto>> Handle(GetAllParkingRecordsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.ParkingRecords.AsQueryable();

            if (request.Date.HasValue)
            {
                var date = request.Date.Value.Date;
                query = query.Where(record => record.EntryTime.Date == date);
            }

            return await query
                .Select(record => new ParkingRecordDto
                {
                    Id = record.Id,
                    OwnerName = record.OwnerName,
                    VehicleType = record.VehicleType,
                    LicenseNumber = record.LicenseNumber,
                    EntryTime = record.EntryTime,
                    ExitTime = record.ExitTime,
                    Status = record.Status
                })
                .ToListAsync(cancellationToken);
        }
    }

    public class ParkingRecordDto
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string VehicleType { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public string Status { get; set; }
    }

}
