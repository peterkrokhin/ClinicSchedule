using MediatR;

namespace ClinicSchedule.Application.Services.Events.Queries.GetAvailableDateEventsForAllPatientAppointments
{
    public class Query : IRequest<Response>
    {
        public int Id { get; set; }

        public Query(int id)
        {
            Id = id;
        }
    }
}