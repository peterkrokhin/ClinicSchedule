using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MediatR;

using ClinicSchedule.Application;

namespace ClinicSchedule.Web
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FindManyAppointmentsResponse>>> Get(int? patientId, 
            string patientName, bool? isLinked)
        {
            var appointments = await _mediator.Send(new FindManyAppointmentsQuery(patientId, patientName, isLinked));
            return Ok(appointments);
        }
    }
}