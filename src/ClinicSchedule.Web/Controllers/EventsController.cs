using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using FluentValidation.AspNetCore;
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

        [HttpPatch("{eventId:int:min(1)}")]
        public async Task<ActionResult> LinkAppointmentToEvent([FromBody] JsonPatchDocument<Event> doc, int eventId)
        {
            var evnt = new Event();
            doc.ApplyTo(evnt, ModelState);

            var command = new LinkAppointmentToEventCommand(evnt.AppointmentId, eventId);
            var results = command.Validate();
            results.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
                return BadRequest(new ValidationProblemDetails(ModelState));
            
            await _mediator.Send(command);
            return Ok();
        }
    }
}