namespace FootballAppV2.Models
{
    public class AdmMatchdays: IAdmMatchdays
    {
        private readonly AppDbContext contexto;

        public AdmMatchdays(AppDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<Matchdays> GetMatchdays(int id)
        {
            return await contexto.Matchdays.FindAsync(id);
        }

    }
}
