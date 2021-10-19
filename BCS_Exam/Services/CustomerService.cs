/* 2021 Michael Robin M. Dasmariñas - rmdasma@outlook.com */

using BCS_Exam.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BCS_Exam.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient client;

        public CustomerService()
        {
            client = new HttpClient();
        }

        public async Task<List<CustomerDTO>> GetCustomers(string parkCode, string arrival)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                throw new TaskCanceledException("No Internet Connection");

            List<CustomerDTO> result = null;
            var uri = $"https://discoverycodetest.azurewebsites.net/api/NPS/Customers?parkCode={parkCode}&arriving={arrival}";
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<List<CustomerDTO>>(content);
            }
            
            return result;
        }

        public async Task<bool> SubmitResponse(string resId, string email)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                throw new TaskCanceledException("No Internet Connection");

            var uri = $"https://discoverycodetest.azurewebsites.net/api/NPS/Response";
            var request = new SubmitDTO { ResId = resId, UserEmail = email };
            HttpContent c = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, c);
            return response.IsSuccessStatusCode;
        }
    }
}