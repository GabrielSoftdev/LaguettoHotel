using LaguettoHotel.Models;
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
            // esta action trata o post (login)
            if (ModelState.IsValid) //verifica se é válido
            {
                var senhaCriptografada = PasswordCipher.Hash(data.password);
                var usuario = db.loginUser.Where(x => x.email == data.email && x.password == senhaCriptografada).FirstOrDefault();

                if (usuario != null)
                {
                    Session["usuarioLogado"] = usuario;


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
            }
            return View(data);
        }

    }
}
