namespace NutraBiotics.Services
{
    using System.Threading.Tasks;
    using Models;
    using Plugin.Connectivity;

    public class NetService
    {
        public async Task<Response> CheckConnectivity()
        {
			if (!CrossConnectivity.Current.IsConnected)
			{
				return new Response
				{
					IsSuccess = false,
					Message = "Verifique que su internet este activo.",
				};
			}

			var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
			if (!isReachable)
			{
				return new Response
				{
					IsSuccess = false,
					Message = "Verifique que tenga conexión a internet.",
				};
			}

			return new Response
			{
				IsSuccess = true,
			};        
        }
    }
}
