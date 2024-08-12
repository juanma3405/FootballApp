namespace BusinessLogicFootballApp.Entities
{
    public class Match
    {
        public string homeTeam { get; set; }
        public string awayTeam { get; set; }
        public int? scoreHome { get; set; }
        public int? scoreAway { get; set; }
        public DateTime date { get; set; }
        public string status { get; set; }
    }
}
