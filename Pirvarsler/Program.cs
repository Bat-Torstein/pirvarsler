using System.Globalization;
using Newtonsoft.Json;

try
{
  var now = DateTime.Now;
  var config = Config.ReadConfig() ?? throw new Exception("Invalid or missing configuration");
  var url = UriBuilder.BuildUri(now, config);
 
  var httpClient = new HttpClient();

  var response = await httpClient.GetAsync(url);

  response.EnsureSuccessStatusCode();

  // Må også sjekke hvilke tidspunkt som ligger over og trunker 
  var forecasts = (JsonConvert.DeserializeObject<Forecast>(await response.Content.ReadAsStringAsync())
    ?.Result.Forecasts) ?? throw new Exception("Unable to convert object to forecast!");

  var forecastsAboveLimit = forecasts.Where(f => (f.HigherPercentile?.Value ?? f.Measurement.Value) > config.NotificationLimit);

  if (!forecastsAboveLimit.Any())
  {
    Console.WriteLine("All clear");
    Logger.LogOperation($"No forecast items above {config.NotificationLimit} cm");
    // TODO: Check number of days since last message and post alive message
  }
  else
  {
    var firstAboveLimit = forecastsAboveLimit.First();
    var date = DateTime.Parse(firstAboveLimit.DateTime).ToString("dd. MMM", new CultureInfo("nb-NO"));
    var times = forecastsAboveLimit.Select(c => DateTime.Parse(c.DateTime).ToString("HH:mm"));
    var message = $"Det er meldt høy vannstand over {config.NotificationLimit} cm {date} kl {string.Join(", ", times)}. Data er levert av © Kartverket";
    await MessageSender.SendMessage(message, config.SlackChannel);
  }
}

catch (Exception ex)
{
  Logger.LogOperation(ex.Message);
}
