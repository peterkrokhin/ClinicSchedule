using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using ClinicSchedule.Infrastructure;
using ClinicSchedule.Core;

namespace ClinicSchedule.Web
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinkAppointmentsServiceController : Controller
    {
        public ILinkAppointmentsService LinkAppointmentsService { get; set; }

        public LinkAppointmentsServiceController(ILinkAppointmentsService linkAppointmentsService)
        {
            LinkAppointmentsService = linkAppointmentsService;
        }



    }
}