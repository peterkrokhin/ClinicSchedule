using System;
using System.Collections.Generic;

namespace ClinicSchedule.Core
{
    public class AvailableDateEvents
    {
        public DateTime? AvailableDate { get; set; }
        public IEnumerable<EventDTO> AvailableEventsList { get; set; } 
    }
}