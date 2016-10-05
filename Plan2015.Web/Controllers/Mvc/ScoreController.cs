using System.Web.Mvc;

namespace Plan2015.Web.Controllers.Mvc
{
    public class ScoreController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Stilling";

            return View();
        }
    }
}