using System.Web.Mvc;

namespace Plan2015.Web.Controllers.Mvc
{
    public class EventController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Begivenheder";

            return View();
        }
    }
}