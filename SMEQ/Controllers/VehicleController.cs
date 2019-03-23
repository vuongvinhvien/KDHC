using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMEQ.Controllers
{
    public class VehicleController : Controller
    {
        // GET: Vehicle
        public ActionResult ListVehicle()
        {
            return View();
        }
    }
}