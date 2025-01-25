public class Forecast
{
  public required ForecastResult Result { get; set; }
}

public class ForecastResult
{
  public required IList<ForecastItem> Forecasts {get; set;}
}

public class ForecastItem
{
  public required ValueUnit Measurement { get; set; }
  public required string DateTime { get; set; }
  public ValueUnit? LowPercentile { get; set; }
  public ValueUnit? LowerPercentile { get; set; }
  public ValueUnit? HighPercentile { get; set; }
  public ValueUnit? HigherPercentile { get; set; }
}

public class ValueUnit
{
  public required double Value { get; set; }
  public required string Unit { get; set; }
}