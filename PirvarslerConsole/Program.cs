using Microsoft.Extensions.Logging;
using PirvarslerLib;

try
{
  using var loggerFactory = LoggerFactory.Create(builder =>
       {
         builder.AddProvider(new ConsoleLoggerProvider());
       });

  var logger = loggerFactory.CreateLogger<Program>();

  await Pirvarsler.CheckAndNotify(logger);
}

catch (Exception ex)
{
  Console.WriteLine(ex);
}
