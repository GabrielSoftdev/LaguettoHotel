using LaguettoHotel.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaguettoHotel.Controllers
{
    public class AccountController : Controller
    {

        private LaguettoHotelEntities db = new LaguettoHotelEntities();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(loginUser data)
        {
            //DEBUG:
            //var json = JsonConvert.SerializeObject(data.password);
            //return Json(json);

            // esta action trata o post (login)
            if (ModelState.IsValid) //verifica se é válido
            {
                var senhaCriptografada = PasswordCipher.GetMD5(data.password);
                var usuario = db.loginUser.Where(x => x.email == data.email && x.password == senhaCriptografada).FirstOrDefault();

                if (usuario != null)
                {
                    Session["usuarioLogado"] = usuario;

                    if (usuario.foreignId != null)
                    {
                        if (usuario.tipoUsuario.ToString() == "c")
                        {
                            // VEM PRA CA CASO SEJA UM CLIENTE LOGADO
                            var cliente = db.Cliente.Where(c => c.idCliente == usuario.foreignId).FirstOrDefault();
                            Session["dadosForeign"] = cliente;
                            return RedirectToAction("Index", "Home");
                        }
                        // VEM PRA CA CASO SEJA UM FUNCIONARIO LOGADO
                        var funcionario = db.Funcionario.Where(c => c.idFuncionario == usuario.foreignId).FirstOrDefault();
                        Session["dadosForeign"] = funcionario;
                        return RedirectToAction("Index", "Home");
                    }
                    if (usuario.admin == "s")
                    {
                        return RedirectToAction("AdminIndex", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Email e /ou senha invalidos");
                return View(data);
            }
            ModelState.AddModelError(string.Empty, "Email e /ou senha invalidos");
            return View(data);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}
