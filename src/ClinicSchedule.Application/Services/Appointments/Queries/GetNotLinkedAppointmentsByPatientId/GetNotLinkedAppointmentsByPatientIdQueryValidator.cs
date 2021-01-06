using FluentValidation;

namespace ClinicSchedule.Application
{
    public class GetNotLinkedAppointmentsByPatientIdQueryValidator : 
        AbstractValidator<GetNotLinkedAppointmentsByPatientIdQuery>
    {
        public GetNotLinkedAppointmentsByPatientIdQueryValidator()
        {
            RuleFor(patient => patient.Id).NotNull().GreaterThan(0);
        }
    }
}