using System;
using System.Collections.Generic;

namespace ClinicSchedule.Core
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }

        List<Appointment> Appointments { get; set; }
    }
}
