using System;
using System.Runtime.Serialization;

namespace ClinicSchedule.Application
{
    public class AppointmentNotFoundException : Exception
    {
        public AppointmentNotFoundException()
        {
        }

        public AppointmentNotFoundException(string message) : base(message)
        {
        }

        public AppointmentNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AppointmentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}