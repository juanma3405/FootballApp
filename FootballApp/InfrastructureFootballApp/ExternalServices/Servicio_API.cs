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
        private static ApiSettings _settings;
        private static HttpClient _httpClient;

        public Servicio_API(IOptions<ApiSettings> apiSettings, HttpClient httpClient)
        {
            _settings = apiSettings.Value;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add(_settings.Key, _settings.Value);
        }

        public async Task<Fixture> GetMatchDay(string code, int matchday)
        {
            try
            {
                var response = await _httpClient.GetAsync(_settings.Baseurl + "competitions/" + code + "/matches?matchday=" + matchday);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var json_respuesta = await response.Content.ReadAsStringAsync();
                    return ParseFixtureResponse(json_respuesta);
                }
                else
                {
                    return new Fixture();
                }

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return new Fixture();
        }

        private Fixture ParseFixtureResponse(string json_respuesta)
        {
            Fixture myFixture = new Fixture
            {
                matches = new List<Match>()
            };
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
    }
}
