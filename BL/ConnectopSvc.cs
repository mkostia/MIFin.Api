using Microsoft.AspNetCore.DataProtection.KeyManagement;
using MIFin.Api.BL.Models.Connectop;
using RestSharp;

namespace MIFin.Api.BL {
    public class ConnectopSvc {
        private readonly IConfiguration _config;
        private string _accessToken;
        private string _apiUrl;
        public ConnectopSvc(IConfiguration config) {
            _config = config;
            _accessToken = config.GetValue<string>("Connectop:X-ACCESS-TOKEN")!;
            _apiUrl = config.GetValue<string>("Connectop:APIUrl")!;
        }
        public async Task<GetPageMeResponse> GetPageMe() {
            var client = new RestClient("https://newapp.connectop.co.il/api/");
            var request = new RestRequest("page/me");
            request.AddHeader("X-ACCESS-TOKEN", _accessToken);
            var response = await client.GetAsync<GetPageMeResponse>(request);
            return response!;
        }
        void SendMessage(string login, string phone, string message) {


        }
    }
}
