using System.Web.Mvc;

namespace Plan2015.Web.Controllers.Mvc
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Start";

            return View();
        }
    }
}