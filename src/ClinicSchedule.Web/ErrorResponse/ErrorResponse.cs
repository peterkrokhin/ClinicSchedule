using System;

namespace ClinicSchedule.Web
{
    public class ErrorResponse
    {
        public string Title { get; set; }
        public string Errors { get; set; }
        public int Status { get; set; }

        public ErrorResponse(Exception ex, int status)
        {
            Title = ex.GetType().Name;
            Errors = ex.Message;
            Status = status;
        }
    }
}