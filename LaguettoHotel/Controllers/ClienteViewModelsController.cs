using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Hotel.Models;
using LaguettoHotel.Models;
using Newtonsoft.Json;

namespace LaguettoHotel.Controllers
{
    public class ClienteViewModelsController : Controller
    {
        private LaguettoHotelEntities db = new LaguettoHotelEntities();

        // GET: ClienteViewModels
        public ActionResult Index()
        {
            return View(db.ClienteViewModels.ToList());
        }

        // GET: ClienteViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteViewModel clienteViewModel = db.ClienteViewModels.Find(id);
            if (clienteViewModel == null)
            {
                return HttpNotFound();
            }
            return View(clienteViewModel);
        }

        // GET: ClienteViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteViewModels/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDpessoa,Nome,CPF,Senha,CEP,Endereco,Bairro,Cidade,Uf,Telefone")] ClienteViewModel clienteViewModel)
        {

            if (ModelState.IsValid)
            {
                //DEBUG:
                var json = JsonConvert.SerializeObject(clienteViewModel);


                //TRATANDO OS DADOS
                string senha = clienteViewModel.Senha != null ? PasswordCipher.Hash(clienteViewModel.Senha) : null;
                string cpf = Regex.Replace(clienteViewModel.CPF, "[^0-9,]", "");
                string telefone = clienteViewModel.Telefone != null ? Regex.Replace(clienteViewModel.Telefone, "[^0-9,]", "") : null;
                string cep = Regex.Replace(clienteViewModel.CEP, "[^0-9,]", "");

                if (db.Cliente.Any(o => o.CPF == cpf))
                {
                    ModelState.AddModelError(string.Empty, "O CPF já existe.");
                    return View(clienteViewModel);
                }

                // PESSOA
                Cliente c = new Cliente();
                c.Nome = clienteViewModel.Nome;
                c.CPF = cpf;
                c.CEP = cep;
                c.Logradouro = clienteViewModel.Endereco;
                c.Bairro = clienteViewModel.Bairro;
                c.Cidade = clienteViewModel.Cidade;
                c.UF = clienteViewModel.Uf;
                c.Telefone = telefone;

                db.Cliente.Add(c);

                try
                {
                    db.SaveChanges();
                    //return RedirectToAction("Login", "Account");
                    return View("Index");
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
                            string message = string.Format("{0}",
                                     subItem.ErrorMessage, entityTypeName, subItem.PropertyName);

                            ModelState.AddModelError(string.Empty, message);
                            return View(clienteViewModel);
                            //Console.WriteLine(message);
                        }
                    }
                }
            }

            return View(clienteViewModel);
        }


        // GET: ClienteViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClienteViewModel clienteViewModel = db.ClienteViewModels.Find(id);
            if (clienteViewModel == null)
            {
                return HttpNotFound();
            }
            return View(clienteViewModel);
        }

        // POST: ClienteViewModels/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDpessoa,Nome,CPF,Senha,CEP,Endereco,Bairro,Cidade,Uf,Telefone")] ClienteViewModel clienteViewModel)
        {
            if (ModelState.IsValid)
            {

                //DEBUG:
                var json = JsonConvert.SerializeObject(clienteViewModel);


                //TRATANDO OS DADOS
                string senha = clienteViewModel.Senha != null ? PasswordCipher.Hash(clienteViewModel.Senha) : null;
                string cpf = Regex.Replace(clienteViewModel.CPF, "[^0-9,]", "");
                string telefone = clienteViewModel.Telefone != null ? Regex.Replace(clienteViewModel.Telefone, "[^0-9,]", "") : null;
                string cep = Regex.Replace(clienteViewModel.CEP, "[^0-9,]", "");

                if (db.Cliente.Any(o => o.CPF == cpf))
                {
                    ModelState.AddModelError(string.Empty, "O CPF já existe.");
                    return View(clienteViewModel);
                }

                var clienteExistente = new Cliente();

                clienteExistente.Nome = clienteViewModel.Nome;
                clienteExistente.Nome = clienteViewModel.Nome;
                clienteExistente.CPF = cpf;
                clienteExistente.CEP = cep;
                clienteExistente.Logradouro = clienteViewModel.Endereco;
                clienteExistente.Bairro = clienteViewModel.Bairro;
                clienteExistente.Cidade = clienteViewModel.Cidade;
                clienteExistente.UF = clienteViewModel.Uf;
                clienteExistente.Telefone = telefone;

                db.Entry(clienteExistente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clienteViewModel);
        }

        // GET: ClienteViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: ClienteViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
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
