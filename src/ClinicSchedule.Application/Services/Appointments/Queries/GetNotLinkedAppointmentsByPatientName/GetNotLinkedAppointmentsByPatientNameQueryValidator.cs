using FluentValidation;

namespace ClinicSchedule.Application
{
    public class GetNotLinkedAppointmentsByPatientNameQueryValidator : 
        AbstractValidator<GetNotLinkedAppointmentsByPatientNameQuery>
    {
        public GetNotLinkedAppointmentsByPatientNameQueryValidator()
        {
            RuleFor(prop => prop.Name).NotNull().Length(2, 100);
        }
    }
}