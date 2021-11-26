using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaguettoHotel.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            LaguettoHotel.Models.loginUser user = (LaguettoHotel.Models.loginUser)Session["usuarioLogado"];
           
            if (user != null && user.admin == "s")
            {
                ////DEBUG:
                //var json = JsonConvert.SerializeObject(Session["usuarioLogado"]);
                //return Json(json, JsonRequestBehavior.AllowGet);
                return RedirectToAction("AdminIndex", "Home");
            }
            ////DEBUG:
            //var json = JsonConvert.SerializeObject(Session["usuarioLogado"]);
            //return Json(json, JsonRequestBehavior.AllowGet);
            return View();
        }


        // GET: Home
        public ActionResult SobreNos()
        {
            return View();
        }
    }
}