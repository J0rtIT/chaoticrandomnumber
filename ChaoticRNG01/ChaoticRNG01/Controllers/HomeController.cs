using ChaoticRNG01.Models;
using System.Linq;
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
            ChaoticRng rgn = new ChaoticRng();
            rgn.ArrayNumber = new ChaoticArrayController().Get();
            rgn.Average = rgn.ArrayNumber.Average();
            rgn.DesStad = rgn.Devest(rgn.ArrayNumber);

            return PartialView("_multiple", rgn);
        }
    }
}