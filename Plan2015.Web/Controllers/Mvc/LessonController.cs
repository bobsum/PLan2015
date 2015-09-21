using System.Web.Mvc;

namespace Plan2015.Web.Controllers.Mvc
{
    public class LessonController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Begivenheder";

            return View();
        }
    }
}