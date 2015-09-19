using System.Web.Mvc;

namespace Plan2015.Web.Controllers.Mvc
{
    public class MagicGamesController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "De magiske lege";

            return View();
        }
    }
}