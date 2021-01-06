using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace ClinicSchedule.Application
{
    public class GetNotLinkedAppointmentsByPatientNameHandler :
        IRequestHandler<GetNotLinkedAppointmentsByPatientNameQuery, GetNotLinkedAppointmentsByPatientNameResponse>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public GetNotLinkedAppointmentsByPatientNameHandler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }

        public async Task<GetNotLinkedAppointmentsByPatientNameResponse> Handle(GetNotLinkedAppointmentsByPatientNameQuery query, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetNotLinkedAppointmentsByPatientNameAsync(query.Name);
            return _mapper.Map<GetNotLinkedAppointmentsByPatientNameResponse>(appointments);
        }
    }
}