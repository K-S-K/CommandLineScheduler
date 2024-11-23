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
        timeController.UpdateRandomTimeDelay();

        app.MapGet("/", () => "Hello World!");

        // Subscribe to the TimeToExecuteTask event
        timeController.TimeToExecuteTask += async (sender, e) =>
        {
            Console.WriteLine($"Task executed at {e.Time:yyyy.MM.dd HH:mm:ss}.");
            await Task.Delay(1000);
        };

        // Start the time controller
        timeController.ResumeDuty();

        app.Run();
    }
}
