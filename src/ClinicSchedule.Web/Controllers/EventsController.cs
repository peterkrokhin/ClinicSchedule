using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using ClinicSchedule.Application;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using ClinicSchedule.Core;
using MediatR;

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

        // // GET api/patients/{id}/nearestavailabledate
        // [HttpGet("patients/{patientId:int}/nearestavailabledate")]
        // public async Task<IActionResult> Get(int patientId)
        // {
        //     Logger.LogInformation($"{DateTime.Now:o}, Request: {HttpContext.Request.Path}");

        //     var nearestAvailableDateEvents = await QuerryAggregator.GetAvailableDateEventsForAllPatientAppointmentsAsync(patientId);

        //     Logger.LogInformation($"{DateTime.Now:o}, Data for response: {JsonSerializer.Serialize(nearestAvailableDateEvents)}");
        //     Logger.LogInformation($"{DateTime.Now:o}, Response status code: 200");

        //     return Ok(nearestAvailableDateEvents);
        // }

        // // PATCH api/api/events/{id}, FromBody: appointmentId={id}
        // [HttpPatch("events/{eventId:int}")]
        // public async Task<IActionResult> Patch(int eventId, [FromBody] JsonDocument jsonDocument)
        // {
        //     Logger.LogInformation($"{DateTime.Now:o}, Request: {HttpContext.Request.Path}");
                  
        //     try
        //     {
        //         int appointmentId = jsonDocument.RootElement.GetProperty("appointmentId").GetInt32();
        //         Logger.LogInformation($"{DateTime.Now:o}, parameter accepted appointmentId={appointmentId}");

        //         await QuerryAggregator.TryLinkAppointmentToEventAsync(appointmentId, eventId);
        //         Logger.LogInformation($"{DateTime.Now:o}, patch successfully, response status code: 200");
                
        //         return Ok("patch successfully");
        //     }
        //     catch (Exception e)
        //     {
        //         Logger.LogInformation($"{DateTime.Now:o}, patch failure: {e.Message}, response status code: 404");
        //         return NotFound($"patch failure: {e.Message}");
        //     }
  
        // }

        [HttpPatch]
        public async Task<ActionResult> LinkAppointmentToEvent([FromBody] JsonPatchDocument<Event> doc)
        {
            var evnt = new Event();
            doc.ApplyTo(evnt);
            return Ok(evnt);
        }
    }
}