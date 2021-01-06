using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace ClinicSchedule.Application
{
    class GetNotLinkedAppointmentsByPatientIdHandler :
        IRequestHandler<GetNotLinkedAppointmentsByPatientIdQuery, GetNotLinkedAppointmentsByPatientIdResponse>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public GetNotLinkedAppointmentsByPatientIdHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<GetNotLinkedAppointmentsByPatientIdResponse> Handle(GetNotLinkedAppointmentsByPatientIdQuery query, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetNotLinkedAppointmentsByPatientIdAsync(query.Id);
            return _mapper.Map<GetNotLinkedAppointmentsByPatientIdResponse>(appointments);
        }
    }
}