using MediatR;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Services.Parking.Commands;
using PMS.Application.Services.Parking.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PMS.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ParkingController : ApiControllerBase
    {
       

        public ParkingController(ISender sender):base(sender)
        {
            
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateParkingEntryCommand command)
        {
            // Send the command to the MediatR pipeline
            var result = await Mediator.Send(command);
            return Ok(); 
                 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateParkingEntryCommand command)
        {
            command.Id = id; // Ensure the ID is set in the command
            var result = await Mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DateTime? date = null)
        {
            var query = new GetAllParkingRecordsQuery { Date = date };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetDashboardData()
        {
            var result = await Mediator.Send(new GetDashboardDataQuery());
            return Ok(result);
        }
    }
}
