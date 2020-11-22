using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using ClinicSchedule.Core;

namespace ClinicSchedule.Web
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : Controller
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public AppointmentsController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        // GET api/Appointments/GetNotLinkedAppointmentsByPatientName/{patientName}
        [Route("[action]/{patientName:length(1, 100)}")]
        public async Task<IActionResult> GetNotLinkedAppointmentsByPatientName(string patientName)
        {
            var appointments = await UnitOfWork.Appointments.GetNotLinkedAppointmentsByPatientNameAsync(patientName);

            if (appointments.Count() == 0)
            {
                return NotFound();
            }
            var appointmentsResult = UnitOfWork.Appointments.ConvertAllToDTO(appointments);
            return Ok(appointmentsResult);
        }

        // GET api/Appointments/GetNotLinkedAppointmentsByPatientId/{patientId}
        [Route("[action]/{patientId:int}")]
        public async Task<IActionResult> GetNotLinkedAppointmentsByPatientId(int patientId)
        {
            var appointments = await UnitOfWork.Appointments.GetNotLinkedAppointmentsByPatientIdAsync(patientId);
            if (appointments.Count() == 0)
            {
                return NotFound();
            }
            var appointmentsResult = UnitOfWork.Appointments.ConvertAllToDTO(appointments);
            return Ok(appointmentsResult);
        }

        protected override void Dispose(bool disposing)
        {
            UnitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}