using System;
using System.Collections.Generic;

namespace ClinicSchedule.Core
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
    }
}