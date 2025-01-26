using Newtonsoft.Json;

public class Config
{
  public required string BaseUrl { get; set; }
  public required string Latitude { get; set; }
  public required string Longitude { get; set; }
  public required string Place { get; set; }
  public required string Interval { get; set; }
  public required double NotificationLimit { get; set; }
  public required int PredictionHours { get; set; }
  public required int AliveMessageDays { get; set; }
  public required string SlackChannel { get; set; }

  public static Config? ReadConfig()
  {
    return JsonConvert.DeserializeObject<Config>(
      File.Exists("config.json")
      ? File.ReadAllText("config.json")
      : File.ReadAllText("config.default.json"));
  }
}