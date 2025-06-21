using System;
using System.Text;
using System.Text.Json;

using CLS.Common.DTO;
using CLS.Common.TimeControl;
using CLS.Common.CommandControl;
using CLS.Common.RequestsAndResponces;

namespace CLS.Service;

internal class Program
{
    /// <summary>
    /// The command log service instance.
    /// </summary>
    private static ICommandLog? cmdLog;

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        builder.Services.AddServices();

        var app = builder.Build();

        // Configure ITimeController instance
        ITimeController timeController = app.Services.GetRequiredService<ITimeController>();
        timeController.Schedule.ExecutionWindow.AlowedExecutionTimeBeg = TimeSpan.FromHours(0);
        timeController.Schedule.ExecutionWindow.AlowedExecutionTimeEnd = TimeSpan.FromHours(24);
        timeController.Schedule.ExecutionWindow.MinDelayBetweenTasks = TimeSpan.FromSeconds(5);
        timeController.Schedule.ExecutionWindow.MaxDelayBetweenTasks = TimeSpan.FromSeconds(5);
        timeController.UpdateRandomTimeDelay();

        // Get ICommandLog instance
        cmdLog = app.Services.GetRequiredService<ICommandLog>();


        // Prepare temporary content
        StringBuilder sb = new();
        sb.AppendLine("My downlosd list");
        sb.AppendLine("https://www.youtube.com/watch?v=6Dh-RL__uN4");
        sb.AppendLine("https://www.youtube.com/watch?v=6Dh-RL__uN5");
        sb.AppendLine("");
        sb.AppendLine("https://www.youtube.com/watch?v=6Dh-RL__uN6");
        string inputText = sb.ToString();

        // Parse the content
        Common.ScriptProcessing.YTDLParser parser = new(inputText);

        // Get the list of tasks
        List<CommandTask> tasks = parser.GetDownloadTasks("");

        // Add the tasks to the log
        cmdLog.AddTasks(tasks);



        app.MapGet("/", () => "Hello World!");
        app.MapGet("/cmd-log", GetCommandLog);  // http://localhost:5375/cmd-log
        app.MapPut("/cmd-upd", PutCommandUpd);  // http://localhost:5375/cmd-esc
        app.MapPut("/queue-control", PutDutyControl); // http://localhost:5375/duty-control

        // Subscribe to the TimeToExecuteTask event
        timeController.TimeToExecuteTask += ExecuteNextTask;

        // Start the time controller
        timeController.ResumeDuty();

        app.Run();
    }

    private static void ExecuteNextTask(object? sender, TimeToExecuteTaskEventArgs args)
    {
        if (cmdLog == null)
        {
            Console.WriteLine("Command log service not found.");
            return;
        }

        if (!cmdLog.GetNextAvailableTask(out CommandTask? nextTask) || nextTask == null)
        {
            Console.WriteLine("No tasks available for execution.");
            return;
        }

        // Execute the task
        cmdLog.UpdateTaskStatus(nextTask.Id, CommandStatus.Running);
        Console.WriteLine($"Executing task {nextTask.Id} at {DateTime.Now:yyyy.MM.dd HH:mm:ss}.");

        try
        {
            // Simulate task execution (should be async in real scenario)
            Task.Delay(1000).Wait();

            // TODO: Implement the actual task execution logic here
            // nextTask.Execute();

            // Mark the task as completed
            cmdLog.UpdateTaskStatus(nextTask.Id, CommandStatus.Completed);
        }
        catch (Exception ex)
        {
            // Mark the task as failed
            cmdLog.UpdateTaskStatus(nextTask.Id, CommandStatus.Failed);
            Console.WriteLine($"Error executing task {nextTask.Id}: {ex.Message}");
        }
    }

    /// <summary>
    /// Get the command log.
    /// </summary>
    /// <param name="context">The HttpContext of the request.</param>
    /// <param name="commandLog">The command log service instance from DI.</param>
    /// <returns>the responce to the client containing the command log.</returns>
    private static async Task GetCommandLog(HttpContext context, ICommandLog commandLog)
    {
        CommandTaskCollectionDto data = new()
        {
            Tasks = commandLog.Items.Select(t => t.ToDto()).ToList()
        };

        await PublishResponce(context, data);
    }

    /// <summary>
    /// Update the command status.
    /// </summary>
    /// <param name="context">The HttpContext of the request.</param>
    /// <param name="request">The request data from the client.</param>
    /// <param name="commandLog">The command log service instance from DI.</param>
    /// <returns>the responce to the client containing the result of the operation.</returns>
    private static async Task PutCommandUpd(HttpContext context, CmdUpdateRequest request, ICommandLog commandLog)
    {
        bool success = commandLog.UpdateTaskStatus(request.Id, request.Status);

        CmdRelatedResponce responce = new()
        {
            Id = request.Id,
            Success = success,
            Status = request.Status,
            Message = success ? "Task status updated." : "Task not found."
        };

        await PublishResponce(context, responce);
    }

    /// <summary>
    /// Publish the responce to the client.
    /// </summary>
    /// <param name="context">The HttpContext of the request.</param>
    /// <param name="data">The responce data prepared for the client.</param>
    /// <returns></returns>
    private static async Task PublishResponce(HttpContext context, object? data)
    {
        ArgumentNullException.ThrowIfNull(data);

        context.Response.Headers.CacheControl = "no-cache";
        context.Response.ContentType = "application/json";

        string responceStr = JsonSerializer.Serialize(data);

        await context.Response.WriteAsync(responceStr);
    }

    /// <summary>
    /// Set the duty control status.
    /// </summary>
    private static async Task PutDutyControl(HttpContext context, CmdDutyControl request, ITimeController timeController)
    {
        if (request is null)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return;
        }

        if (request.Command == DutyControlCommandType.EnableQueue)
        {
            await timeController.ResumeDuty();
        }
        else
        {
            await timeController.PauseDuty();
        }

        await PublishResponce(context, new { Success = true, Message = "Duty control updated." });
    }
}
