using BusinessLogicFootballApp.Entities;
using BusinessLogicFootballApp.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureFootballApp.ExternalServices
{
    public class Servicio_API : IServicio_API
    {
        private static string _key;
        private static string _value;
        private static string _baseUrl;

        public Servicio_API(IOptions<ApiSettings> apiSettings)
        {
            _key = apiSettings.Value.Key;
            _value = apiSettings.Value.Value;
            _baseUrl = apiSettings.Value.BaseUrl;
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
