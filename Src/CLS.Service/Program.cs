using CLS.Common.TimeControl;

namespace CLS.Service;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddServices();

        var app = builder.Build();

        // Configure ITimeController instance
        var timeController = app.Services.GetRequiredService<ITimeController>();
        timeController.Schedule.ExecutionWindow.AlowedExecutionTimeBeg = TimeSpan.FromHours(0);
        timeController.Schedule.ExecutionWindow.AlowedExecutionTimeEnd = TimeSpan.FromHours(24);
        timeController.Schedule.ExecutionWindow.MinDelayBetweenTasks = TimeSpan.FromSeconds(5);
        timeController.Schedule.ExecutionWindow.MaxDelayBetweenTasks = TimeSpan.FromSeconds(5);

        app.MapGet("/", () => "Hello World!");

        timeController.TimeToExecuteTask += async (sender, e) =>
        {
            Console.WriteLine($"Task executed at {e.Time}.");
            await Task.Delay(1000);
        };

        app.Run();
    }
}
