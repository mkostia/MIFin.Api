using Microsoft.AspNetCore.Mvc;
using MIFin.Api.BL;
using MIFin.Api.BL.Models.Connectop;
using MIFin.Api.Data;
using Newtonsoft.Json;
using RestSharp;
using System.ComponentModel.DataAnnotations;

namespace MIFin.Api.Controllers {

    //https://restsharp.dev/usage.html#request-body
    //https://json2csharp.com/
    //[Route("api/[controller]")]
    [Route("api/[controller]/[action]")]
    public class ConnectopController : ControllerBase {
        private readonly DataRepository _dataRepository;
        private readonly ConnectopSvc _connectopSvc;
        public ConnectopController(DataRepository dataRepository, IConfiguration config
            , ConnectopSvc connectopSvc
            ) {
            _dataRepository = dataRepository;
            _connectopSvc = connectopSvc;
        }
        [HttpGet]
        public async Task<GetPageMeResponse> GetPageMe() {
           return await _connectopSvc.GetPageMe(); 
        }

       

        [HttpPost]

        void SendMessage(string login, string phone,string message ) {


        }

        [HttpGet]
        public string GetLoginByToken(string token) {
            var login = _dataRepository.GetLoginByToken(token);
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
