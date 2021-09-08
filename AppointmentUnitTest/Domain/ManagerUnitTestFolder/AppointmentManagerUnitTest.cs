using CMD.Appointment.Domain.Entities;
using CMD.Appointment.Domain.Managers;
using CMD.Appointment.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentAPIModelUnitTest.Domain.Manager
{


    [TestClass]
    public class AppointmentAPIModelManagerUnitTest
    {
        Mock<IAppointmentRepository> mockRepo;

        [TestInitialize]
        public void Init()
        {
            mockRepo = new Mock<IAppointmentRepository>();
        }

        #region GetAllAppointmentAsync
        [TestMethod]
        public async Task GetAllAppointmentAsync_ShouldReturnAllAppointment()
        {
            //Arrange
            mockRepo.Setup(x => x.GetAllAppointmentsAsync()).ReturnsAsync(GetAppointment());

            var mgr = new AppointmentManager(mockRepo.Object);
            //Act

            var mgrAppointments = await mgr.GetAllAppointmentsAsync();
            var expected = mgrAppointments.ToList();
            var actual = GetAppointment();

            //Assert

            Assert.AreEqual(expected.Count, 5);

        }

        [TestMethod]
        public async Task GetAllAppointmentAsync_ShouldReturnSameAppointmentDoctorId()
        {
            //Arrange
            mockRepo.Setup(x => x.GetAllAppointmentsAsync()).ReturnsAsync(GetAppointment());

            var mgr = new AppointmentManager(mockRepo.Object);
            //Act

            var mgrAppointments = await mgr.GetAllAppointmentsAsync();
            var expected = mgrAppointments.ToList();
            var actual = GetAppointment();

            //Assert

            Assert.AreEqual(expected[0].DoctorId, actual[0].DoctorId);

        }

        [TestMethod]
        public async Task GetAllAppointmentAsync_ShouldReturnSameAppointmentId()
        {
            //Arrange
            mockRepo.Setup(x => x.GetAllAppointmentsAsync()).ReturnsAsync(GetAppointment());

            var mgr = new AppointmentManager(mockRepo.Object);
            //Act

            var mgrAppointments = await mgr.GetAllAppointmentsAsync();
            var expected = mgrAppointments.ToList();
            var actual = GetAppointment();

            //Assert

            Assert.AreEqual(expected[0].Id, actual[0].Id);

        }

        [TestMethod]
        public async Task GetAllAppointmentAsync_ShouldReturnSameAppointmentTime()
        {
            //Arrange
            mockRepo.Setup(x => x.GetAllAppointmentsAsync()).ReturnsAsync(GetAppointment());

            var mgr = new AppointmentManager(mockRepo.Object);
            //Act

            var mgrAppointments = await mgr.GetAllAppointmentsAsync();
            var expected = mgrAppointments.ToList();
            var actual = GetAppointment();

            //Assert

            Assert.AreEqual(expected[0].AppointmentTime, actual[0].AppointmentTime);

        }

        [TestMethod]
        public async Task GetAllAppointmentAsync_ShouldReturnSamePatientId()
        {
            //Arrange
            mockRepo.Setup(x => x.GetAllAppointmentsAsync()).ReturnsAsync(GetAppointment());

            var mgr = new AppointmentManager(mockRepo.Object);
            //Act

            var mgrAppointments = await mgr.GetAllAppointmentsAsync();
            var expected = mgrAppointments.ToList();
            var actual = GetAppointment();

            //Assert

            Assert.AreEqual(expected[0].PatientId, actual[0].PatientId);

        }

        [TestMethod]
        public async Task GetAllAppointmentAsync_ShouldReturnSameStatus()
        {
            //Arrange
            mockRepo.Setup(x => x.GetAllAppointmentsAsync()).ReturnsAsync(GetAppointment());

            var mgr = new AppointmentManager(mockRepo.Object);
            //Act

            var mgrAppointments = await mgr.GetAllAppointmentsAsync();
            var expected = mgrAppointments.ToList();
            var actual = GetAppointment();

            //Assert

            Assert.AreEqual(expected[0].Status, actual[0].Status);

        }
        #endregion

        #region GetAllAppointmentsByPatientIdAsync

        [TestMethod]
        public async Task GetAllAppointmentsByPatientIdAsync_ShouldReturnSameAppointmentCount()
        {
            //Arrange
            var appointments = GetAppointment().Where(a => a.PatientId == 1).ToList();

            mockRepo.Setup(x => x.GetAllAppointmentsByPatientIdAsync(1)).ReturnsAsync(appointments);

            var mgr = new AppointmentManager(mockRepo.Object);
            //Act

            var mgrAppointments = await mgr.GetAllAppointmentsByPatientIdAsync(1);
            var expected = mgrAppointments.ToList();
            var actual = GetAppointment().Where(a => a.PatientId == 1).ToList();

            //Assert

            Assert.AreEqual(expected.Count, actual.Count);

        }

        #endregion

        #region GetAppointmentByIdAsync

        [TestMethod]
        public async Task GetAppointmentByIdAsync_ShouldReturnValidAppointment()
        {
            //Arrange
            var appointment = GetAppointment().Where(a => a.Id == 4).FirstOrDefault();

            mockRepo.Setup(x => x.GetAppointmentByIdAsync(4)).ReturnsAsync(appointment);

            var mgr = new AppointmentManager(mockRepo.Object);
            //Act

            var mgrAppointmentById = await mgr.GetAppointmentByIdAsync(4);
            var expected = mgrAppointmentById;
            var actual = appointment;

            //Assert

            Assert.AreEqual(expected.Id, actual.Id);

        }

        #endregion

        #region AcceptAppointmentAsync

        [TestMethod]
        public async Task AcceptAppointmentAsync_ShouldHaveSameStatusAsAccepted()
        {
            //Arrange

            mockRepo.Setup(x => x.AcceptAppointmentAsync(4)).ReturnsAsync(new Appointment { Status = "accepted" });

            var mgr = new AppointmentManager(mockRepo.Object);
            //Act

            var mgrAppointment = await mgr.AcceptAppointmentAsync(4);
            var expected = mgrAppointment.Status;
            var actual = "accepted";

            //Assert

            Assert.AreEqual(expected, actual);

        }

        #endregion

        #region RejectAppointmentAsync

        [TestMethod]
        public async Task RejectAppointmentAsync_ShouldHaveSameStatusAsRejected()
        {
            //Arrange

            mockRepo.Setup(x => x.RejectAppointmentAsync(4)).ReturnsAsync(new Appointment { Status = "rejected" });

            var mgr = new AppointmentManager(mockRepo.Object);
            //Act

            var mgrAppointment = await mgr.RejectAppointmentAsync(4);
            var expected = mgrAppointment.Status;
            var actual = "rejected";

            //Assert

            Assert.AreEqual(expected, actual);

        }

        #endregion



        private List<Appointment> GetAppointment()
        {
            return new List<Appointment>()
            {
                new Appointment(){Id=2, DoctorId=1, PatientId=1,Status="pending", AppointmentTime = new DateTime(2021, 08, 10)},
                new Appointment(){Id=4, DoctorId=1, PatientId=1,Status="pending", AppointmentTime = new DateTime(2020, 05, 05)},
                new Appointment(){Id=5, DoctorId=1, PatientId=1,Status="pending", AppointmentTime = new DateTime(2020, 05, 05)},
                new Appointment(){Id=6, DoctorId=1, PatientId=1,Status="pending", AppointmentTime = new DateTime(2020, 05, 05)},
                new Appointment(){Id=7, DoctorId=1, PatientId=2,Status="pending", AppointmentTime = new DateTime(2020, 05, 05)},
            };
        }
    }
}
