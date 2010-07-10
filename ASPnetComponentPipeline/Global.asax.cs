using System.Web.Mvc;
using System.Web.Routing;
using ASPnetComponentPipeline.Controllers;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace ASPnetComponentPipeline
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WindsorContainer container = CreateContainer();

            RegisterControllerFactory(container); 

            RegisterRoutes(RouteTable.Routes);
        }

        private WindsorContainer CreateContainer()
        {
            var container = new WindsorContainer(); 
            container.Register(Component.For<HomeController>().ImplementedBy<HomeController>().LifeStyle.Transient);
            container.Register(Component.For<IComponentProvider>().ImplementedBy<ComponentProvider>().LifeStyle.Transient);
            container.Register(Component.For<IActionInvoker>().ImplementedBy<ComponentiserInvoker>().LifeStyle.Transient);
            return container;
        }

        private void RegisterControllerFactory(WindsorContainer container)
        {
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
        }
    }
}