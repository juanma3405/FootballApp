namespace FootballAppV2.Models
{
    public interface IAdmLeague
    {
        Task<League> GetLeague(int id);
        Task<List<League>> GetLeagueList();
        Task<string> GetLeagueCode(int id);
    }
}
