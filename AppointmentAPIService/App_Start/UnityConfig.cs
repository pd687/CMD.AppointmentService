
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
            var unity = ConfigurationManager.GetSection("unity") as IUnityContainer;
            
            /*container.RegisterType<IAppointmentManager, AppointmentManager>();
            container.RegisterType<IAppointmentRepository, AppointmentRepository>();*/
            
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(unity);
        }
    }
}