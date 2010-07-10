using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using ASPnetComponentPipeline.ViewModels;

namespace ASPnetComponentPipeline
{
    public class ComponentiserInvoker : ControllerActionInvoker 
    {
        private readonly IComponentProvider _provider;

        public ComponentiserInvoker(IComponentProvider provider)
        {
            _provider = provider;
        }

        protected override ActionResult InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, System.Collections.Generic.IDictionary<string, object> parameters)
        {
            var actionResult = base.InvokeActionMethod(controllerContext, actionDescriptor, parameters);

            ViewDataDictionary viewData = GetViewDataDictionary(actionResult);
            if (viewData != null)
            {
                CreateMasterPageViewData(viewData);

                PopulateModelWithComponentData(viewData.Model);
            }

            return actionResult;
        }

        private void PopulateModelWithComponentData(object model)
        {
            PropertyInfo[] properties = GetProperties(model);

            foreach (var property in properties)
            {
                var isPropertyComponent = Regex.Match(property.Name, "Component_(?<name>.+)");
                if(isPropertyComponent.Success)
                {
                    string componentKey = isPropertyComponent.Groups["name"].Value;
                    var component = _provider.GetString(componentKey);
                    property.SetValue(model, component, null);
                }
            }
        }

        private PropertyInfo[] GetProperties(object model)
        {
            return model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        private void CreateMasterPageViewData(ViewDataDictionary viewData)
        {
            MasterPageViewModel model = new MasterPageViewModel();

            PropertyInfo[] properties = GetProperties(model);
            foreach (var property in properties)
            {
                var component = _provider.GetString(property.Name);
                property.SetValue(model, component, null);
            }

            viewData[model.GetType().Name] = model;
        }

        private ViewDataDictionary GetViewDataDictionary(ActionResult actionResult)
        {
            ViewDataDictionary viewData = null;

            if (actionResult is ViewResult)
                viewData = ((ViewResult) actionResult).ViewData;
            return viewData;
        }
    }
}