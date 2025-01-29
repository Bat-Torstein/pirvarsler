using System.Globalization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace PirvarslerLib;

public class Pirvarsler
{
  public static async Task CheckAndNotify(ILogger logger)
  {
    var now = DateTime.Now;
    //var config = Config.ReadConfig() ?? throw new Exception("Invalid or missing configuration");
    var url = UriBuilder.BuildUri(now);

    var httpClient = new HttpClient();

    var response = await httpClient.GetAsync(url);

    response.EnsureSuccessStatusCode();

    // Må også sjekke hvilke tidspunkt som ligger over og trunker 
    var forecasts = (JsonConvert.DeserializeObject<Forecast>(await response.Content.ReadAsStringAsync())
      ?.Result.Forecasts) ?? throw new Exception("Unable to convert object to forecast!");

    var forecastsAboveLimit = forecasts.Where(f => (f.HigherPercentile?.Value ?? f.Measurement.Value) > Config.NotificationLimit);

    if (!forecastsAboveLimit.Any())
    {
      Console.WriteLine("All clear");
      logger.LogInformation($"No forecast items above {Config.NotificationLimit} cm");
      // TODO: Check number of days since last message and post alive message
    }
    else
    {
      var firstAboveLimit = forecastsAboveLimit.First();
      var date = DateTime.Parse(firstAboveLimit.DateTime).ToString("dd. MMM", new CultureInfo("nb-NO"));
      var times = forecastsAboveLimit.Select(c => DateTimeOffset.Parse(c.DateTime).ToString("HH:mm"));
      var message = $"I morgen {date} er det meldt høy vannstand over {Config.NotificationLimit} cm. Varselet gjelder for følgende klokkeslett: {string.Join(", ", times)}. Data er levert av © Kartverket";
      await MessageSender.SendMessage(logger, message, Config.SlackChannel);
    }
  }
}
