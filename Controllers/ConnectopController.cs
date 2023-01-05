using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using MIFin.Api.BL;
using MIFin.Api.BL.Models.Connectop;
using MIFin.Api.Data;
using Newtonsoft.Json;
using RestSharp;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MIFin.Api.Controllers {

    //https://restsharp.dev/usage.html#request-body
    //https://json2csharp.com/
    [Authorize(AuthenticationSchemes = $"ApiKey")]
    // [Authorize(AuthenticationSchemes = $"{JwtBearerDefaults.AuthenticationScheme},ApiKey")]
    [Route("api/[controller]/[action]")]
    public class ConnectopController : ControllerBase {
        private readonly DataRepository _dataRepository;
        private readonly ConnectopSvc _connectopSvc;
        private readonly IConfiguration _configuration;
   

        public ConnectopController(DataRepository dataRepository, IConfiguration configuration
            , ConnectopSvc connectopSvc
            
            ) {
            _dataRepository = dataRepository;
            _connectopSvc = connectopSvc;
            _configuration = configuration;
            //var connStr = configuration["ConnectionStrings:MFin"]!;

        }
        [HttpGet]
        public async Task<GetPageMeResponse> GetPageMe(string login) {
           return await _connectopSvc.GetPageMe(login); 
        }
        
        [HttpGet]
        public async Task<GetCatFactResponse> GetCatFact() {
           var userName = User.FindFirst(ClaimTypes.Name).Value;
           return await _connectopSvc.GetCatFact(userName); 
        }

        [HttpGet]
        public IActionResult GetSettings() {

            return Ok(new {
                XApiKey = _configuration.GetValue<string>("XApiKey")
        });
        }



        [HttpPost]

        void SendMessage(string login, string phone,string message ) {


        }

        [HttpGet]
        public string GetLoginByToken(string token) {
            var login = _dataRepository.GetUserNameByToken(token);
            return login;
        }


        [HttpGet]
        public IEnumerable<Product> GetProducts() {
            return new Product[] {new Product() { Name = "Product #1" },new Product() { Name = "Product #2" },};
        }
        [HttpGet("{id}")]
        public Product GetProduct() {
            return new Product() {
                ProductId = 1, Name = "Test Product"
            };
        }




    }









/// <summary>
/// ///////////////
/// </summary>
    public class Product {
        public  int ProductId { get; set; }
        public string Name { get; set; }
    }



}
