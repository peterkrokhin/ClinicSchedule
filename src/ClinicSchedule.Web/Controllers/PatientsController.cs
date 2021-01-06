using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MediatR;

using ClinicSchedule.Application;
using EventsQuery = 
    ClinicSchedule.Application.Services.Events.Queries.GetAvailableDateEventsForAllPatientAppointments;

namespace ClinicSchedule.Web
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{patientId:int}/notlinkedappointments")]
        public async Task<ActionResult<IEnumerable<GetNotLinkedAppointmentsByPatientIdResponse>>> GetNotLinkedAppointmentsByPatientId(int patientId)
        {
            return Ok(await _mediator.Send(new GetNotLinkedAppointmentsByPatientIdQuery(patientId)));
        }

        [HttpGet("name={patientName:length(1, 100)}/notlinkedappointments")]
        public async Task<ActionResult<IEnumerable<GetNotLinkedAppointmentsByPatientNameResponse>>> GetNotLinkedAppointmentsByPatientName(string patientName)
        {
            return Ok(await _mediator.Send(new GetNotLinkedAppointmentsByPatientNameQuery(patientName)));
        }

        [HttpGet("{patientId:int}/nearestavailabledate")]
        public async Task<ActionResult<EventsQuery.Response>> GetAvailableDateEventsForAllPatientAppointments(int patientId)
        {
            return await _mediator.Send(new EventsQuery.Query(patientId));
        }
    }
}