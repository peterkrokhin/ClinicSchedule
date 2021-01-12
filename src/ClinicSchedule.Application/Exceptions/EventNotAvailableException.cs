using System;
using System.Runtime.Serialization;

namespace ClinicSchedule.Application
{
    public class EventNotAvailableException : Exception
    {
        public EventNotAvailableException()
        {
        }

        public EventNotAvailableException(string message) : base(message)
        {
        }

        public EventNotAvailableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EventNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}