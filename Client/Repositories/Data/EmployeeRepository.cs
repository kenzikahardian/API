using API.Models;
using Client.Base;
using Client.ViewModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }
        public async Task<List<RegisteredDataVM>> RegisteredData()
        {
            List<RegisteredDataVM> entities = new List<RegisteredDataVM>();

            using (var response = await httpClient.GetAsync(request + "RegisteredData/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<RegisteredDataVM>>(apiResponse);
            }
            return entities;
        }
        public HttpStatusCode Register(RegisterVM registerVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request + "Register", content).Result;
            return result.StatusCode;
        }



        /* public async Task<List<RegisterVM>> RegisteredDataView(string nik)
         {
             List<RegisterVM> entities = new List<RegisterVM>();

             using (var response = await httpClient.GetAsync(request + "RegisteredData/" + nik))
             {
                 string apiResponse = await response.Content.ReadAsStringAsync();
                 entities = JsonConvert.DeserializeObject<List<RegisterVM>>(apiResponse);
             }
             return entities;
         }*/
    }
}
