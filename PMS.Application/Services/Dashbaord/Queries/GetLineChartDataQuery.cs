using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Application.Common.Interfaces;
using PMS.Application.Services.Dashbaord.Models;

namespace PMS.Application.Services.Dashbaord.Queries
{
    public class GetLineChartDataQuery : IRequest<List<LineChartDataDto>>
    {
    }

    public class GetLineChartDataQueryHandler : IRequestHandler<GetLineChartDataQuery, List<LineChartDataDto>>
    {
        private readonly IAppDbContext _context;

        public GetLineChartDataQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LineChartDataDto>> Handle(GetLineChartDataQuery request, CancellationToken cancellationToken)
        {
            // Query to group the records by EntryTime date and count the number of parked cars
            var dailyData = await _context.ParkingRecords
                .GroupBy(p => p.EntryTime.Date)
                .Select(g => new LineChartDataDto
                {
                    Date = g.Key,
                    Count = g.Count()
                })
                .OrderBy(d => d.Date)
                .ToListAsync(cancellationToken);

            return dailyData;
        }
    }
}
