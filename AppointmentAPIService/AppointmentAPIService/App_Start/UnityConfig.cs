using CMD.Appointment.Domain.Managers;
using CMD.Appointment.Domain.Repositories;
using Data.Repositories;
using System.Configuration;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace AppointmentAPIService
{
	public static class UnityConfig
	{
		public static void RegisterComponents()
		{
			//var container = new UnityContainer();

			// register all your components with the container here
			// it is NOT necessary to register your controllers

			// e.g. container.RegisterType<ITestService, TestService>();

			//container.RegisterType<IAppointmentManager, AppointmentManager>();
			//container.RegisterType<IAppointmentRepository, AppointmentRepository>();

			var unity = ConfigurationManager.GetSection("unity") as IUnityContainer;

			GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(unity);

		}
	}
}