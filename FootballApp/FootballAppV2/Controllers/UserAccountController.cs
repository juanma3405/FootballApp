using Microsoft.AspNetCore.Mvc;
using FootballAppV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace FootballAppV2.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly UserManager<IdentityUser> gestionUsuarios;
        private readonly SignInManager<IdentityUser> gestionLogin;
        private readonly ILogger<UserAccountController> logger;

        public UserAccountController(UserManager<IdentityUser> gestionUsuarios, SignInManager<IdentityUser> gestionLogin, ILogger<UserAccountController> logger)
        {
            this.gestionUsuarios = gestionUsuarios;
            this.gestionLogin = gestionLogin;
            this.logger = logger;
        }

        public IActionResult Index()
        {
            RegisterModel registro = new RegisterModel();
            return View(registro);
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterModel model)
        {
            if (ModelState.IsValid) 
            {
                var usuario = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };
                var resultado = await gestionUsuarios.CreateAsync(usuario,model.Password);
                if (resultado.Succeeded)
                {
                    var token = await gestionUsuarios.GenerateEmailConfirmationTokenAsync(usuario);
                    var linkConfirmacion = "http://localhost:5120/UserAccount/ConfirmarEmail?usuarioId=" + usuario.Id + "&token=" + WebUtility.UrlEncode(token);
                    logger.Log(LogLevel.Error, linkConfirmacion);

                    if (gestionLogin.IsSignedIn(User) && User.IsInRole("Administrador"))
                    {
                        return RedirectToAction("ListaUsuarios", "Administracion");
                    }

                    ViewBag.ErrorTitle = "Registro correcto";
                    ViewBag.ErrorMessage = "Antes de iniciar sesion confirme el registro clicando en el link del email recibido";
                    return View("ErrorGeneral");
                }

                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CerrarSesion()
        {
            await gestionLogin.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel {};
            return View(model);
        }

        public async Task<IActionResult> ConfirmarEmail (string usuarioId, string token)
        {
            if (usuarioId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            var usuario = await gestionUsuarios.FindByIdAsync(usuarioId);
            if (usuario == null)
            {
                ViewBag.ErrorMessage = $"El usuario con id {usuarioId} es invalido";
                return View("ErrorGeneral");
            }
            var result = await gestionUsuarios.ConfirmEmailAsync(usuario, token);
            if (result.Succeeded)
            {
                return View();
            }
            ViewBag.ErrorTitle = "El email no pudo ser confirmado";
            return View("ErrorGeneral");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {   
            if (ModelState.IsValid)
            {
                var usuario = await gestionUsuarios.FindByEmailAsync(model.Email);
                if (usuario != null && !usuario.EmailConfirmed &&
                    (await gestionUsuarios.CheckPasswordAsync(usuario, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email todavia no confirmado");
                    return View(model);
                }

                var result = await gestionLogin.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded) 
                {
                    return RedirectToAction("index", "home");
                }
                ModelState.AddModelError(string.Empty, "Inicio de sesión no válido");
            }
            return View(model);
        }

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult>ComprobarEmail(string email)
        {
            var user = await gestionUsuarios.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"El email {email} no esta disponible.");
            }
        }

        public IActionResult OlvidoPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OlvidoPassword(OlvidoPasswordViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var usuario = await gestionUsuarios.FindByEmailAsync(model.Email);

                if (usuario != null && await gestionUsuarios.IsEmailConfirmedAsync(usuario))
                {
                    var token = await gestionUsuarios.GeneratePasswordResetTokenAsync(usuario);

                    var linkReseteaPass = "http:://localhost:5120/UserAccount/ReseteaPassword?Email=" + model.Email + "&token=" + WebUtility.UrlEncode(token);

                    logger.Log(LogLevel.Warning, linkReseteaPass);

                    return View("OlvidoPasswordConfirmacion");
                }

                return View("OlvidoPasswordConfirmacion");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ReseteaPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Token para resetear password incorrecto");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReseteaPassword(ResetPassViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await gestionUsuarios.FindByEmailAsync(model.Email);

                if (usuario != null)
                {
                    var resultado = await gestionUsuarios.ResetPasswordAsync(usuario, model.Token, model.Password);
                    if (resultado.Succeeded)
                    {
                        return View("ResetearPasswordConfirmacion");
                    }
                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
                return View("ResetearPasswordConfirmacion");
            }
            return View(model);
        }
    }
}
