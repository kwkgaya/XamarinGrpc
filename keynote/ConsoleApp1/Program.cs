using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using WeatherClientLib;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            try
            {
                var handler = new SocketsHttpHandler();
                var httpClient = new HttpClient(handler);
                var options = new GrpcChannelOptions {HttpClient = httpClient, DisposeHttpClient = true};
                var channel = GrpcChannel.ForAddress("https://ttsl.tech/", options);
                var client = new Weather.Weather.WeatherClient(channel);
                var weatherService = new GrpcWeatherForecastService(client);

                var result = await weatherService.GetWeather();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
