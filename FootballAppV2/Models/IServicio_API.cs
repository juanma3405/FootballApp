namespace FootballAppV2.Models
{
    public interface IServicio_API
    {
        Task<Fixture> GetMatchDay(string code, int matchday);
    }
}
