using CMD.Appointment.Domain.ApiModels;
using CMD.Appointment.Domain.Managers;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace AppointmentAPIService.Controllers
{

    [RoutePrefix("api/appointment")]
    public class AppointmentController : ApiController
    {
        //IAppointmentManager mng = new AppointmentManager(new AppointmentRepository());
        IAppointmentManager mng = null;
        public AppointmentController(IAppointmentManager mng)
        {
            this.mng = mng;
        }
        #region Sync
        // GET api/appointment
        /*public IEnumerable<AppointmentAPIModel> Get()
        {
            return mng.GetAllAppointments();
        }

        // GET api/appointment/5
        public AppointmentAPIModel Get(int id)
        {
            return mng.GetAppointmentById(id);
        }

        // GET api/appointment/patient/5

        [Route("patient/{id}")]
        public IHttpActionResult GetAllAppointmentsByPatientId(int id)
        {
            return Ok(mng.GetAllAppointmentsByPatientId(id));
        }

        // POST api/appointment/accept/1
        [Route("accept/{id}")]
        public IHttpActionResult Post(int Id)
        {
            return Ok(mng.AcceptAppointment(Id));
        }


        // POST api/appointment/reject/1
        [Route("reject/{id}")]
        public IHttpActionResult PostReject(int Id)
        {
            return Ok(mng.RejectAppointment(Id));
        }
*/
        #endregion 


        #region Async
        // GET api/appointment
        /// <summary>
        /// Returns all Appointments.
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var appointments = await mng.GetAllAppointmentsAsync();
                return Ok(appointments);
            }
            catch(Exception e)
            {
                var logger = LogManager.GetLogger("databaseLogger");
                logger.Error(e);
                throw e;
            }
        }

        // GET api/appointment/5
        /// <summary>
        /// Returns Appointment by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var appointment = await mng.GetAppointmentByIdAsync(id);
                return Ok(appointment);
            }
            catch(Exception e)
            {
                var logger = LogManager.GetLogger("databaseLogger");
                if (e.InnerException != null)
                {
                    logger.Error(new Exception(e.Message, e.InnerException));
                }
                else
                {
                    logger.Error(e);
                }
                throw e;
            }
        }

        // GET api/appointment/patient/5
        /// <summary>
        /// Returns the Appointments for Patient Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("patient/{id}")]
        public async Task<IHttpActionResult> GetAllAppointmentsByPatientId(int id)
        {

            try
            {
                return Ok(await mng.GetAllAppointmentsByPatientIdAsync(id));
            }
            catch(Exception e)
            {
                var logger = LogManager.GetLogger("databaseLogger");
                logger.Error(e);
                throw e;
            }
        }

        // POST api/appointment/accept/1
        /// <summary>
        /// Accepting an Appointment By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("accept/{id}")]
        public async Task<IHttpActionResult> Post(int Id)
        {
            try
            {
                return Ok(await mng.AcceptAppointmentAsync(Id));
            }
            catch(Exception e)
            {
                var logger = LogManager.GetLogger("databaseLogger");
                logger.Error(e);
                throw e;
            }
        }

        /// <summary>
        /// Rejecting an Appointment by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        // POST api/appointment/reject/1
        [Route("reject/{id}")]
        public async Task<IHttpActionResult> PostReject(int Id)
        {
            try
            {
                return Ok(await mng.RejectAppointmentAsync(Id));
            }
            catch(Exception e)
            {
                var logger = LogManager.GetLogger("databaseLogger");
                logger.Error(e);
                throw e;
            }
        }
        #endregion


    }
}
