namespace FootballAppV2.Models
{
    public class AdmLeague : IAdmLeague
    {
        private readonly AppDbContext contexto;
        private List<League> leagueList;

        public AdmLeague(AppDbContext contexto)
        {
            this.contexto = contexto;
        }
        public League getLeague(int id)
        {
            return contexto.League.Find(id);
        }

        public string getLeagueCode(int id)
        {
            return contexto.League.Find(id).Lcode;
        }

        public List<League> getLeagueList()
        {
            leagueList = contexto.League.ToList<League>();
            return leagueList;
        }

        
    }
}
