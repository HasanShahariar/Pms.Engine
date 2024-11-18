using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PMS.Domain.Entity;


namespace PMS.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<ParkingRecord> ParkingRecords { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);


}
