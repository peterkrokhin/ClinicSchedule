namespace ClinicSchedule.Application
{
    public class GetNotLinkedAppointmentsByPatientIdResponse
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
    }
}