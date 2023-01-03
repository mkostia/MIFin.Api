﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace MIFin.Api.Controllers {

    //https://restsharp.dev/usage.html#request-body
    //https://json2csharp.com/
    //[Route("api/[controller]")]
    [Route("[controller]/[action]")]
    public class ConnectopController : ControllerBase {


        [HttpGet]
        public async Task<GetPageMeResponse> GetPageMe() {
            var client = new RestClient("https://newapp.connectop.co.il/api/");
            //client.Timeout = -1;
            var request = new RestRequest("page/me");
            request.AddHeader("X-ACCESS-TOKEN", "1209859.XaDZyY9c6R627yF9x2WfJRmIm38ItSr1XcSkMWwOxV");
            var response = await client.GetAsync<GetPageMeResponse>(request);
            //Console.WriteLine(JsonConvert.SerializeObject(response));
            return response!;
        }

        public class GetPageMeResponse {
            public int page_id { get; set; }
            public string name { get; set; }
            public bool active { get; set; }
            public int created { get; set; }
            public int total_users { get; set; }
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