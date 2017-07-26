using ChaoticRNG01.Models;
using System.Web.Mvc;

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
            ChaoticRng rgn = new ChaoticRng();
            rgn.SingleNumber = rgn.GetSingleRn();

            return PartialView("_single", rgn.SingleNumber);
        }

        public ActionResult GetMultiple()
        {

            double[] array = new ChaoticArrayController().Get();
            return PartialView("_multiple", array);
        }
    }
}