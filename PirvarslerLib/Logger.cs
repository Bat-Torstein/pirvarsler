using Microsoft.Extensions.Logging;

public class ConsoleLogger : ILogger
{
    private readonly string _categoryName;

    public ConsoleLogger(string categoryName)
    {
        _categoryName = categoryName ?? throw new ArgumentNullException(nameof(categoryName));
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= LogLevel.Information;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        string message = formatter(state, exception);
        string logOutput = $"[{logLevel}] {_categoryName}: {message}";

        if (exception != null)
        {
            logOutput += $"\nException: {exception}";
        }

        Console.WriteLine(logOutput);
    }
}

public class ConsoleLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new ConsoleLogger(categoryName);
    }

    public void Dispose()
    {

    }
}