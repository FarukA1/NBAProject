using NBA.UI.Model;
using System.Globalization;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace NBA.UI.Services
{
    public class NBAService : INBAService
    {
        private readonly HttpClient _httpClient;
        private readonly string API_URL;

        public NBAService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            API_URL = "https://localhost:7204/api/Team/details";
        }

        public async Task<List<Team>> GetTeamsDetail()
        {
            List<Team> teamsDetail = new List<Team>();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, API_URL);
                var response = await _httpClient.SendAsync(request);

                if(response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var details = JsonSerializer.Deserialize<List<Team>>(responseContent);
                    teamsDetail =  details;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error", ex);
            }
            return teamsDetail;
        }
    }
}
