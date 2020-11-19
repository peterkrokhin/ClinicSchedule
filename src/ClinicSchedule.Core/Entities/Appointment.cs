using System;
using System.Collections.Generic;

namespace ClinicSchedule.Core
{
    public class Appointment
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public List<Event> Events { get; set; }
    }
}
