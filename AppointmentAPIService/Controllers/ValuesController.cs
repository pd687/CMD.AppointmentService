using CMD.Appointment.Domain.ApiModels;
using CMD.Appointment.Domain.Managers;
using Data.Repositories;
using System.Collections.Generic;
using System.Web.Http;


namespace AppointmentAPIService.Controllers
{
    public class ValuesController : ApiController
    {
        IAppointmentManager mng = new AppointmentManager(new AppointmentRepository());

        // GET api/values
        public IEnumerable<AppointmentAPIModel> Get()
        {
            return mng.GetAllAppointments();
        }

        // GET api/values/5
        public AppointmentAPIModel Get(int id)
        {
            return mng.GetAppointmentById(id);
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
