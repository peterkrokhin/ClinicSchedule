namespace ClinicSchedule.Application.Services.Appointments.Queries.GetNotLinkedAppointmentsByPatientId
{
    public class Response
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
    }
}