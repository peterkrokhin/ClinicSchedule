using System;
using System.Runtime.Serialization;

namespace ClinicSchedule.Application
{
    public class ServicesAppointmentEventDontMatchException : Exception
    {
        public ServicesAppointmentEventDontMatchException()
        {
        }

        public ServicesAppointmentEventDontMatchException(string message) : base(message)
        {
        }

        public ServicesAppointmentEventDontMatchException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServicesAppointmentEventDontMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}