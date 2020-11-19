using System;
using System.Collections.Generic;

namespace ClinicSchedule.Core
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }

        List<Appointment> Appointments { get; set; }

        List<Event> Events { get; set; }
    }
}
