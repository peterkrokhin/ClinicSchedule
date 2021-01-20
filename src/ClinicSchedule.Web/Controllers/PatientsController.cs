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

        [HttpGet("{patientId:int:min(1)}/notlinkedappointments")]
        public async Task<ActionResult<IEnumerable<FindManyAppointmentsResponse>>> FindManyAppointmentsByPatientId(int patientId)
        {
            var query = new FindManyAppointmentsQuery()
            {
                PatientId = patientId,
                IsLinked = false
            };

            return Ok(await _mediator.Send(query));
        }

        [HttpGet("name={patientName:length(1, 100)}/notlinkedappointments")]
        public async Task<ActionResult<IEnumerable<FindManyAppointmentsResponse>>> FindManyAppointmentsByPatientName(string patientName)
        {
            var query = new FindManyAppointmentsQuery()
            {
                PatientName = patientName,
                IsLinked = false
            };

            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{patientId:int:min(1)}/suitabledate")]
        public async Task<ActionResult<FindSuitableDateResponse>> FindSuitableDateForAllNotLinkedPatientAppointments(int patientId)
        {
            var query = new FindSuitableDateQuery(patientId);

            return await _mediator.Send(query);
        }
    }
}