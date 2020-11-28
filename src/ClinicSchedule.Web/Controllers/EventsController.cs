using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using ClinicSchedule.Infrastructure;
using ClinicSchedule.Core;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ClinicSchedule.Web
{
    [ApiController]
    [Route("api/")]
    public class EventsController : Controller
    {
        private IUnitOfWork UnitOfWork { get; set; }
        private ILogger Logger { get; set; }

        public EventsController(IUnitOfWork unitOfWork, ILogger<EventsController> logger)
        {
            UnitOfWork = unitOfWork;
            Logger = logger;
        }

        // GET api/patients/{id}/nearestavailabledate
        [Route("patients/{patientId:int}/nearestavailabledate")]
        public async Task<IActionResult> GetAvailableDateEventsForAllPatientAppointments(int patientId)
        {
            Logger.LogInformation($"{DateTime.Now:o}, Request: {HttpContext.Request.Path}");

            var nearestAvailableDateEvents = await UnitOfWork.GetAvailableDateEventsForAllPatientAppointmentsAsync(patientId);

            Logger.LogInformation($"{DateTime.Now:o}, Data for response: {JsonSerializer.Serialize(nearestAvailableDateEvents)}");
            Logger.LogInformation($"{DateTime.Now:o}, Response status code: 200");

            return Ok(nearestAvailableDateEvents);
        }

        // GET api/apievents/{id}/{id}
        [Route("events/{eventId:int}/{appointmentId:int}")]
        public async Task<IActionResult> TryLinkAppointmentToEvent(int eventId, int appointmentId)
        {
            Logger.LogInformation($"{DateTime.Now:o}, Request: {HttpContext.Request.Path}");
            
            try
            {
                await UnitOfWork.TryLinkAppointmentToEventAsync(appointmentId, eventId);
                Logger.LogInformation($"{DateTime.Now:o}, Message: done, Response status code: 200");
                return Ok("done");
            }
            catch (Exception e)
            {
                Logger.LogInformation($"{DateTime.Now:o}, Message: {e.Message}, Response status code: 404");
                return NotFound(e.Message);
            }
            
        }
    }
}