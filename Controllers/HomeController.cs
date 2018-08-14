using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GIATA_Integration_V2.Models;

namespace GIATA_Integration_V2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Phase1()
        {
            return View();
        }

        public ActionResult Phase2()
        {
            return View();
        }
        public ActionResult Phase3()
        {
            return View();
        }

        [HttpPost]
        public JsonResult getGiataHotels(string country)
        {
            String clientID = "23";
            Phase1Runner Phase1Model = new Phase1Runner(clientID);
            Phase1Model.updateGiataHotelsSpecific(country);
            List<GiataHotel> allGiataHotels = Phase1Model.GetAllGiataHotels();
            return Json(allGiataHotels);
        }


    }
}