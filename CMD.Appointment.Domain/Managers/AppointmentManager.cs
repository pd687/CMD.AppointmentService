using AutoMapper;
using CMD.Appointment.Domain.ApiModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMD.Appointment.Domain.Managers
{
    public class AppointmentManager : IAppointmentManager
    {

        private readonly Repositories.IAppointmentRepository repo;

        #region Sync

        public AppointmentManager(Repositories.IAppointmentRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<AppointmentAPIModel> GetAllAppointments()
        {
            var appointments = repo.GetAllAppointments();
            ICollection<AppointmentAPIModel> result = new List<AppointmentAPIModel>();


            var config = new MapperConfiguration(cfg => cfg.CreateMap<Entities.Appointment, AppointmentAPIModel>());
            var mapper = new Mapper(config);
            foreach (var item in appointments)
            {
                result.Add(mapper.Map<AppointmentAPIModel>(item));

            }

            return result;
        }

        public IEnumerable<AppointmentAPIModel> GetAllAppointmentsByPatientId(int id)
        {
            var appointments = repo.GetAllAppointmentsByPatientId(id);
            ICollection<AppointmentAPIModel> result = new List<AppointmentAPIModel>();


            var config = new MapperConfiguration(cfg => cfg.CreateMap<Entities.Appointment, AppointmentAPIModel>());
            var mapper = new Mapper(config);
            foreach (var item in appointments)
            {
                result.Add(mapper.Map<AppointmentAPIModel>(item));

            }

            return result;
        }

        public AppointmentAPIModel GetAppointmentById(int id)
        {
            var appointment = repo.GetAppointmentById(id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Entities.Appointment, AppointmentAPIModel>());
            var mapper = new Mapper(config);


            return mapper.Map<AppointmentAPIModel>(appointment);
        }

        public AppointmentAPIModel RejectAppointment(int id)
        {

            var appointment = repo.RejectAppointment(id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Entities.Appointment, AppointmentAPIModel>());
            var mapper = new Mapper(config);


            return mapper.Map<AppointmentAPIModel>(appointment);

        }

        public AppointmentAPIModel AcceptAppointment(int id)
        {

            var appointment = repo.AcceptAppointment(id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Entities.Appointment, AppointmentAPIModel>());
            var mapper = new Mapper(config);


            return mapper.Map<AppointmentAPIModel>(appointment);
        }

        #endregion


        #region Async


        async Task<IQueryable<AppointmentAPIModel>> IAppointmentManager.GetAllAppointmentsAsync()
        {
            var appointments = await repo.GetAllAppointmentsAsync();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Appointment, AppointmentAPIModel>();
            });

            var mapper = new Mapper(config);

            var result = new List<AppointmentAPIModel>();

            foreach (var item in appointments)
            {
                result.Add(mapper.Map<AppointmentAPIModel>(item));
            }

            return result.AsQueryable();
        }

        async Task<IQueryable<AppointmentAPIModel>> IAppointmentManager.GetAllAppointmentsByPatientIdAsync(int id)
        {

            var appointments = await repo.GetAllAppointmentsByPatientIdAsync(id);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Appointment, AppointmentAPIModel>();
            });

            var mapper = new Mapper(config);

            var result = new List<AppointmentAPIModel>();

            foreach (var item in appointments)
            {
                result.Add(mapper.Map<AppointmentAPIModel>(item));
            }

            return result.AsQueryable();
        }

        async Task<AppointmentAPIModel> IAppointmentManager.GetAppointmentByIdAsync(int id)
        {
            var appointment = await repo.GetAppointmentByIdAsync(id);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Appointment, AppointmentAPIModel>();
            });
            Mapper mapper = new Mapper(config);
            return mapper.Map<AppointmentAPIModel>(appointment);
        }

        async Task<AppointmentAPIModel> IAppointmentManager.AcceptAppointmentAsync(int id)
        {

            var appointment = await repo.GetAppointmentByIdAsync(id);

            appointment.Status = "accepted";
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Appointment, AppointmentAPIModel>();
            });
            Mapper mapper = new Mapper(config);
            return mapper.Map<AppointmentAPIModel>(appointment);

        }

        async Task<AppointmentAPIModel> IAppointmentManager.RejectAppointmentAsync(int id)
        {
            var appointment = await repo.GetAppointmentByIdAsync(id);
            appointment.Status = "rejected";
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Appointment, AppointmentAPIModel>();
            });
            Mapper mapper = new Mapper(config);
            return mapper.Map<AppointmentAPIModel>(appointment);
        }

        #endregion
    }
}
