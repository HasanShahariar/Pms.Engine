using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Common.Interfaces;
using PMS.Application.Services.Parking.Models;

namespace PMS.Application.Services.Parking.Queries
{
    public class GetParkingRecordByIdQuery : IRequest<ParkingRecordDto>
    {
        public int Id { get; set; }
    }

    public class GetParkingRecordByIdQueryHandler : IRequestHandler<GetParkingRecordByIdQuery, ParkingRecordDto>
    {
        private readonly IAppDbContext _context;

        public GetParkingRecordByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ParkingRecordDto> Handle(GetParkingRecordByIdQuery request, CancellationToken cancellationToken)
        {
            var parkingRecord = await _context.ParkingRecords
                .Where(p => p.Id == request.Id)
                .Select(p => new ParkingRecordDto
                {
                    Id = p.Id,
                    LicenseNumber = p.LicenseNumber,
                    VehicleType = p.VehicleType,
                    OwnerName = p.OwnerName,
                    OwnerPhone = p.OwnerPhone,
                    OwnerAddress = p.OwnerAddress,
                    EntryTime = p.EntryTime,
                    ExitTime = p.ExitTime,
                    ParkingCharge = p.ParkingCharge,
                    Status = p.Status
                })
                .FirstOrDefaultAsync(cancellationToken);

            return parkingRecord;
        }
    }
}
