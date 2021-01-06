using FluentValidation;

namespace ClinicSchedule.Application
{
    public class GetNotLinkedAppointmentsByPatientNameQueryValidator : 
        AbstractValidator<GetNotLinkedAppointmentsByPatientNameQuery>
    {
        public GetNotLinkedAppointmentsByPatientNameQueryValidator()
        {
            RuleFor(patient => patient.Name).NotNull().Length(2, 100);
        }
    }
}