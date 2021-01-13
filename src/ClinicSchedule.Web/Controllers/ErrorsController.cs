using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClinicSchedule.Application;

namespace ClinicSchedule.Web
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("/api/errors")]
        public ActionResult<ErrorResponse> HandleErrors()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var code = 500; // Internal Server Error by default

            if  (exception is AppointmentNotFoundException) code = 404;
            else if (exception is EventNotFoundException) code = 404;
            else if (exception is AppointmentAlreadyLinkedException) code = 400;
            else if (exception is EventNotAvailableException) code = 400;
            else if (exception is ServicesAppointmentEventDontMatchException) code = 400;

            Response.StatusCode = code;

            return new ErrorResponse(exception, code);
        }
    }
}