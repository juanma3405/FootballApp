using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessLogicFootballApp.Interfaces
{
    public interface ICreateListMatchdays
    {
        Task<List<SelectListItem>> GetMatchdayList(int leagueId);
    }
}
