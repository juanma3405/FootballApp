using BusinessLogicFootballApp.Entities;
using BusinessLogicFootballApp.Interfaces;
using FootballAppV2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace FootballAppV2.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAdmLeague _admLeague;
        private readonly IServicio_API _servicioAPI;
        private readonly ICreateListMatchdays _createListMatchdays;

        public HomeController(ILogger<HomeController> logger, IAdmLeague admLeague,/* IAdmMatchdays admMatchdays,*/ IServicio_API servicioAPI, ICreateListMatchdays createListMatchdays)
        {
            _logger = logger;
            _admLeague = admLeague;
            _servicioAPI = servicioAPI;
            _createListMatchdays = createListMatchdays;
        }

        public async Task<ActionResult<List<League>>> Index()
        {
            try
            {
                List<League> leagues = await _admLeague.GetLeagueList();
                return View(leagues);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        public async Task<ActionResult<string>> GetNumberMatchDays(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction("Index");
            }
            try
            {
                var valoresSegundoSelect = await _createListMatchdays.GetMatchdayList(id);
                return Json(valoresSegundoSelect);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
            
        }

        public async Task<ActionResult<string>> GetCodeLeague(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction("Index");
            }
            try
            {
                string code = await _admLeague.GetLeagueCode(id);
                if (string.IsNullOrEmpty(code))
                {
                    return Json(new { error = "Código de liga no encontrado." });
                }
                return Json(code);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        public async Task<ActionResult<Fixture>> Fixture(string code, int matchday)
        {
            //throw new Exception("Forzando error"); /* Prueba para capturar error general*/

            if (string.IsNullOrEmpty(code) || matchday <= 0)
            {
                return RedirectToAction("Index");
            }
            try
            {
                Fixture myMatchday = await _servicioAPI.GetMatchDay(code, matchday);
                if (myMatchday.matches == null || myMatchday.matches.Count == 0)
                {
                    return View("FixtureError");
                }
                return View(myMatchday);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult<ErrorViewModel> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private ActionResult HandleError(Exception ex)
        {
            _logger.LogError(ex, "Se produjo un error.");
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}