using FluentValidation;

namespace ClinicSchedule.Application.Services.Appointments.Queries.GetNotLinkedAppointmentsByPatientName
{
    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(patient => patient.Name).NotNull().Length(2, 100);
        }
    }
}