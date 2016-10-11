using System.Web.Mvc;

namespace Plan2015.Web.Controllers.Mvc
{
    public class QuizController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Quiz";

            return View();
        }
    }
}