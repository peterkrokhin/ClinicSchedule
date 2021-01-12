using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using MediatR;
using ClinicSchedule.Application;
using ClinicSchedule.Core;

namespace ClinicSchedule.Web
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("{eventId:int}")]
        public async Task<ActionResult> LinkAppointmentToEvent([FromBody] JsonPatchDocument<Event> doc, int eventId)
        {
            var evnt = new Event();
            doc.ApplyTo(evnt, ModelState);

            if (evnt.AppointmentId == null)
                return BadRequest();

            await _mediator.Send(new LinkAppointmentToEventCommand(evnt.AppointmentId.Value, eventId));
            return NoContent();
        }
    }
}