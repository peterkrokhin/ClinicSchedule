using FluentValidation;

namespace ClinicSchedule.Application.Services.Events.Queries.GetAvailableDateEventsForAllPatientAppointments
{
    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(patient => patient.Id).NotNull().GreaterThan(0);
        }
    }
}