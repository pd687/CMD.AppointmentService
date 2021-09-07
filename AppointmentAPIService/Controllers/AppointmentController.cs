using AppointmentAPIService.Models;
using CMD.Appointment.Domain.ApiModels;
using CMD.Appointment.Domain.Managers;
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
        public async Task<IHttpActionResult> Get()
        {

            var appointments = await mng.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        // GET api/appointment/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var appointment = await mng.GetAppointmentByIdAsync(id);
            return Ok(appointment);
        }

        // GET api/appointment/patient/5

        [Route("patient/{id}")]
        public async Task<IHttpActionResult> GetAllAppointmentsByPatientId(int id)
        {

            return Ok(await mng.GetAllAppointmentsByPatientIdAsync(id));
        }

        // POST api/appointment/accept/1
        [Route("accept/{id}")]
        public async Task<IHttpActionResult> Post(int Id)
        {
            return Ok(await mng.AcceptAppointmentAsync(Id));
        }


        // POST api/appointment/reject/1
        [Route("reject/{id}")]
        public async Task<IHttpActionResult> PostReject(int Id)
        {
            return Ok(await mng.RejectAppointmentAsync(Id));
        }



        #endregion


    }
}
