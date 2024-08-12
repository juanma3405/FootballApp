using BusinessLogicFootballApp.Entities;

namespace BusinessLogicFootballApp.Interfaces
{
    public interface IAdmMatchdays
    {
        Task<Matchdays> GetMatchdays(int id);
    }
}
