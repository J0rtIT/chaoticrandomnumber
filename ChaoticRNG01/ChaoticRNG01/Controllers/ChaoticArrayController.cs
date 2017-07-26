using ChaoticRNG01.Models;
using System.Web.Http;

namespace ChaoticRNG01.Controllers
{

    public class ChaoticArrayController : ApiController
    {
        public double[] Get()
        {
            return new ChaoticRng().GetArrayRn();
        }
    }
}
