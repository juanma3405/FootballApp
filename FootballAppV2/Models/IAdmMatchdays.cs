namespace FootballAppV2.Models
{
    public interface IAdmMatchdays
    {
        Task<Matchdays> GetMatchdays(int id);
    }
}
