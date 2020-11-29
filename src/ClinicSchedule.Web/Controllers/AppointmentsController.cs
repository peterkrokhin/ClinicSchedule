using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using ClinicSchedule.Core;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ClinicSchedule.Web
{
    [ApiController]
    [Route("api/")]
    public class AppointmentsController : Controller
    {
        private IUnitOfWork UnitOfWork { get; set; }
        private ILogger Logger { get; set; }

        public AppointmentsController(IUnitOfWork unitOfWork, ILogger<AppointmentsController> logger)
        {
            UnitOfWork = unitOfWork;
            Logger = logger;
        }

        // GET api/patients/name={name}/notlinkedappointments
        [HttpGet("patients/name={patientName:length(1, 100)}/notlinkedappointments")]
        public async Task<IActionResult> Get(string patientName)
        {
            Logger.LogInformation($"{DateTime.Now:o}, Request: {HttpContext.Request.Path}");

            var notLinkedAppointments = await UnitOfWork.Appointments.GetNotLinkedAppointmentsByPatientNameAsync(patientName);
            var notLinkedAppointmentsDTO = UnitOfWork.Appointments.ConvertAllToDTO(notLinkedAppointments);

            Logger.LogInformation($"{DateTime.Now:o}, Data for response: {JsonSerializer.Serialize(notLinkedAppointmentsDTO)}");
            Logger.LogInformation($"{DateTime.Now:o}, Response status code: 200");

            return Ok(notLinkedAppointmentsDTO);
        }

        // GET api/patients/{id}/notlinkedappointments
        [HttpGet("patients/{patientId:int}/notlinkedappointments")]
        public async Task<IActionResult> Get(int patientId)
        {
            Logger.LogInformation($"{DateTime.Now:o}, Request: {HttpContext.Request.Path}");

            var notLinkedAppointments = await UnitOfWork.Appointments.GetNotLinkedAppointmentsByPatientIdAsync(patientId);
            var notLinkedAppointmentsDTO = UnitOfWork.Appointments.ConvertAllToDTO(notLinkedAppointments);
            
            Logger.LogInformation($"{DateTime.Now:o}, Data for response: {JsonSerializer.Serialize(notLinkedAppointmentsDTO)}");
            Logger.LogInformation($"{DateTime.Now:o}, Response status code: 200");
            
            return Ok(notLinkedAppointmentsDTO);
        }
    }
}