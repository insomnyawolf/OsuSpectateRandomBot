using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.Json;

namespace OsuSpectateRandomBot
{
    internal static class Program

    {
        private static async Task Main()
        {
            var BanchoHttpClient = new BanchoHttpClient();
            var Random = new Random();

            Console.CancelKeyPress += (object sender, ConsoleCancelEventArgs e) =>
            {
                e.Cancel = true;
                BanchoHttpClient.Dispose();
                Environment.Exit(0);
            };

            while (true)
            {
                List<Client> PlayersPlaying = new List<Client>();

                var response = await BanchoHttpClient.GetAsync().ConfigureAwait(false);

                foreach (var user in response.Users)
                {
                    foreach (var client in user.Value)
                    {
                        if (client.Type == (int)ClientType.Game
                         && client.Action.Id == (int)ActionType.Playing)
                        {
                            PlayersPlaying.Add(client);
                        }
                    }
                }

                if (PlayersPlaying.Count == 0)
                {
                    Console.WriteLine("Nobody Is Playing...");
                    await Task.Delay(new TimeSpan(hours: 0, minutes: 5, seconds: 0)).ConfigureAwait(false);

                    // Skips to the next iteration of the loop
                    continue;
                }

                var selected = PlayersPlaying[Random.Next(0, PlayersPlaying.Count - 1)];


                await Task.Delay(new TimeSpan(hours: 0, minutes: 5, seconds: 0)).ConfigureAwait(false);
            }
        }
    }

    public class BanchoHttpClient
    {
        private const string Url = "http://c.ripple.moe/api/v2/clients";
        private HttpClient HttpClient;

        public BanchoHttpClient()
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(Url)
            };

            // Add an Accept header for JSON format.
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<CRippleResponse> GetAsync()
        {
            var response = await HttpClient.GetAsync(string.Empty).ConfigureAwait(false);

            // List data response.
            if (!response.IsSuccessStatusCode)
            {
                Console.Error.Write(response.StatusCode);
                Environment.Exit(1);
            }

            var dataString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            // Parse the response body.
            //Make sure to add a reference to System.Net.Http.Formatting.dll

            return JsonSerializer.Deserialize<CRippleResponse>(dataString);
        }

        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}