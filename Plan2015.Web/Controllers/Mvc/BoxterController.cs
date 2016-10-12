using System.Web.Mvc;

namespace Plan2015.Web.Controllers.Mvc
{
    public class BoxterController : Controller
    {
        public ActionResult Marker()
        {
            ViewBag.Title = "O-løb";

            return View();
        }
    }
}