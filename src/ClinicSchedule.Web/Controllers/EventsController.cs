using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using MediatR;
using ClinicSchedule.Application;

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
        public async Task<ActionResult> LinkAppointmentToEvent([FromBody] JsonPatchDocument<EventPatchDTO> doc, int eventId)
        {
            var evnt = new EventPatchDTO();
            doc.ApplyTo(evnt, ModelState);

            await _mediator.Send(new LinkAppointmentToEventCommand(evnt.AppointmentId, eventId));
            return NoContent();
        }
    }
}