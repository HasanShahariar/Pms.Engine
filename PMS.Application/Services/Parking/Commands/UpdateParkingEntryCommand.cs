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
    public DateTime ExitTime { get; set; }
    public string Status { get; set; } // "Out"
    public decimal ParkingCharge { get; set; }
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

        record.ExitTime = request.ExitTime;
        record.Status = request.Status;
        record.ParkingCharge = request.ParkingCharge;

        _context.ParkingRecords.Update(record);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.CreateSuccess();
    }
}


