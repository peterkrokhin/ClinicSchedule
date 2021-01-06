namespace ClinicSchedule.Application
{
    public class GetNotLinkedAppointmentsByPatientNameResponse
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ServiceId { get; set; }
    }
}