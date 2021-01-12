using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;

using ClinicSchedule.Application;

namespace ClinicSchedule.Web
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{patientId:int}/notlinkedappointments")]
        public async Task<ActionResult<IEnumerable<FindManyAppointmentsResponse>>> FindManyAppointmentsByPatientId(int patientId)
        {
            return Ok(await _mediator.Send(new FindManyAppointmentsQuery(){PatientId = patientId, IsLinked = false}));
        }

        [HttpGet("name={patientName:length(1, 100)}/notlinkedappointments")]
        public async Task<ActionResult<IEnumerable<FindManyAppointmentsResponse>>> FindManyAppointmentsByPatientName(string patientName)
        {
            return Ok(await _mediator.Send(new FindManyAppointmentsQuery(){PatientName = patientName, IsLinked = false}));
        }

        [HttpGet("{patientId:int}/suitabledate")]
        public async Task<ActionResult<FindSuitableDateResponse>> FindSuitableDateForAllNotLinkedPatientAppointments(int patientId)
        {
            return await _mediator.Send(new FindSuitableDateQuery(patientId));
        }
    }
}