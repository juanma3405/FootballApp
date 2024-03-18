using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FootballAppV2.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger) 
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ErrorMessage = "Capturando error general";
            _logger.LogError($"Ruta del ERROR: {exceptionHandlerPathFeature.Path}" +
            $"Excepcion: {exceptionHandlerPathFeature.Error}" +
            $"Traza del ERROR:{exceptionHandlerPathFeature.Error.StackTrace}");
            return View("ErrorGeneral");
        }
    }
}
