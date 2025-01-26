Pirvarsler will check forecasted waterlevels for the next day and send notification if above configured limit.

PirvarslerConsole
Run Pirvarsler as a console app.

Set the environment variable SLACK_API_TOKEN before running and then execute in a command window

PirvarslerLib
Library for re-use

PirvarslerFunction
The Azure Functions app running in Azure.
It will run a check for the next day at 19:00 UTC each day.

Extenstions
------------
To be able to develop and test using Visual Studio code the following extensions are requried:

.NET Install Tool
Azure Functions
Azure Resouces
C#
C# Dev kit
Azurite

Debugging Azure Functions app
----------------------------
Start azurite blob storage from command palette.
In local.settings.json set "AzureWebJobsStorage": "UseDevelopmentStorage=true",
Set the timer trigger to once each minute : "0 * * * * *"



