using System.Web.Mvc;
using ASPnetComponentPipeline.ViewModels;

namespace ASPnetComponentPipeline.Extensions
{
    public static class ViewDataDictionaryExtensions
    {
        public static MasterPageViewModel MasterPageModel(this ViewDataDictionary viewData)
        {
            return viewData[typeof(MasterPageViewModel).Name] as MasterPageViewModel;
        }
    }
}