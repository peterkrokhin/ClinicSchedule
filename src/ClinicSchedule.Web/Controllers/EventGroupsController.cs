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
    public class EventGroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventGroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FindManyEventGroups>>> Get(int? patientId, bool? isLinked, bool? getNearest)
        {
            var appointments = await _mediator.Send(new FindManyAppointmentsQuery(patientId, patientName, isLinked));
            return Ok(appointments);
        }
    }
}