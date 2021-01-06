namespace ClinicSchedule.Application.Services.Appointments.Queries.GetNotLinkedAppointmentsByPatientName
{
    public class Response
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
    }
}