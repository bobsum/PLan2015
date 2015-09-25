using System.Web.Mvc;

namespace Plan2015.Web.Controllers.Mvc
{
    public class MagicGamesSetupController : Controller
    {
        public ActionResult Marker()
        {
            ViewBag.Title = "De magiske lege - O-Løb";

            return View();
        }

        public ActionResult Setup()
        {
            ViewBag.Title = "De magiske lege - Setup";

            return View();
        }

        public ActionResult Score()
        {
            ViewBag.Title = "De magiske lege - Score";

            return View();
        }
    }
}