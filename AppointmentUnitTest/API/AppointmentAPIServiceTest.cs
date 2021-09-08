using AppointmentAPIService.Controllers;
using CMD.Appointment.Domain.ApiModels;
using CMD.Appointment.Domain.Managers;
using CMD.Appointment.Domain.Repositories;
using Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace AppointmentUnitTest.API
{
    [TestClass]
    public class AppointmentAPIServiceTest
    {
        #region valid tests
        [TestMethod]
        public async Task GetAppoinementsReturnsAppointments()
        {
            //Arrange
            var mockRepo = new Mock<IAppointmentManager>();
            mockRepo.Setup(x => x.GetAllAppointmentsAsync()).ReturnsAsync(GetAppointments().AsQueryable());

            var controller = new AppointmentController(mockRepo.Object);

            //Act
            IHttpActionResult actionResult = await controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<AppointmentAPIModel>>;
            var expected = GetAppointments();
            var actual = contentResult.Content.ToList();
            // Assert
            
            Assert.AreEqual(expected.Count, actual.Count);
        }
        [TestMethod]
        public async Task GetAppoinementReturnsAppointmentWithSameIdAsync_ValidId()
        {
            //Arrange
            var mockRepo = new Mock<IAppointmentManager>();
            mockRepo.Setup(x => x.GetAppointmentByIdAsync(2)).ReturnsAsync(GetAppointments().Where(a => a.Id == 2).First());

            var controller = new AppointmentController(mockRepo.Object);

            //Act
            IHttpActionResult actionResult = await controller.Get(2);
            var contentResult = actionResult as OkNegotiatedContentResult<AppointmentAPIModel>;
            // Assert
            Assert.AreEqual(2, contentResult.Content.Id);
        }
        [TestMethod]
        public async Task GetAppoinementReturnsAppointmentWithSameIdAsync_ValidAppointmentTime()
        {
            //Arrange
            var mockRepo = new Mock<IAppointmentManager>();
            mockRepo.Setup(x => x.GetAppointmentByIdAsync(2)).ReturnsAsync(GetAppointments().Where(a => a.Id == 2).First());

            /*var controller = new AppointmentController(new AppointmentManager(new AppointmentRepository()));*/
            var controller = new AppointmentController(mockRepo.Object);

            //Act
            var expected = GetAppointments().Where(a => a.Id == 2).First();
            IHttpActionResult actionResult = await controller.Get(2);
            var contentResult = actionResult as OkNegotiatedContentResult<AppointmentAPIModel>;
            // Assert
            Assert.AreEqual(expected.AppointmentTime, contentResult.Content.AppointmentTime);
        }
        [TestMethod]
        public async Task GetAppoinementReturnsAppointmentWithSameIdAsync_ValidDoctorId()
        {
            //Arrange
            var mockRepo = new Mock<IAppointmentManager>();
            mockRepo.Setup(x => x.GetAppointmentByIdAsync(2)).ReturnsAsync(GetAppointments().Where(a => a.Id == 2).First());

            /*var controller = new AppointmentController(new AppointmentManager(new AppointmentRepository()));*/
            var controller = new AppointmentController(mockRepo.Object);

            //Act
            var expected = GetAppointments().Where(a => a.Id == 2).First();
            IHttpActionResult actionResult = await controller.Get(2);
            var contentResult = actionResult as OkNegotiatedContentResult<AppointmentAPIModel>;
            // Assert
            Assert.AreEqual(expected.DoctorId, contentResult.Content.DoctorId);
        }
        [TestMethod]
        public async Task GetAppoinementReturnsAppointmentWithSameIdAsync_ValidPatientId()
        {
            //Arrange
            var mockRepo = new Mock<IAppointmentManager>();
            mockRepo.Setup(x => x.GetAppointmentByIdAsync(2)).ReturnsAsync(GetAppointments().Where(a => a.Id == 2).First());

            /*var controller = new AppointmentController(new AppointmentManager(new AppointmentRepository()));*/
            var controller = new AppointmentController(mockRepo.Object);

            //Act
            var expected = GetAppointments().Where(a => a.Id == 2).First();
            IHttpActionResult actionResult = await controller.Get(2);
            var contentResult = actionResult as OkNegotiatedContentResult<AppointmentAPIModel>;
            // Assert
            Assert.AreEqual(expected.PatientId, contentResult.Content.PatientId);
        }
        [TestMethod]
        public async Task GetAppoinementReturnsAppointmentWithSameIdAsync_ValidStatus()
        {
            //Arrange
            var mockRepo = new Mock<IAppointmentManager>();
            mockRepo.Setup(x => x.GetAppointmentByIdAsync(2)).ReturnsAsync(GetAppointments().Where(a => a.Id == 2).First());

            /*var controller = new AppointmentController(new AppointmentManager(new AppointmentRepository()));*/
            var controller = new AppointmentController(mockRepo.Object);

            //Act
            var expected = GetAppointments().Where(a => a.Id == 2).First();
            IHttpActionResult actionResult = await controller.Get(2);
            var contentResult = actionResult as OkNegotiatedContentResult<AppointmentAPIModel>;
            // Assert
            Assert.AreEqual(expected.Status, contentResult.Content.Status);
        }

        [TestMethod]
        public async Task GetAppointmentsForPatientReturnsAppointments()
        {
            //Arrange
            var appointments = GetAppointments().Where(a => a.PatientId == 1).ToList();
            var mockRepo = new Mock<IAppointmentManager>();
            mockRepo.Setup(x => x.GetAllAppointmentsByPatientIdAsync(1)).ReturnsAsync(appointments.AsQueryable());
            var controller = new AppointmentController(mockRepo.Object);

            //Act
            IHttpActionResult actionResult = await controller.GetAllAppointmentsByPatientId(1);
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<AppointmentAPIModel>>;

            //Assert
            
            Assert.AreEqual(appointments.Count, contentResult.Content.Count());
            //CollectionAssert.AreEqual(appointments, contentResult.Content.ToList());
        }
        
        [TestMethod]
        public async Task AcceptAppointmentReturnsAcceptedAppointment_ValidStatus()
        {
            var mockRepo = new Mock<IAppointmentManager>();
            mockRepo.Setup(x => x.AcceptAppointmentAsync(2)).ReturnsAsync(new AppointmentAPIModel { Id=2, Status = "accepted" });

            var controller = new AppointmentController(mockRepo.Object);

            IHttpActionResult actionResult = await controller.Post(2);
            var contentResult = actionResult as OkNegotiatedContentResult<AppointmentAPIModel>;

            Assert.AreEqual("accepted", contentResult.Content.Status);
        }
        [TestMethod]
        public async Task AcceptAppointmentReturnsAcceptedAppointment_ValidId()
        {
            var mockRepo = new Mock<IAppointmentManager>();
            mockRepo.Setup(x => x.AcceptAppointmentAsync(2)).ReturnsAsync(new AppointmentAPIModel { Id = 2, Status = "accepted" });

            var controller = new AppointmentController(mockRepo.Object);

            IHttpActionResult actionResult = await controller.Post(2);
            var contentResult = actionResult as OkNegotiatedContentResult<AppointmentAPIModel>;

            Assert.AreEqual(2, contentResult.Content.Id);
            
        }

        [TestMethod]
        public async Task RejectAppointmentReturnsRejectedAppointment_ValidStatus()
        {
            var mockRepo = new Mock<IAppointmentManager>();
            mockRepo.Setup(x => x.RejectAppointmentAsync(2)).ReturnsAsync(new AppointmentAPIModel {Id=2, Status = "rejected" });

            var controller = new AppointmentController(mockRepo.Object);

            IHttpActionResult actionResult = await controller.PostReject(2);
            var contentResult = actionResult as OkNegotiatedContentResult<AppointmentAPIModel>;

            Assert.AreEqual("rejected", contentResult.Content.Status);
        }
        [TestMethod]
        public async Task RejectAppointmentReturnsRejectedAppointment_ValidId()
        {
            var mockRepo = new Mock<IAppointmentManager>();
            mockRepo.Setup(x => x.RejectAppointmentAsync(2)).ReturnsAsync(new AppointmentAPIModel { Id = 2, Status = "rejected" });

            var controller = new AppointmentController(mockRepo.Object);

            IHttpActionResult actionResult = await controller.PostReject(2);
            var contentResult = actionResult as OkNegotiatedContentResult<AppointmentAPIModel>;

            Assert.AreEqual(2, contentResult.Content.Id);
        }
        #endregion

        #region invalid tests

        [TestMethod]
        public async Task GetAppoinementReturnsAppointmentWithSameIdAsync_InvalidTest()
        {
            //Arrange
            var mockRepo = new Mock<IAppointmentManager>();
            mockRepo.Setup(x => x.GetAppointmentByIdAsync(2)).ReturnsAsync(new AppointmentAPIModel { Id = 1 });

            /*var controller = new AppointmentController(new AppointmentManager(new AppointmentRepository()));*/
            var controller = new AppointmentController(mockRepo.Object);

            //Act
            IHttpActionResult actionResult = await controller.Get(2);
            var contentResult = actionResult as OkNegotiatedContentResult<AppointmentAPIModel>;
            // Assert
            Assert.AreNotEqual(2, contentResult.Content.Id);
        }

        [TestMethod]
        public async Task GetAppointmentsForPatientReturnsAppointments_InvalidTest()
        {
            //Arrange
            var appointments = GetAppointments().Where(a => a.PatientId != 1).ToList();
            var mockRepo = new Mock<IAppointmentManager>();
            mockRepo.Setup(x => x.GetAllAppointmentsByPatientIdAsync(1)).ReturnsAsync(appointments.AsQueryable());
            var controller = new AppointmentController(mockRepo.Object);

            //Act
            IHttpActionResult actionResult = await controller.GetAllAppointmentsByPatientId(1);
            var contentResult = actionResult as OkNegotiatedContentResult<IQueryable<AppointmentAPIModel>>;
            var expected = GetAppointments().Where(a => a.PatientId == 1).ToList();
            var actual = contentResult.Content.ToList();

            //Assert
            Assert.AreNotEqual(expected.Count, actual.Count);
        }

        [TestMethod]
        public async Task AcceptAppointmentReturnsAcceptedAppointment_InvalidTest()
        {
            var mockRepo = new Mock<IAppointmentManager>();
            mockRepo.Setup(x => x.AcceptAppointmentAsync(2)).ReturnsAsync(new AppointmentAPIModel { Status = "pending" });

            var controller = new AppointmentController(mockRepo.Object);

            IHttpActionResult actionResult = await controller.Post(2);
            var contentResult = actionResult as OkNegotiatedContentResult<AppointmentAPIModel>;

            Assert.AreNotEqual("accepted", contentResult.Content.Status);
        }

        [TestMethod]
        public async Task RejectAppointmentReturnsRejectedAppointment_InvalidTest()
        {
            var mockRepo = new Mock<IAppointmentManager>();
            mockRepo.Setup(x => x.RejectAppointmentAsync(2)).ReturnsAsync(new AppointmentAPIModel { Status = "accepted" });

            var controller = new AppointmentController(mockRepo.Object);

            IHttpActionResult actionResult = await controller.PostReject(2);
            var contentResult = actionResult as OkNegotiatedContentResult<AppointmentAPIModel>;

            Assert.AreNotEqual("rejected", contentResult.Content.Status);
        }
        #endregion
        private List<AppointmentAPIModel> GetAppointments()
        {
            return new List<AppointmentAPIModel>()
            {
                new AppointmentAPIModel(){Id=2, DoctorId=1, PatientId=1,Status="pending", AppointmentTime = new DateTime(2021, 08, 10)},
                new AppointmentAPIModel(){Id=4, DoctorId=1, PatientId=1,Status="pending", AppointmentTime = new DateTime(2020, 05, 05)},
                new AppointmentAPIModel(){Id=5, DoctorId=1, PatientId=1,Status="pending", AppointmentTime = new DateTime(2020, 05, 05)},
                new AppointmentAPIModel(){Id=6, DoctorId=1, PatientId=1,Status="pending", AppointmentTime = new DateTime(2020, 05, 05)},
                new AppointmentAPIModel(){Id=7, DoctorId=1, PatientId=2,Status="pending", AppointmentTime = new DateTime(2020, 05, 05)},
            };
        }
    }
}
