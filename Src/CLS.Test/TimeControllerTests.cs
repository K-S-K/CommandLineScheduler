namespace CLS.Test;

using System;
using System.Text;
using System.Threading;
using System.Diagnostics;

using CLS.Common.TimeControl;

/// <summary>
/// Tests for the <see cref="TimeController"/> class.
/// </summary>
public class TimeControllerTests
{
    /// <summary>
    /// Tests the <see cref="TimeController"/> class.
    /// </summary>
    [Fact]
    public async Task TestTimeController()
    {

        // Arrange
        TestTimeProvider testTimeProvider = new();
        testTimeProvider.SetTime(new(2024, 10, 15, 10, 0, 0));
        ITimeController timeController = new TimeController(testTimeProvider);

        StringBuilder sb = new();

        timeController.Schedule.ExecutionWindow.AllowedExecutionTimeBeg = TimeSpan.FromHours(0);
        timeController.Schedule.ExecutionWindow.AllowedExecutionTimeEnd = TimeSpan.FromHours(24);
        timeController.Schedule.ExecutionWindow.MinDelayBetweenTasks = TimeSpan.FromSeconds(1);
        timeController.Schedule.ExecutionWindow.MaxDelayBetweenTasks = TimeSpan.FromSeconds(1);

        Assert.Equal(TimeControllerStatus.Paused, timeController.ControllerStatus);
        Assert.Equal(TaskExecutionStatus.Waiting, timeController.TaskStatus);
        await timeController.ResumeDuty();
        Assert.Equal(TimeControllerStatus.Running, timeController.ControllerStatus);
        await timeController.PauseDuty();
        Assert.Equal(TimeControllerStatus.Paused, timeController.ControllerStatus);

        timeController.TimeToExecuteTask += async (sender, e) =>
        {
            sb.Append($"Task executed at {e.Time}.");
            Thread.Sleep(1000);
            await timeController.PauseDuty();
            sb.Append($"Controller paused.");
        };

        testTimeProvider.SetTime(new(2024, 10, 15, 10, 20, 0));
        await timeController.ResumeDuty();

        Stopwatch sw = Stopwatch.StartNew();
        while (timeController.ControllerStatus == TimeControllerStatus.Running)
        {
            await Task.Delay(100);

            if (sw.ElapsedMilliseconds > 1000000)
            {
                break;
            }
        }

        Assert.Contains("Task executed", sb.ToString());
        Assert.Contains("Controller paused", sb.ToString());
    }
}
