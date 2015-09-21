using System.Web.Mvc;

namespace Plan2015.Web.Controllers.Mvc
{
    public class MagicGamesSetupController : Controller
    {
        public ActionResult Setup()
        {
            ViewBag.Title = "De magiske lege";

            return View();
        }

        public ActionResult Score()
        {
            ViewBag.Title = "De magiske lege - Score";

            return View();
        }
    }
}