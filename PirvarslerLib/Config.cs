public class Config
{
  public const string BaseUrl = "https://www.kartverket.no/api/vsl/waterLevels/";
  public const string Latitude = "63.44133";
  public const string Longitude = "10.40257";
  public const string Place ="Pirsenteret";
  public const int Interval = 60;
  public const double NotificationLimit = 320;
  public const int PredictionHours = 24;
  public const int AliveMessageDays = 14;
  public const string SlackChannel = "#pirgjengen";

  // Reading from file does not work in Azure Functions
  // public static Config? ReadConfig()
  // {
  //   return JsonConvert.DeserializeObject<Config>(
  //     File.Exists("config.json")
  //     ? File.ReadAllText("config.json")
  //     : File.ReadAllText("config.default.json"));
  // }
}