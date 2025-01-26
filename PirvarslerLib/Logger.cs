public class Logger
{
  public static void LogMessage(string message)
  {
    File.AppendAllText("messagelog.txt", $"{DateTime.Now}: {message}\n");
    LogOperation("Notification sent");
  }

  public static void LogOperation(string operation)
  {
    File.AppendAllText("log.txt", $"{DateTime.Now}: {operation}\n");
  }
}