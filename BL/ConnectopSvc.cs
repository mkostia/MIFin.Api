using Microsoft.AspNetCore.DataProtection.KeyManagement;
using MIFin.Api.BL.Models.Connectop;
using MIFin.Api.Data;
using Newtonsoft.Json;
using RestSharp;

namespace MIFin.Api.BL {
    public class ConnectopSvc {
        private readonly IConfiguration _config;
        private string _accessToken;
        private string _apiUrl;
        private DataRepository _dataRepository;
        public ConnectopSvc(IConfiguration config, DataRepository dataRepository) {
            _config = config;
            _accessToken = config.GetValue<string>("Connectop:X-ACCESS-TOKEN")!;
            _apiUrl = config.GetValue<string>("Connectop:APIUrl")!;
            _dataRepository = dataRepository;
        }
        public async Task<GetCatFactResponse> GetCatFact(string login) {


            _dataRepository.p_WF_TraceLogInsert(1, 1, 1, "GetPageMe", "Start", login);
            var client = new RestClient("https://catfact.ninja/");
            var request = new RestRequest("fact");
            //request.AddHeader("X-ACCESS-TOKEN", _accessToken);
            var response = await client.GetAsync<GetCatFactResponse>(request);
            _dataRepository.p_WF_TraceLogInsert(1, 1, 1, "GetPageMe", JsonConvert.SerializeObject(response), login);
            return response!;
        }



        public async Task<GetPageMeResponse> GetPageMe(string login) {
            _dataRepository.p_WF_TraceLogInsert(1, 1, 1, "GetPageMe", "Start", login);
            var client = new RestClient(_apiUrl);
            var request = new RestRequest("page/me");
            request.AddHeader("X-ACCESS-TOKEN", _accessToken);
            var response = await client.GetAsync<GetPageMeResponse>(request);
            _dataRepository.p_WF_TraceLogInsert(1, 1, 1, "GetPageMe", JsonConvert.SerializeObject(response), login);
            return response!;
        }
        void SendMessage(string login, string phone, string message) {


        }
    }
}
