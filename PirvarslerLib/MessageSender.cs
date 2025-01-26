using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class MessageSender
{
  private static readonly string SlackApiUrl = "https://slack.com/api/chat.postMessage";


  public static async Task SendMessage(ILogger logger, string message, string slackChannel)
  {
    var slackApiToken = Environment.GetEnvironmentVariable("SLACK_API_TOKEN");
    try
    {
      if (slackApiToken != null)
      {
        var response = await PostMessageToSlack(slackApiToken, message, slackChannel);
        if (response)
        {
          logger.LogInformation($"Notification sent: {message}");
        }
        else
        {
          logger.LogError("Failed to post to slack");
        }
      }
      else
      {
        logger.LogInformation("No slack token found - not posting to slack");
        logger.LogInformation(message);
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
      logger.LogError($"Message was not sent! {ex.Message}");
    }
  }
}