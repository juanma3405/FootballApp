using Newtonsoft.Json.Linq;

namespace FootballAppV2.Models
{
    public class Servicio_API: IServicio_API
    {
        private static string _key;
        private static string _value;
        private static string _baseUrl;

        public Servicio_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _key = builder.GetSection("ApiSettings:key").Value;
            _value = builder.GetSection("ApiSettings:value").Value;
            _baseUrl = builder.GetSection("ApiSettings:baseUrl").Value;
        }

        public async Task<Fixture> GetMatchDay(string code, int matchday)
        {
            Fixture myFixture = new Fixture();
            myFixture.matches = new List<Match>();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add(_key, _value);
            var response = await client.GetAsync(_baseUrl + "competitions/" + code + "/matches?matchday=" + matchday);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json_respuesta = await response.Content.ReadAsStringAsync();
                JObject jsonObj = JObject.Parse(json_respuesta);
                myFixture.matchDay = (int)jsonObj["filters"]["matchday"];
                myFixture.season = (int)jsonObj["filters"]["season"];
                foreach (JObject item in jsonObj["matches"])
                {
                    Match myMatch = new Match();
                    string homeTeam = item["homeTeam"]["shortName"].ToString();
                    string awayTeam = item["awayTeam"]["shortName"].ToString();
                    string status = item["status"].ToString();
                    int? scoreHome = null;
                    int? scoreAway = null;
                    if (status == "FINISHED")
                    {
                        scoreHome = (int)item["score"]["fullTime"]["home"];
                        scoreAway = (int)item["score"]["fullTime"]["away"];
                    }
                    DateTime date = (DateTime)item["utcDate"];
                    myMatch.homeTeam = homeTeam;
                    myMatch.awayTeam = awayTeam;
                    myMatch.status = status;
                    myMatch.scoreHome = scoreHome;
                    myMatch.scoreAway = scoreAway;
                    myMatch.date = date;
                    myFixture.matches.Add(myMatch);
                }
                return myFixture;
            }
            else
            {
                var myFixture2 = new Fixture();
                return myFixture2;
            }
        }
    }
}
