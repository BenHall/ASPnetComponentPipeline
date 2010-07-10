using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace ASPnetComponentPipeline
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly WindsorContainer _container;

        public WindsorControllerFactory(WindsorContainer container)
        {
            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext context, Type controllerType)
        {
            return (IController)_container.Resolve(controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;

            if (disposable != null)
            {
                disposable.Dispose();
            }

            _container.Release(controller);
        }
    }
}