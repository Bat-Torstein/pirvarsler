using System.Globalization;
using System.Reflection.Metadata;
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

    var forecasts = (JsonConvert.DeserializeObject<Forecast>(await response.Content.ReadAsStringAsync())
      ?.Result.Forecasts) ?? throw new Exception("Unable to convert object to forecast!");

    var relevantForecasts = forecasts.Where(f => IsRelevant(f));
          
    // We do not care about single hour spikes, so count must be more than 1
    if (relevantForecasts.Count() <= 1)
    {
      Console.WriteLine("All clear");
      logger.LogInformation($"No relevant forecast items above {Config.NotificationLimit} cm");
      // TODO: Check number of days since last message and post alive message
    }
    else
    {
      var firstAboveLimit = relevantForecasts.First();
      var date = DateTime.Parse(firstAboveLimit.DateTime).ToString("dd. MMM", new CultureInfo("nb-NO"));
      var times = relevantForecasts.Select(c => DateTimeOffset.Parse(c.DateTime).ToString("HH:mm"));
      var message = $"I morgen {date} er det meldt høy vannstand over {Config.NotificationLimit} cm. Varselet gjelder for følgende klokkeslett: {string.Join(", ", times)}. Data er levert av © Kartverket";
      await MessageSender.SendMessage(logger, message, Config.SlackChannel);
    }
  }

  private static bool IsRelevant(ForecastItem forecastItem)
  {
    // Waterlevel less than the configured limit is not relevant
    if (forecastItem.Measurement.Value < Config.NotificationLimit)
    {
        return false;
    }

    // We only care about forecasts between 6AM and 18PM
    var timeOfDay = DateTime.Parse(forecastItem.DateTime);
    if (timeOfDay.Hour < 5 || timeOfDay.Hour > 18)
    {
        return false;
    }

    return true;
  }
}
