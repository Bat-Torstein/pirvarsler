public class MessageSender
{
  public static void SendMessage(string message)
  {
    try
    {
      Console.WriteLine(message);
      Logger.LogMessage(message);
    }
    catch (Exception ex)
    {
      Logger.LogOperation($"Message was not sent! {ex.Message}");
    }
  }
}