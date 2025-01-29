
public class UriBuilder
{
  private const string StringDateFormat = "yyyy-MM-dd";
  static public string BuildUri(DateTime now)
  {
    var time = now.Date.AddDays(1).ToString(StringDateFormat);
 
    return $"{Config.BaseUrl}?latitude={Config.Latitude}&longitude={Config.Longitude}&language=nb&interval={Config.Interval}&fromTime={time}&toTime={time}&referenceCode=CD&place={Config.Place}";
  }
}