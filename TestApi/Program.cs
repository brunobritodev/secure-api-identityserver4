using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;

namespace TestApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // discover endpoints from metadata
            var client = new HttpClient() { BaseAddress = new Uri("http://localhost:5001") };
            var disco = await client.GetDiscoveryDocumentAsync();
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "console-test",
                ClientSecret = "console-secret",
                Scope = "report-api"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine("Access Token");
            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");
        }
    }
}
