using FootballAppV2.Models;
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
        private readonly IAdmMatchdays _admMatchdays;
        private readonly IServicio_API _servicioAPI;

        public HomeController(ILogger<HomeController> logger, IAdmLeague admLeague, IAdmMatchdays admMatchdays, IServicio_API servicioAPI)
        {
            _logger = logger;
            _admLeague = admLeague;
            _admMatchdays = admMatchdays;
            _servicioAPI = servicioAPI;
        }

        public IActionResult Index()
        {
            /*ViewBag.Message = "Mensaje con ViewBag";
            ViewBag.Clave = "Mi clave";*/
            //List<League> leagues = _admLeague.getLeagueList();

            /*League league = _admLeague.getLeague(1);
            return View(league);*/
            List<League> leagues = _admLeague.getLeagueList();
            return View(leagues);
        }

        public IActionResult GetValoresSegundoSelect(int id)
        {
            List<SelectListItem> valoresSegundoSelect = new List<SelectListItem>();
            Matchdays matchdays = _admMatchdays.getMatchdays(id);
            int matchdaysLeague = Convert.ToInt32(matchdays.Mmatchdays);
            int value = 1;
            string fecha = "Fecha ";
            while (value <= matchdaysLeague)
            {
                valoresSegundoSelect.Add(new SelectListItem { Value = value.ToString(), Text = fecha + value.ToString() });
                value++;
            }
            return Json(valoresSegundoSelect);
        }

        public IActionResult GetCodeLeague (int id) 
        { 
            string code = _admLeague.getLeagueCode(id);
            return Json(code);
        }

        public async Task<IActionResult> Fixture(string code, int matchday)
        {
            //throw new Exception("Forzando error"); /* Prueba para capturar error general*/

            if (code == null)
            {
                return RedirectToAction("Index");
            }
            Fixture myMatchday = await _servicioAPI.GetMatchDay(code, matchday);
            if (myMatchday.matches == null || myMatchday.matches.Count == 0  )
            {
                return View("FixtureError");
            }
            return View(myMatchday);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}