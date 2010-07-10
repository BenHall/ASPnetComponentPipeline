using System.Web.Mvc;
using ASPnetComponentPipeline.ViewModels;

namespace ASPnetComponentPipeline.Extensions
{
    public static class ViewPageExtensions
    {
        public static MasterPageViewModel MasterPageModel(this ViewPage viewPage)
        {
            return viewPage.ViewData[typeof(MasterPageViewModel).Name] as MasterPageViewModel;
        }
    }
}