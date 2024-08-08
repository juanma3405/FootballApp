using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FootballAppV2.Models
{
    public class AdmLeague : IAdmLeague
    {
        private readonly AppDbContext contexto;

        public AdmLeague(AppDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<League> GetLeague(int id)
        {
            var league = await contexto.League.FindAsync(id);
            return league;  
        }

        public async Task<string> GetLeagueCode(int id)
        {
            var league = await contexto.League.FindAsync(id);
            return league?.Lcode;
        }

        public async Task<List<League>> GetLeagueList()
        {
            return await contexto.League.ToListAsync();
        }

        
    }
}
