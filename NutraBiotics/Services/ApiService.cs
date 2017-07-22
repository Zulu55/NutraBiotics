namespace NutraBiotics.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
	using System.Text;
	using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;

	public class ApiService
    {
        public async Task<Response> Login(
            string urlBase, 
            string controller,
            string email,
            string password)
        {
            try
            {
                var loginRequest = new LoginRequest
                {
                    Email = email,
                    Password = password,
                };

                var body = JsonConvert.SerializeObject(loginRequest);
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var response = await client.PostAsync(controller, content);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    return new Response
                    {
                        IsSuccess = false,
                        Message = error.Message,
                    };
                }

                var user = JsonConvert.DeserializeObject<User>(result);
				return new Response
				{
					IsSuccess = true,
					Result = user,
				};
			}
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

		public async Task<Response> GetList<T>(
			string urlBase,
			string controller) 
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var response = await client.GetAsync(controller);
                if (!response.IsSuccessStatusCode)
                {
					return new Response
					{
						IsSuccess = false,
						Message = "No se pudo descargar.",
					};
				}

				var answer = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<T>>(answer);
				return new Response
				{
					IsSuccess = true,
					Result = list,
				};
			}
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
