using System.Web.Mvc;

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

    public class HomeViewModel
    {
        public string Message { get; set; }
        public string Component_Title { get; set; }
    }
}
