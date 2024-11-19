using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PMS.Application.Common.Interfaces;
using PMS.Application.Common.Models;


public class UpdateParkingEntryCommand : IRequest<IResult>
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

public class UpdateParkingEntryCommandHandler : IRequestHandler<UpdateParkingEntryCommand, IResult>
{
    private readonly IAppDbContext _context;

    public UpdateParkingEntryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IResult> Handle(UpdateParkingEntryCommand request, CancellationToken cancellationToken)
    {
        var record = await _context.ParkingRecords.FindAsync(request.Id);

        if (record == null)
            return Result.CreateFailure("Parking record not found");

        record.LicenseNumber = request.LicenseNumber;
        record.VehicleType = request.VehicleType;
        record.OwnerName = request.OwnerName;
        record.OwnerPhone = request.OwnerPhone;
        record.OwnerAddress = request.OwnerAddress;
        record.EntryTime = request.EntryTime;

        if (record.Status == "In" && request.Status=="Out")
        {
            record.ExitTime = DateTime.Now;
        }
        record.Status = request.Status;
        record.ParkingCharge = request.ParkingCharge;



        _context.ParkingRecords.Update(record);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.CreateSuccess();
    }
}


