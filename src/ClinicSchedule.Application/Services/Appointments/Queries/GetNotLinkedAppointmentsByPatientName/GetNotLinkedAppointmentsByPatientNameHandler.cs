using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;

using ClinicSchedule.Core;

namespace ClinicSchedule.Application
{
    public class GetNotLinkedAppointmentsByPatientNameHandler :
        IRequestHandler<GetNotLinkedAppointmentsByPatientNameQuery, IEnumerable<GetNotLinkedAppointmentsByPatientNameResponse>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public GetNotLinkedAppointmentsByPatientNameHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetNotLinkedAppointmentsByPatientNameResponse>> Handle(GetNotLinkedAppointmentsByPatientNameQuery query, 
            CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetNotLinkedAppointmentsByPatientNameAsync(query.Name);
            return _mapper.Map<IEnumerable<Appointment>, IEnumerable<GetNotLinkedAppointmentsByPatientNameResponse>>(appointments);
        }
    }
}