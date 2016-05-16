using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ChaoticRNG01.Models;

namespace ChaoticRNG01.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSingle()
        {
            ChaoticRNG rgn = new ChaoticRNG();
            rgn.SingleNumber = rgn.GetSingleRN();

            return PartialView("_single", rgn.SingleNumber);
        }
    }
}