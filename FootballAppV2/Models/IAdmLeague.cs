namespace FootballAppV2.Models
{
    public interface IAdmLeague
    {
        League getLeague(int id);
        List<League> getLeagueList();
        string getLeagueCode(int id);
    }
}
