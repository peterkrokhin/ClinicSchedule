using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using MediatR;

using ClinicSchedule.Core;

namespace ClinicSchedule.Application
{
    public class GetNotLinkedAppointmentsByPatientIdHandler :
        IRequestHandler<GetNotLinkedAppointmentsByPatientIdQuery, IEnumerable<GetNotLinkedAppointmentsByPatientIdResponse>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public GetNotLinkedAppointmentsByPatientIdHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetNotLinkedAppointmentsByPatientIdResponse>> Handle(GetNotLinkedAppointmentsByPatientIdQuery query, 
            CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetNotLinkedAppointmentsByPatientIdAsync(query.Id);
            return _mapper.Map<IEnumerable<Appointment>, IEnumerable<GetNotLinkedAppointmentsByPatientIdResponse>>(appointments);
        }
    }
}