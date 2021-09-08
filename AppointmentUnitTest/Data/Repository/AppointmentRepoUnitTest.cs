using CMD.Appointment.Domain.Entities;
using Data;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentUnitTest.Data.Repository
{
    [TestClass]
    public class AppointmentRepoUnitTest
    {
        private ICollection<Appointment> GetAppointments()
        {
            return new List<Appointment>()
            {
                new Appointment(){Id=2, DoctorId=1, PatientId=1,Status="pending", AppointmentTime = new DateTime(2021, 08, 10)},
                new Appointment(){Id=4, DoctorId=1, PatientId=1,Status="pending", AppointmentTime = new DateTime(2020, 05, 05)},
                new Appointment(){Id=5, DoctorId=1, PatientId=1,Status="pending", AppointmentTime = new DateTime(2020, 05, 05)},
                new Appointment(){Id=6, DoctorId=1, PatientId=1,Status="pending", AppointmentTime = new DateTime(2020, 05, 05)},
                new Appointment(){Id=7, DoctorId=1, PatientId=1,Status="pending", AppointmentTime = new DateTime(2020, 05, 05)},
            };
        }
    }
}
