using System;
using System.Collections.Generic;

namespace ClinicSchedule.Application
{
    public class FindSuitableDateResponse
    {
        public DateTime? Date { get; set; }
        public IEnumerable<SuitableEvent> EventList { get; set; }

        public static FindSuitableDateResponse CreateEmpty()
        {
            return new FindSuitableDateResponse()
            {
                Date = null,
                EventList = new List<SuitableEvent>(){},
            };
        }
    }
}