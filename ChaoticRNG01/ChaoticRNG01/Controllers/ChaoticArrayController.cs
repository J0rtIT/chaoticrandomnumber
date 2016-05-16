using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ChaoticRNG01.Models;
using System.Web.Mvc;

namespace ChaoticRNG01.Controllers
{
    
    public class ChaoticArrayController : ApiController
    {
        public double[] Get()
        {
            ChaoticRNG rg = new ChaoticRNG();
            rg.ArrayNumber = rg.GetArrayRN();
            return rg.ArrayNumber;
        }

        


    }
}
