namespace FootballAppV2.Models
{
    public class AdmMatchdays: IAdmMatchdays
    {
        private readonly AppDbContext contexto;

        public AdmMatchdays(AppDbContext contexto)
        {
            this.contexto = contexto;
        }
        public Matchdays getMatchdays(int id)
        {
            return contexto.Matchdays.Find(id);
        }

    }
}
