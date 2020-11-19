using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using ClinicSchedule.Infrastructure;
using ClinicSchedule.Core;

namespace ClinicSchedule.Web
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchAppointmentsServiceController : Controller
    {
        public ISearchAppointmentsService SearchAppointmentsService { get; set; }

        public SearchAppointmentsServiceController(ISearchAppointmentsService searchAppointmentsService)
        {
            SearchAppointmentsService = searchAppointmentsService;
        }

        // GET api/SearchAppointmentsService/GetNotLinkedAppointmentsByPatientName/{patientName}
        [Route("[action]/{patientName:length(1, 100)}")]
        public async Task<IActionResult> GetNotLinkedAppointmentsByPatientName(string patientName)
        {
            var appointmentsResult = await SearchAppointmentsService.GetNotLinkedAppointmentsByPatientNameAsync(patientName);
            if (appointmentsResult.Count == 0)
            {
                return NotFound();
            }
            return Ok(appointmentsResult);
        }

        // GET api/SearchAppointmentsService/GetNotLinkedAppointmentsByPatientId/{patientId}
        [Route("[action]/{patientId:int}")]
        public async Task<IActionResult> GetNotLinkedAppointmentsByPatientId(int patientId)
        {
            var appointmentsResult = await SearchAppointmentsService.GetNotLinkedAppointmentsByPatientIdAsync(patientId);
            if (appointmentsResult.Count == 0)
            {
                return NotFound();
            }
            return Ok(appointmentsResult);
        }
    }
}