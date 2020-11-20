using System;

namespace ClinicSchedule.Core
{
    public class EventDTO
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int ServiceId { get; set; }
        public int? AppointmentId { get; set; }
    }
}
