using CMD.Appointment.Domain.Entities;
using CMD.Appointment.Domain.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        AppointmentDB db = new AppointmentDB();

        #region Sync
        public IQueryable<Appointment> GetAllAppointments()
        {
            return db.Appointments.AsQueryable();
        }


        public IQueryable<Appointment> GetAllAppointmentsByPatientId(int id)
        {
            return db.Appointments.Where(c => c.PatientId == id).AsQueryable();
        }


        public Appointment GetAppointmentById(int id)
        {
            return db.Appointments.Find(id);
        }

        public Appointment AcceptAppointment(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            appointment.Status = "accepted";
            db.SaveChanges();
            return appointment;

        }

        public Appointment RejectAppointment(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            appointment.Status = "rejected";
            db.SaveChanges();
            return appointment;
        }

        #endregion

        #region Async
        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            var appointments = await db.Appointments.ToListAsync();

            return appointments;
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            var appointment = await db.Appointments.FindAsync(id);
            return appointment;
        }

        public async Task<List<Appointment>> GetAllAppointmentsByPatientIdAsync(int patientId)
        {
            var appointments = await db.Appointments.Where(app => app.PatientId == patientId).ToListAsync();

            return appointments;
        }
        public async Task<Appointment> AcceptAppointmentAsync(int id)
        {
            var appointment = await db.Appointments.FindAsync(id);
            appointment.Status = "accepted";
            await db.SaveChangesAsync();

            return appointment;
        }

        public async Task<Appointment> RejectAppointmentAsync(int id)
        {
            var appointment = await db.Appointments.FindAsync(id);
            appointment.Status = "rejected";
            await db.SaveChangesAsync();

            return appointment;
        }
        #endregion
    }
}
