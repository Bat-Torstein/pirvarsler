using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using PirvarslerLib;

namespace Pirvarsler.Function
{
    public class PirvarslerTrigger
    {
        private readonly ILogger _logger;

        public PirvarslerTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PirvarslerTrigger>();
        }

        [Function("PirvarslerTrigger")]
        public async Task Run([TimerTrigger("0 19 * * * *")] TimerInfo myTimer)
        {
            try
            {
                _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

                await PirvarslerLib.Pirvarsler.CheckAndNotify(_logger);

                if (myTimer.ScheduleStatus is not null)
                {
                    _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
