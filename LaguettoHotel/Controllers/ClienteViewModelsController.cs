using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using LaguettoHotel.Models;
using Newtonsoft.Json;

namespace LaguettoHotel.Controllers
{
    public class ClienteViewModelsController : Controller
    {
        private LaguettoHotelWebEntities db = new LaguettoHotelWebEntities();
        // GET: ClienteViewModels
        public ActionResult Index()
        {
            LaguettoHotel.Models.loginUser user = (LaguettoHotel.Models.loginUser)Session["usuarioLogado"];
            if (user != null && user.admin == "s")
            {
                return View(db.Cliente.ToList());
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: ClienteViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            LaguettoHotel.Models.Cliente clienteLogado = (LaguettoHotel.Models.Cliente)Session["dadosForeign"];

            if (clienteLogado != null)
            {
                
                var cliente = (from a in db.Cliente
                               where a.idCliente == clienteLogado.idCliente
                               select a).FirstOrDefault();

                var reserva = (from a in db.Reserva
                               where a.CPF == cliente.CPF
                               select a).ToList();

                if (cliente == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                var model = new CustomerDetailsViewModel { _cliente = cliente, _reserva = reserva };

                return View(model);

            }
            return RedirectToAction("Index", "Home");
    }

          

    // GET: ClienteViewModels/Create
    public ActionResult Create()
    {
        LaguettoHotel.Models.loginUser user = (LaguettoHotel.Models.loginUser)Session["usuarioLogado"];
        if (user != null && user.admin != "s")
        {
            return View();
        }
        if (user == null)
        {
            return View();
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(ClienteViewModel clienteViewModel)
    {

        //DEBUG:
        //var json = JsonConvert.SerializeObject(clienteViewModel);
        //ViewBag.post = json;

        //return Json(json);

        if (ModelState.IsValid)
        {
            //TRATANDO OS DADOS
            string senha = clienteViewModel.Senha != null ? PasswordCipher.GetMD5(clienteViewModel.Senha) : null;
            string cpf = Regex.Replace(clienteViewModel.CPF, "[^0-9,]", "");
            string telefone = clienteViewModel.Telefone != null ? Regex.Replace(clienteViewModel.Telefone, "[^0-9,]", "") : null;
            string cep = Regex.Replace(clienteViewModel.CEP, "[^0-9,]", "");
            string usuario = clienteViewModel.Nome.Split(' ')[0];


            // VALIDANDO DADOS UNICOS
            if (db.Cliente.Any(o => o.CPF == cpf))
            {
                ModelState.AddModelError(string.Empty, "O CPF já existe.");
                return View(clienteViewModel);
            }

            if (db.loginUser.Any(o => o.email == clienteViewModel.Email))
            {
                ModelState.AddModelError(string.Empty, "O Email já existe.");
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
                // PEGANDO O ID DO CLIENTE INSERIDO
                int lastInsertedId = c.idCliente;

                // USUARIO
                loginUser u = new loginUser();
                u.Username = usuario;
                u.password = senha;
                u.email = clienteViewModel.Email;
                u.tipoUsuario = "c";
                u.admin = "n";
                u.foreignId = lastInsertedId;

                db.loginUser.Add(u);

                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Login", "Account");
                    //return View("Index");
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
                            return View(clienteViewModel);
                            //Console.WriteLine(message);
                        }
                    }
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
        Cliente clienteViewModel = db.Cliente.Find(id);
        if (clienteViewModel == null)
        {
            return HttpNotFound();
        }
        return View(clienteViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "IDpessoa,Nome,CPF,Senha,CEP,Endereco,Bairro,Cidade,Uf,Telefone")] ClienteViewModel clienteViewModel)
    {
        if (ModelState.IsValid)
        {

            //DEBUG:
            var json = JsonConvert.SerializeObject(clienteViewModel);


            //TRATANDO OS DADOS
            string senha = clienteViewModel.Senha != null ? PasswordCipher.GetMD5(clienteViewModel.Senha) : null;
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
        LaguettoHotel.Models.loginUser user = (LaguettoHotel.Models.loginUser)Session["usuarioLogado"];

        if (user != null && user.admin == "s")
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


        return RedirectToAction("Index", "Home");
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
