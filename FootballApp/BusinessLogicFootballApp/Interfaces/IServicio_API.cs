using BusinessLogicFootballApp.Entities;

namespace BusinessLogicFootballApp.Interfaces
{
    public interface IServicio_API
    {
        Task<Fixture> GetMatchDay(string code, int matchday);
    }
}
