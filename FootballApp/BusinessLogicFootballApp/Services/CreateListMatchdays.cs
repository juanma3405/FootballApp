using BusinessLogicFootballApp.Entities;
using BusinessLogicFootballApp.Interfaces;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicFootballApp.Services
{
    public class CreateListMatchdays: ICreateListMatchdays
    {
        private readonly IAdmMatchdays _admMatchdays;

        public CreateListMatchdays(IAdmMatchdays admMatchdays)
        {
            _admMatchdays = admMatchdays;
        }

        public async Task<List<SelectListItem>> GetMatchdayList(int leagueId)
        {
            List<SelectListItem> valoresSegundoSelect = new List<SelectListItem>();
            Matchdays matchdays = await _admMatchdays.GetMatchdays(leagueId);
            int matchdaysLeague = Convert.ToInt32(matchdays.Mmatchdays);
            string fecha = "Fecha ";

            for (int value = 1; value <= matchdaysLeague; value++)
            {
                valoresSegundoSelect.Add(new SelectListItem { Value = value.ToString(), Text = fecha + value.ToString() });
            }

            return valoresSegundoSelect;
        }
    }
}
