using CMD.Appointment.Domain.Repositories;
using System.Linq;

namespace CMD.Appointment.Data.RepositoriesEF
{
    public class AppointmentRepository : IAppointmentRepository
    {
        ApplicationsDbContext db = new ApplicationsDbContext();
        public Domain.Entities.Appointment AcceptAppointment(int id)
        {
            Domain.Entities.Appointment appointment = db.Appointments.Find(id);
            appointment.Status = "accepted";
            db.Entry(appointment).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return appointment;

        }

        public IQueryable<Domain.Entities.Appointment> GetAllAppointments()
        {
            return db.Appointments.AsQueryable();
        }

        public IQueryable<Domain.Entities.Appointment> GetAllAppointmentsByPatientId(int id)
        {
            return db.Appointments.Where(c => c.PatientId == id).AsQueryable();
        }

        public Domain.Entities.Appointment GetAppointmentById(int id)
        {
            return db.Appointments.Find(id);
        }

        public Domain.Entities.Appointment RejectAppointment(int id)
        {
            CMD.Appointment.Domain.Entities.Appointment appointment = db.Appointments.Find(id);
            appointment.Status = "rejected";
            db.Entry(appointment).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return appointment;
        }
    }
}
