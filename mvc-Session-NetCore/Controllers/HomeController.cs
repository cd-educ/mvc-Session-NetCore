using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc_Session_NetCore.DAOs;
using mvc_Session_NetCore.Models;
using Newtonsoft.Json;

namespace mvc_Session_NetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            var usuarioJson = HttpContext.Session.GetString("usuario");

            if(usuarioJson != null)
            {
                return Redirect("/Home/Private");
            } else
            {
                return View();
            }

        }

        [HttpPost]
        public IActionResult Login(string usuario, string password)
        {

            var usuarioEncontrado = UsuarioDAO.getInstancia().buscarUsuario(usuario, password);

            if(usuarioEncontrado != null)
            {

                HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(usuarioEncontrado));

                return Redirect("/Home/Private");

            } else {

                ViewBag.msg = "El usuario no existe";
                return View("Index");

            }

        }

        [HttpPost]

        public IActionResult Register(string usuario, string password)
        {

            UsuarioDAO.getInstancia().add(new Usuario(usuario, password));
            ViewBag.msg = "El usuario fue creado correctamente";
            return View("Index");

        }

        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return Redirect("/Home/Index");

        }

        public IActionResult Private()
        {

            var usuarioJson = HttpContext.Session.GetString("usuario");

            if(usuarioJson != null)
            {

                var usuario = JsonConvert.DeserializeObject<dynamic>(usuarioJson);

                ViewBag.usuario = usuario;
                return View("Private");

            } else {
                return View("Index");
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
