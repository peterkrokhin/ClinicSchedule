using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

using ClinicSchedule.Core;
using ClinicSchedule.Application;

namespace ClinicSchedule.UnitTests
{
    public class UnitOfWorkTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public async Task TearDown()
        {
            // Возврат БД к начальному состоянию
            await ResetAppointmentIdInEvents();
        }

        // Тест метода GetAvailableDateEventsForAllPatientAppointmentsAsync
        [TestCaseSource(typeof(UnitOfWorkTestsData), 
            nameof(UnitOfWorkTestsData.GetAvailableDateEventsForAllPatientAppointmentsAsyncTestData))]
        public async Task GetAvailableDateEventsForAllPatientAppointmentsAsyncTest(int patientId, AvailableDateEvents expectedAvailableDate)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(new TestDbContext()))
            {
                AvailableDateEvents availableDate = 
                    await unitOfWork.GetAvailableDateEventsForAllPatientAppointmentsAsync(patientId);

                bool result = availableDate.AvailableDate == expectedAvailableDate.AvailableDate &
                    availableDate.AvailableEventsList.Select(e => e.Id)
                        .SequenceEqual(expectedAvailableDate.AvailableEventsList.Select(e => e.Id)) &
                    availableDate.AvailableEventsList.Select(e => e.DateTime)
                        .SequenceEqual(expectedAvailableDate.AvailableEventsList.Select(e => e.DateTime)) &
                    availableDate.AvailableEventsList.Select(e => e.ServiceId)
                        .SequenceEqual(expectedAvailableDate.AvailableEventsList.Select(e => e.ServiceId)) &
                    availableDate.AvailableEventsList.Select(e => e.AppointmentId)
                        .SequenceEqual(expectedAvailableDate.AvailableEventsList.Select(e => e.AppointmentId));

                Assert.That(result == true);
            }
        }

        // Тест метода TryLinkAppointmentToEventAsync. Исключения.
        [TestCase(-1, 1)]       // Назначение не найдено
        [TestCase(1, -1)]       // Ячейка события не найдена
        [TestCase(-1, -1)]      // Назначение и ячека события не найдены
        [TestCase(1, 2)]        // Услуга в назначении и услуга в ячеки не совпадают
        [TestCase(7, 9)]        // Назначение уже привязано
        public async Task TryLinkAppointmentToEventAsyncTestExceptoins(int appointmentId, int eventId)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(new TestDbContext()))
            {
                // Привязка Appointment c Id=7 к Event с Id=9 для проверки [TestCase(7, 9)]
                if (appointmentId == 7)
                {
                    try
                    {
                        await unitOfWork.TryLinkAppointmentToEventAsync(7, 9);
                    }
                    catch
                    {
                    }
                }
            
                Assert.That(async () => await unitOfWork.TryLinkAppointmentToEventAsync(appointmentId, eventId), Throws.Exception);
            }
        }

        // Тест метода TryLinkAppointmentToEventAsync. Корректные данные.
        [TestCase(1, 1)]
        [TestCase(5, 15)]
        [TestCase(7, 9)]
        [TestCase(17, 3)]
        public async Task TryLinkAppointmentToEventAsyncTestNormal(int appointmentId, int eventId)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(new TestDbContext()))
            {
                await unitOfWork.TryLinkAppointmentToEventAsync(appointmentId, eventId);

                Event linkedEvent = await unitOfWork.Events.GetByIdAsync(eventId);
                Assert.That(linkedEvent.AppointmentId == appointmentId);
            }

        }

        private async Task ResetAppointmentIdInEvents()
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(new TestDbContext()))
            {
                var events = await unitOfWork.Events.GetAllAsync();
                foreach (var e in events)
                {
                    e.AppointmentId = null;
                }
                await unitOfWork.SaveChangesAsync();
                }
        }
        
    }
    
    // Данные для тестов
    class UnitOfWorkTestsData
    {
        public static object[] GetAvailableDateEventsForAllPatientAppointmentsAsyncTestData =
        {
            new object[] {1, new AvailableDateEvents(){  
                    AvailableDate = new DateTime(2020, 1, 1),
                    AvailableEventsList = new List<EventDTO>(){
                        new EventDTO(){Id=1, DateTime=new DateTime(2020, 1, 1, 12, 0, 0), ServiceId=1, AppointmentId=null},
                        new EventDTO(){Id=2, DateTime=new DateTime(2020, 1, 1, 13, 0, 0), ServiceId=2, AppointmentId=null},
                        new EventDTO(){Id=3, DateTime=new DateTime(2020, 1, 1, 14, 0, 0), ServiceId=3, AppointmentId=null},
                    }
                },
            },
            new object[] {2, new AvailableDateEvents(){  
                    AvailableDate = new DateTime(2020, 1, 4),
                    AvailableEventsList = new List<EventDTO>(){
                        new EventDTO(){Id=9,  DateTime=new DateTime(2020, 1, 4, 12, 0, 0), ServiceId=4, AppointmentId=null}, 
                        new EventDTO(){Id=10, DateTime=new DateTime(2020, 1, 4, 13, 0, 0), ServiceId=3, AppointmentId=null},
                        new EventDTO(){Id=11, DateTime=new DateTime(2020, 1, 4, 12, 0, 0), ServiceId=2, AppointmentId=null}, 
                    },
                },    
            },
            new object[] {6, new AvailableDateEvents(){  
                    AvailableDate = new DateTime(2020, 1, 6),
                    AvailableEventsList = new List<EventDTO>(){
                        new EventDTO(){Id=14, DateTime=new DateTime(2020, 1, 6, 12, 0, 0), ServiceId=4, AppointmentId=null}, 
                        new EventDTO(){Id=15, DateTime=new DateTime(2020, 1, 6, 13, 0, 0), ServiceId=1, AppointmentId=null}, 
                        new EventDTO(){Id=16, DateTime=new DateTime(2020, 1, 6, 14, 0, 0), ServiceId=1, AppointmentId=null}, 
                        new EventDTO(){Id=17, DateTime=new DateTime(2020, 1, 6, 15, 0, 0), ServiceId=2, AppointmentId=null}, 
                        new EventDTO(){Id=18, DateTime=new DateTime(2020, 1, 6, 16, 0, 0), ServiceId=3, AppointmentId=null},  
                    },
                },    
            },
            new object[] {1000, new AvailableDateEvents(){              // Назначение Id=1000 не найдено
                    AvailableDate = null,
                    AvailableEventsList = new List<EventDTO>(){},
                },
            },
            new object[] {-1, new AvailableDateEvents(){                // Назначение Id=-1 не найдено
                    AvailableDate = null,
                    AvailableEventsList = new List<EventDTO>(){},
                },
            },
        };
    }
}
