using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMD.Appointment.Domain.Repositories
{
    public interface IAppointmentRepository
    {
        IQueryable<Entities.Appointment> GetAllAppointments();
        IQueryable<Entities.Appointment> GetAllAppointmentsByPatientId(int id);
        Entities.Appointment GetAppointmentById(int id);
        Entities.Appointment AcceptAppointment(int id);
        Entities.Appointment RejectAppointment(int id);

        #region Async

        Task<List<Entities.Appointment>> GetAllAppointmentsAsync();

        Task<List<Entities.Appointment>> GetAllAppointmentsByPatientIdAsync(int id);
        Task<Entities.Appointment> GetAppointmentByIdAsync(int id);
        Task<Entities.Appointment> AcceptAppointmentAsync(int id);
        Task<Entities.Appointment> RejectAppointmentAsync(int id);

        #endregion

    }
}
