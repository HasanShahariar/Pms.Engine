using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PMS.Application.Common.Interfaces;
using PMS.Application.Common.Models;
using PMS.Domain.Entity;

namespace PMS.Application.Services.Parking.Commands
{
    public class CreateParkingEntryCommand:IRequest<IResult>
    {
        public string LicenseNumber { get; init; }
        public string VehicleType { get; init; }
        public string OwnerName { get; init; }
        public string OwnerPhone { get; init; }
        public string OwnerAddress { get; init; }
        public DateTime EntryTime { get; init; }
        public DateTime? ExitTime { get; init; }
        public decimal ParkingCharge { get; init; }
        public string Status { get; init; }  // "In" or "Out"
    }

    public class CreateParkingEntryCommandHandler : IRequestHandler<CreateParkingEntryCommand, IResult>
    {


        private readonly IAppDbContext _context;
        public CreateParkingEntryCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IResult> Handle(CreateParkingEntryCommand request, CancellationToken cancellationToken)
        {
            
                // Calculate parking charge based on vehicle type
                decimal parkingCharge = request.VehicleType switch
                {
                    "Car" => 20,
                    "Truck" => 40,
                    "Micro-bus" => 60,
                    "Bike" => 10,
                    _ => 15
                };

                var parkingEntry = new ParkingRecord
                {
                    LicenseNumber = request.LicenseNumber,
                    VehicleType = request.VehicleType,
                    OwnerName = request.OwnerName,
                    OwnerPhone = request.OwnerPhone,
                    OwnerAddress = request.OwnerAddress,
                    EntryTime = request.EntryTime,
                    ExitTime = request.ExitTime,
                    ParkingCharge = parkingCharge,
                    Status = request.Status
                };

                 _context.ParkingRecords.Add(parkingEntry);
                await _context.SaveChangesAsync(cancellationToken);



            return Result.CreateSuccess();
            
           
        }
    }
}
