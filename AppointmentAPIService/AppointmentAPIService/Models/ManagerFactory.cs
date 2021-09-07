using CMD.Appointment.Domain.Managers;
using CMD.Appointment.Domain.Repositories;
using Data.Repositories;
using Unity;

namespace AppointmentAPIService.Models
{
    public class ManagerFactory
    {
        private static readonly IUnityContainer container;
        static ManagerFactory()
        {
            container = new UnityContainer();
            container.RegisterType<IAppointmentManager, AppointmentManager>();
            container.RegisterType<IAppointmentRepository, AppointmentRepository>();
        }





        public static IAppointmentManager CreateManager()
        {
            return container.Resolve<IAppointmentManager>();
        }
    }
}