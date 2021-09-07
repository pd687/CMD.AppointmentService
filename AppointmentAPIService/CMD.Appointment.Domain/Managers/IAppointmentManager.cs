using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CMD.Appointment.Domain.Managers
{
    public interface IAppointmentManager
    {
        IEnumerable<ApiModels.AppointmentAPIModel> GetAllAppointments();
        IEnumerable<ApiModels.AppointmentAPIModel> GetAllAppointmentsByPatientId(int id);
        ApiModels.AppointmentAPIModel GetAppointmentById(int id);
        ApiModels.AppointmentAPIModel AcceptAppointment(int id);
        ApiModels.AppointmentAPIModel RejectAppointment(int id);


        #region Async

        Task<IQueryable<ApiModels.AppointmentAPIModel>> GetAllAppointmentsAsync();
        Task<IQueryable<ApiModels.AppointmentAPIModel>> GetAllAppointmentsByPatientIdAsync(int id);
        Task<ApiModels.AppointmentAPIModel> GetAppointmentByIdAsync(int id);
        Task<ApiModels.AppointmentAPIModel> AcceptAppointmentAsync(int id);
        Task<ApiModels.AppointmentAPIModel> RejectAppointmentAsync(int id);

        #endregion

    }
}
