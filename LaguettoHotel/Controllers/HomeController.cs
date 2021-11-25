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
            if (Session["usuarioLogado"] != null)
            { return View(); }
            else
            {
                return RedirectToAction("Login", "Account");
            }


            //if (Session["usuarioLogado"] != null)
            //{
            //    //DEBUG:
            //    var json = JsonConvert.SerializeObject(Session["usuarioLogado"]);
            //    return Json(json, JsonRequestBehavior.AllowGet);
            //}
            //else
            //{
            //    return RedirectToAction("Login", "Account");
            //}
        }


        // GET: Home
        public ActionResult AdminIndex()
        {
           return View(); 
        }
    }
}