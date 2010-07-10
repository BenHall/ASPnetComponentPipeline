using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;

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

            ViewDataDictionary viewData = GetViewData(actionResult);
            if (viewData != null)
            {
                CreateMasterPageViewData(viewData);

                PopulateModelWithComponentData(viewData.Model);
            }

            return actionResult;
        }

        private void PopulateModelWithComponentData(object model)
        {
            var properties = model.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

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

        private void CreateMasterPageViewData(ViewDataDictionary viewData)
        {
        }

        private ViewDataDictionary GetViewData(ActionResult actionResult)
        {
            ViewDataDictionary viewData = null;

            if (actionResult is ViewResult)
                viewData = ((ViewResult) actionResult).ViewData;
            return viewData;
        }
    }
}