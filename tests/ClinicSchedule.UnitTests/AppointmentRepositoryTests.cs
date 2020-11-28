using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

using ClinicSchedule.Core;
using ClinicSchedule.Application;

namespace ClinicSchedule.UnitTests
{
    public class AppointmentRepositoryTests
    {
        [SetUp]
        public void Setup()
        {
        }

        // Тест метода GetNotLinkedAppointmentsByPatientIdAsync
        [TestCaseSource(typeof(AppointmentRepositoryTestsData), 
            nameof(AppointmentRepositoryTestsData.GetNotLinkedAppointmentsByPatientIdAsyncTestData))]
        public async Task GetNotLinkedAppointmentsByPatientIdAsyncTest(int patientId, IEnumerable<Appointment> expectedAppointments)
        {
            using (IAppDbContext testDbContext = new TestDbContext())
            {
                AppointmentRepository appointmentRepository = new AppointmentRepository(testDbContext);

                IEnumerable<Appointment> appointments = 
                await appointmentRepository.GetNotLinkedAppointmentsByPatientIdAsync(patientId);

                bool result = appointments.Select(a => a.Id)
                    .SequenceEqual(expectedAppointments.Select(a => a.Id)) &
                appointments.Select(a => a.ServiceId)
                    .SequenceEqual(expectedAppointments.Select(a => a.ServiceId)) &
                appointments.Select(a => a.PatientId)
                    .SequenceEqual(expectedAppointments.Select(a => a.PatientId));

                Assert.That(result == true);
            }
        
        }

        // Тест метода GetNotLinkedAppointmentsByPatientNameAsync
        [TestCaseSource(typeof(AppointmentRepositoryTestsData), 
            nameof(AppointmentRepositoryTestsData.GetNotLinkedAppointmentsByPatientNameAsyncTestData))]
        public async Task GetNotLinkedAppointmentsByPatientNameAsyncTest(string patientName, IEnumerable<Appointment> expectedAppointments)
        {
            using (IAppDbContext testDbContext = new TestDbContext())
            {
                AppointmentRepository appointmentRepository = new AppointmentRepository(testDbContext);

                IEnumerable<Appointment> appointments = 
                await appointmentRepository.GetNotLinkedAppointmentsByPatientNameAsync(patientName);

                bool result = appointments.Select(a => a.Id)
                    .SequenceEqual(expectedAppointments.Select(a => a.Id)) &
                appointments.Select(a => a.ServiceId)
                    .SequenceEqual(expectedAppointments.Select(a => a.ServiceId)) &
                appointments.Select(a => a.PatientId)
                    .SequenceEqual(expectedAppointments.Select(a => a.PatientId));

                Assert.That(result == true);
            }
        }
    }
    
    // Данные для тестов
    class AppointmentRepositoryTestsData
    {
        public static object[] GetNotLinkedAppointmentsByPatientIdAsyncTestData =
        {
            new object[] {1, new List<Appointment>()
            {  
                new Appointment(){Id = 1, ServiceId = 1, PatientId = 1},
                new Appointment(){Id = 2, ServiceId = 2, PatientId = 1},
                new Appointment(){Id = 3, ServiceId = 3, PatientId = 1},
            }},
            new object[] {2, new List<Appointment>()
            {  
                new Appointment(){Id = 4, ServiceId = 4, PatientId = 2},
            }},
            new object[] {3, new List<Appointment>()
            {  
                new Appointment(){Id = 5, ServiceId = 1, PatientId = 3},
                new Appointment(){Id = 6, ServiceId = 2, PatientId = 3},
                new Appointment(){Id = 7, ServiceId = 4, PatientId = 3},
            }},
            new object[] {1000, new List<Appointment>()},
            new object[] {-1, new List<Appointment>()},
        };

        public static object[] GetNotLinkedAppointmentsByPatientNameAsyncTestData =
        {
            new object[] {"Отражающий Ловец Фрейдович", new List<Appointment>()
            {  
                new Appointment(){Id = 1, ServiceId = 1, PatientId = 1},
                new Appointment(){Id = 2, ServiceId = 2, PatientId = 1},
                new Appointment(){Id = 3, ServiceId = 3, PatientId = 1},
            }},
            new object[] {"Летящий Солярис Парацельсиевич", new List<Appointment>()
            {  
                new Appointment(){Id = 4, ServiceId = 4, PatientId = 2},
            }},
            new object[] {"Суматошный Совет Аврелиевич", new List<Appointment>()
            {  
                new Appointment(){Id = 5, ServiceId = 1, PatientId = 3},
                new Appointment(){Id = 6, ServiceId = 2, PatientId = 3},
                new Appointment(){Id = 7, ServiceId = 4, PatientId = 3},
            }},
            new object[] {"", new List<Appointment>()},
            new object[] {"Any invalid string", new List<Appointment>()},
        };
    }

}
