using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ClinicSchedule.Core;

namespace ClinicSchedule.Application
{
    public class FindManyAppointmentsHandler : 
        IRequestHandler<FindManyAppointmentsQuery, IEnumerable<FindManyAppointmentsResponse>>
    {
        private readonly IAppointmentRepository _appointments;
        private readonly IMapper _mapper;

        public FindManyAppointmentsHandler(IAppointmentRepository appointments, IMapper mapper)
        {
            _appointments = appointments;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FindManyAppointmentsResponse>> Handle(FindManyAppointmentsQuery query, 
            CancellationToken cancellationToken)
        {
            var appointments = await _appointments.FindMany(query.GetPredicate());
            return _mapper.Map<IEnumerable<Appointment>, IEnumerable<FindManyAppointmentsResponse>>(appointments);
        }
    }
}