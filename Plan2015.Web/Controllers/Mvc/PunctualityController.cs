using System.Web.Mvc;

namespace Plan2015.Web.Controllers.Mvc
{
    public class PunctualityController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Punktlighed";

            return View();
        }
    }
}