using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PMS.Application.Common.Interfaces;
using PMS.Domain.Entity;

namespace PMS.Infrastructure;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ParkingRecord> ParkingRecords => Set<ParkingRecord>();

}
