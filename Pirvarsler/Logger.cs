public class Logger
{
  public static void LogMessage(string message)
  {
    File.AppendAllText("messagelog.txt", $"{DateTime.Now}: {message}");
      
  }

  public static void LogOperation(string operation)
  {
    File.AppendAllText("log.txt", $"{DateTime.Now}: {operation}");
  }
}