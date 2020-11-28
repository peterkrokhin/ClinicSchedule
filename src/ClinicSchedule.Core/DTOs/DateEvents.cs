using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicSchedule.Core
{
    public class DateEvents
    {
        public DateTime? Date { get; set; }
        public IEnumerable<EventDTO> EventList { get; set; }

        public static DateEvents CreateEmpty()
        {
            return new DateEvents()
            {
                Date = null,
                EventList = new List<EventDTO>(){},
            };
        }

        public bool IsEmty()
        {
            if (Date == null & (EventList != null & EventList.Count() == 0))
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