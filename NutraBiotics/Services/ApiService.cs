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

		public async Task<Response> SyncOrder(
			string urlBase,
			string controller,
            List<OrderHeader> orders)
        {
			try
			{
                var syncHeaders = new List<SynOrderHeaderRequest>();
                foreach (var order in orders)
                {
                    var syncDetails = new List<SynOrderDetailRequest>();
                    foreach (var detail in order.OrderDetails)
                    {
                        syncDetails.Add(new SynOrderDetailRequest 
                        {
                            Discount = detail.Discount,
                            OrderLine = detail.OrderLine,
                            OrderQty = detail.OrderQty,
                            PartId = detail.PartId,
                            PriceListPartId = detail.PriceListPartId,
                            PartNum = detail.PartNum,
                            TaxAmt = detail.TaxAmt,
                            UnitPrice = detail.UnitPrice,
                        });
                    }

                    syncHeaders.Add(new SynOrderHeaderRequest 
                    {
                        ContactId = order.ContactId,
                        CreditHold = order.CreditHold,
                        CustomerId = order.CustomerId,
                        Date = order.Date,
                        Observations = order.Observations,
                        OrderDetails = syncDetails,
                        SalesCategory = order.SalesCategory,
                        ShipToId = order.ShipToId,
                        TermsCode = order.TermsCode,
                        UserId = order.UserId,
                    });
                }

                var request = JsonConvert.SerializeObject(syncHeaders);
				var content = new StringContent(request, Encoding.UTF8, "application/json");
				var client = new HttpClient();
				client.BaseAddress = new Uri(urlBase);
                var response = await client.PostAsync(controller, content);
				if (!response.IsSuccessStatusCode)
				{
					var answer = await response.Content.ReadAsStringAsync();
					return new Response
					{
						IsSuccess = false,
						Message = answer,
					};
				}

				return new Response
				{
					IsSuccess = true,
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