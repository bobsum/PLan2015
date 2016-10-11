using System.Web.Mvc;

namespace Plan2015.Web.Controllers.Mvc
{
    public class SortingHatController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Fordelingshat";

            return View();
        }
    }
}