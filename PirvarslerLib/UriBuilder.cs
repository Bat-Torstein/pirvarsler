
public class UriBuilder
{
  private const string StringDateFormat = "yyyy-MM-dd";
  static public string BuildUri(DateTime now, Config config)
  {
    var time = now.Date.AddDays(1).ToString(StringDateFormat);
 
    return $"{config.BaseUrl}?latitude={config.Latitude}&longitude={config.Longitude}&language=nb&interval={config.Interval}&fromTime={time}&toTime={time}&referenceCode=CD&place={config.Place}";
  }
}