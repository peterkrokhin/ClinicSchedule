using System;
using System.Runtime.Serialization;

namespace ClinicSchedule.Application
{
    public class AppointmentAlreadyLinkedException : Exception
    {
        public AppointmentAlreadyLinkedException()
        {
        }

        public AppointmentAlreadyLinkedException(string message) : base(message)
        {
        }

        public AppointmentAlreadyLinkedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AppointmentAlreadyLinkedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}