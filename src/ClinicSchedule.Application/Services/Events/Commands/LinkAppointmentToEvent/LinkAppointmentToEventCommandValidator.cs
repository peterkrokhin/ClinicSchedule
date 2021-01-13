using FluentValidation;

namespace ClinicSchedule.Application
{
    public class LinkAppointmentToEventCommandValidator : AbstractValidator<LinkAppointmentToEventCommand>
    {
        public LinkAppointmentToEventCommandValidator()
        {
            RuleFor(p => p.AppointmentId).NotNull().GreaterThan(0);
            RuleFor(p => p.EventId).GreaterThan(0);
        }
    }
}