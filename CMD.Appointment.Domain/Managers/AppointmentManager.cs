using AutoMapper;
using CMD.Appointment.Domain.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace CMD.Appointment.Domain.Managers
{
    public class AppointmentManager : IAppointmentManager
    {

        private readonly Repositories.IAppointmentRepository repo;

        private readonly ObjectCache _cache = new MemoryCache("PatientCache");


        #region Sync

        public AppointmentManager(Repositories.IAppointmentRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<AppointmentAPIModel> GetAllAppointments()
        {

            var cached_allAppointment = _cache.Get("Appointments");
            if (cached_allAppointment != null)
                return (IEnumerable<AppointmentAPIModel>)cached_allAppointment;

            var appointments = repo.GetAllAppointments();
            ICollection<AppointmentAPIModel> result = new List<AppointmentAPIModel>();


            var config = new MapperConfiguration(cfg => cfg.CreateMap<Entities.Appointment, AppointmentAPIModel>());
            var mapper = new Mapper(config);
            foreach (var item in appointments)
            {
                result.Add(mapper.Map<AppointmentAPIModel>(item));

            }
            _cache.Set("Appointments", result, DateTimeOffset.Now.AddMinutes(10));
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
            var cached_allAppointmentAsync = _cache.Get("Appointments");
            if (cached_allAppointmentAsync != null)
                return (IQueryable<AppointmentAPIModel>)cached_allAppointmentAsync;

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

            _cache.Set("Appointments", result, DateTimeOffset.Now.AddMinutes(10));

            return result.AsQueryable();
        }

        async Task<IQueryable<AppointmentAPIModel>> IAppointmentManager.GetAllAppointmentsByPatientIdAsync(int id)
        {
            var cached_allAppointmentAsync = _cache.Get(String.Concat("PatientAppointment", id));
            if (cached_allAppointmentAsync != null)
                return (IQueryable<AppointmentAPIModel>)cached_allAppointmentAsync;

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

            _cache.Set(String.Concat("ActiveIssue-", id), result, DateTimeOffset.Now.AddMinutes(10));

            return result.AsQueryable();
        }

        async Task<AppointmentAPIModel> IAppointmentManager.GetAppointmentByIdAsync(int id)
        {
            var cached_allAppointmentAsync = _cache.Get(String.Concat("PatientAppointment", id));
            if (cached_allAppointmentAsync != null)
                return (AppointmentAPIModel)cached_allAppointmentAsync;


            var appointment = await repo.GetAppointmentByIdAsync(id);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entities.Appointment, AppointmentAPIModel>();
            });
            Mapper mapper = new Mapper(config);

            //_cache.Set(String.Concat("ActiveIssue-", id), result, DateTimeOffset.Now.AddMinutes(10));/**/

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
