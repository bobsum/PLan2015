﻿using System.Web.Mvc;

namespace Plan2015.Web.Controllers.Mvc
{
    public class ActivityController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Begivenheder";

            return View();
        }
    }

    public class TurnoutController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Turnout";

            return View();
        }
    }
}