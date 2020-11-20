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
    public class EventsController : Controller
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public EventsController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        // GET api/Events/GetAvailableDateEventsForAllPatientAppointments{patientId}
        [Route("[action]/{patientId:int}")]
        public async Task<IActionResult> GetAvailableDateEventsForAllPatientAppointments(int patientId)
        {
            try
            {
                var availableDateEventsResult = await UnitOfWork.GetAvailableDateEventsForAllPatientAppointmentsAsync(patientId);
                return Ok(availableDateEventsResult);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET api/Events/LinkAppointmentToEvent/{patientId}
        [Route("[action]/{patientId:int}/{eventId:int}")]
        public async Task<IActionResult> LinkAppointmentToEvent(int patientId, int eventId)
        {
            string linkResult = await UnitOfWork.LinkAppointmentToEventAsync(patientId, eventId);
            return Ok(linkResult); 
        }


        protected override void Dispose(bool disposing)
        {
            UnitOfWork.Dispose();
            base.Dispose(disposing);
        }

    }
}