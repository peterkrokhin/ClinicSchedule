using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicSchedule.Core
{
    public class AvailableDateEvents
    {
        public DateTime? AvailableDate { get; set; }
        public IEnumerable<EventDTO> AvailableEventsList { get; set; }

        public static AvailableDateEvents CreateEmpty()
        {
            return new AvailableDateEvents()
            {
                AvailableDate = null,
                AvailableEventsList = new List<EventDTO>(){},
            };
        }

        public bool IsEmty()
        {
            if (AvailableDate == null & 
                (AvailableEventsList != null & AvailableEventsList.Count() == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       
    }
}