using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class MessageSender
{
  private static readonly string SlackApiUrl = "https://slack.com/api/chat.postMessage";


  public static async Task SendMessage(string message, string slackChannel)
  {
    var slackApiToken = Environment.GetEnvironmentVariable("SLACK_API_TOKEN");
    try
    {
      Console.WriteLine(message);

      if (slackApiToken != null)
      {
        var response = await PostMessageToSlack(slackApiToken, message, slackChannel);
        if (response)
        {
          Logger.LogMessage(message);
        }
        else
        {
          Logger.LogOperation("Failed to post to slack");
        }
      }
      else
      {
        Logger.LogOperation("No slack token found - not posting to slack");
      }

      static async Task<bool> PostMessageToSlack(string token, string message, string slackChannel)
      {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var payload = new
        {
          channel = slackChannel,
          text = message
        };

        var jsonPayload = JsonConvert.SerializeObject(payload);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(SlackApiUrl, content);

        return response.IsSuccessStatusCode;
      }
    }
    catch (Exception ex)
    {
      Logger.LogOperation($"Message was not sent! {ex.Message}");
    }
  }
}