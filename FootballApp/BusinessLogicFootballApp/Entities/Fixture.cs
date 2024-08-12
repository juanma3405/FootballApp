namespace BusinessLogicFootballApp.Entities
{
    public class Fixture
    {
        public int season { get; set; }
        public int matchDay { get; set; }
        public int count { get; set; }
        public DateTime date { get; set; }
        public List<Match>? matches { get; set; }
    }
}
