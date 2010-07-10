using System.Web.Mvc;
using ASPnetComponentPipeline.ViewModels;

namespace ASPnetComponentPipeline.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.Message = "Hello ASP.net";
            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
