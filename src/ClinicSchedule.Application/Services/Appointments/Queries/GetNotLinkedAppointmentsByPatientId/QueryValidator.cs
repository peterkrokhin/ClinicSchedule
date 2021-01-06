using FluentValidation;

namespace ClinicSchedule.Application.Services.Appointments.Queries.GetNotLinkedAppointmentsByPatientId
{
    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(patient => patient.Id).NotNull().GreaterThan(0);
        }
    }
}