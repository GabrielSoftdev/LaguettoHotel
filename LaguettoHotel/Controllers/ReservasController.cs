using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LaguettoHotel.Models;
using Newtonsoft.Json;

namespace LaguettoHotel.Controllers
{
    public class ReservasController : Controller
    {
        private LaguettoHotelWebEntities db = new LaguettoHotelWebEntities();

        // GET: Reservas
        public ActionResult Index()
        {
            LaguettoHotel.Models.loginUser user = (LaguettoHotel.Models.loginUser)Session["usuarioLogado"];
            if (user != null && user.admin == "s")
            {
                return View(db.Reserva.ToList());
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Reservas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reserva.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // GET: Reservas/Create
        public ActionResult Create()
        {
            LaguettoHotel.Models.loginUser user = (LaguettoHotel.Models.loginUser)Session["usuarioLogado"];
            if (user != null)
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: Reservas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reserva reserva)
        {
            LaguettoHotel.Models.Cliente user = (LaguettoHotel.Models.Cliente)Session["dadosForeign"];

            reserva.Nome = user.Nome;
            reserva.valorDiaria = "80,00";

            TimeSpan date = Convert.ToDateTime(reserva.dataSaida) - Convert.ToDateTime(reserva.dataEntrada);

            int totalDias = date.Days;

            reserva.valorTotal = (totalDias * 80).ToString();

            //DEBUG
            //var json = JsonConvert.SerializeObject(reserva);
            //return Json(json);

            if (ModelState.IsValid)
            {
                db.Reserva.Add(reserva);
                try
                {
                    LaguettoHotel.Models.Cliente cliente = (LaguettoHotel.Models.Cliente)Session["dadosForeign"];
                    if (db.SaveChanges() != 0)
                    {
                        return RedirectToAction("Details", "ClienteViewModels", new { id = cliente.idCliente });
                    }

                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                    {
                        // Get entry

                        DbEntityEntry entry = item.Entry;
                        string entityTypeName = entry.Entity.GetType().Name;

                        // Display or log error messages

                        foreach (DbValidationError subItem in item.ValidationErrors)
                        {
                            //occurred in {1} at {2}
                            string message = string.Format("{0}",
                                     subItem.ErrorMessage, entityTypeName, subItem.PropertyName);

                            ModelState.AddModelError(string.Empty, message);
                            return View(reserva);
                            //Console.WriteLine(message);
                        }
                    }
                }
            }

            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reserva.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Reserva reserva)
        {
            LaguettoHotel.Models.Cliente cliente = (LaguettoHotel.Models.Cliente)Session["dadosForeign"];
            reserva.Nome = cliente.Nome;
            reserva.valorDiaria = "80,00";

            TimeSpan date = Convert.ToDateTime(reserva.dataSaida) - Convert.ToDateTime(reserva.dataEntrada);

            int totalDias = date.Days;

            reserva.valorTotal = (totalDias * 80).ToString();


            if (ModelState.IsValid)
            {
                db.Entry(reserva).State = EntityState.Modified;
                try
                {
                    if (db.SaveChanges() != 0)
                    {
                        return RedirectToAction("Details", "ClienteViewModels", new { id = cliente.idCliente });
                    }

                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                    {
                        // Get entry

                        DbEntityEntry entry = item.Entry;
                        string entityTypeName = entry.Entity.GetType().Name;

                        // Display or log error messages

                        foreach (DbValidationError subItem in item.ValidationErrors)
                        {
                            //occurred in {1} at {2}
                            string message = string.Format("{0}",
                                     subItem.ErrorMessage, entityTypeName, subItem.PropertyName);

                            ModelState.AddModelError(string.Empty, message);
                            return View(reserva);
                            //Console.WriteLine(message);
                        }
                    }
                }
            }
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public ActionResult CancelarReserva(int? id)
        {
            LaguettoHotel.Models.Cliente cliente = (LaguettoHotel.Models.Cliente)Session["dadosForeign"];

            Reserva reserva = db.Reserva.Find(id);
            db.Reserva.Remove(reserva);
            db.SaveChanges();

            return RedirectToAction("Details", "ClienteViewModels", new { id = cliente.idCliente });
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserva reserva = db.Reserva.Find(id);
            db.Reserva.Remove(reserva);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
