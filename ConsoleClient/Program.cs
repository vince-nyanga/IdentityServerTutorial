using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {

         static async Task Main()
        {
            var identityServerUrl = "https://localhost:44349/";
            var apiUrl = "https://localhost:44334";

            var serverClient = new HttpClient();


            // Get the discovery document
            var discoverDocument = await serverClient.GetDiscoveryDocumentAsync(identityServerUrl);

            // Get Access token for console app client
            var tokenResponse = await serverClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoverDocument.TokenEndpoint,
                    ClientId = "console_app",
                    ClientSecret = "console_app_secret",
                    Scope = "dummy_api"
                });

            if (tokenResponse.IsError)
            {
                Console.WriteLine($"Failed to get access token. {tokenResponse.Error}");
            }

            var apiClient = new HttpClient();
            // Add Bearer toke to request header
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync($"{apiUrl}/secret");
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"API response:\n{content}");

        }
    }
}
