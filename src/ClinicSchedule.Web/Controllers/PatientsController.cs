using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MediatR;

using EventsQuery = 
    ClinicSchedule.Application.Services.Events.Queries.GetAvailableDateEventsForAllPatientAppointments;
using AppointmentsIdQuery = 
    ClinicSchedule.Application.Services.Appointments.Queries.GetNotLinkedAppointmentsByPatientId;
using AppointmentsNameQuery = 
    ClinicSchedule.Application.Services.Appointments.Queries.GetNotLinkedAppointmentsByPatientName;

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
        public async Task<ActionResult<IEnumerable<AppointmentsIdQuery::Response>>> GetNotLinkedAppointmentsByPatientId(int patientId)
        {
            return Ok(await _mediator.Send(new AppointmentsIdQuery::Query(patientId)));
        }

        [HttpGet("name={patientName:length(1, 100)}/notlinkedappointments")]
        public async Task<ActionResult<IEnumerable<AppointmentsNameQuery::Response>>> GetNotLinkedAppointmentsByPatientName(string patientName)
        {
            return Ok(await _mediator.Send(new AppointmentsNameQuery::Query(patientName)));
        }

        [HttpGet("{patientId:int}/nearestavailabledate")]
        public async Task<ActionResult<EventsQuery::Response>> GetAvailableDateEventsForAllPatientAppointments(int patientId)
        {
            return await _mediator.Send(new EventsQuery::Query(patientId));
        }
    }
}