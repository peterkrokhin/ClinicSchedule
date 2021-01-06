using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;

using ClinicSchedule.Core;

namespace ClinicSchedule.Application.Services.Appointments.Queries.GetNotLinkedAppointmentsByPatientId
{
    public class Handler : IRequestHandler<Query, IEnumerable<Response>>
    {
        private readonly IAppointmentRepository _appointments;
        private readonly IMapper _mapper;

        public Handler(IAppointmentRepository appointments, IMapper mapper)
        {
            _appointments = appointments;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Response>> Handle(Query query, CancellationToken cancellationToken)
        {
            var appointments = await _appointments.GetNotLinkedAppointmentsByPatientIdAsync(query.Id);
            return _mapper.Map<IEnumerable<Appointment>, IEnumerable<Response>>(appointments);
        }
    }
}