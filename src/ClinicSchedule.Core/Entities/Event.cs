using System;

namespace ClinicSchedule.Core
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        public int? AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
