using System;
using System.Collections.Generic;

namespace ClinicSchedule.Application.Services.Events.Queries.GetAvailableDateEventsForAllPatientAppointments
{
    public class Response
    {
        public DateTime? Date { get; set; }
        public IEnumerable<EventResponse> EventList { get; set; }

        public static Response CreateEmpty()
        {
            return new Response()
            {
                Date = null,
                EventList = new List<EventResponse>(){},
            };
        }
    }
}