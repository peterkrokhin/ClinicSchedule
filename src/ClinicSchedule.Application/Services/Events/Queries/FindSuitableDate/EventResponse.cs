using System;

namespace ClinicSchedule.Application
{
    public class EventResponse
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int ServiceId { get; set; }
        public int? AppointmentId { get; set; }
    }
}
