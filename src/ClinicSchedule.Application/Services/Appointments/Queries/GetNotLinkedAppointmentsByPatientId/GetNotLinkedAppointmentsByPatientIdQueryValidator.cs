using FluentValidation;

namespace ClinicSchedule.Application
{
    public class GetNotLinkedAppointmentsByPatientIdQueryValidator : 
        AbstractValidator<GetNotLinkedAppointmentsByPatientIdQuery>
    {
        public GetNotLinkedAppointmentsByPatientIdQueryValidator()
        {
            RuleFor(prop => prop.Id).NotNull().GreaterThan(0);
        }
    }
}