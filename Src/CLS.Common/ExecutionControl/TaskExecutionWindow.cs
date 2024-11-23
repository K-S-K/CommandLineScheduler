namespace CLS.Common.TimeControl;

/// <summary>
/// Represents a task schedule.
/// </summary>
public class TaskExecutionWindow
{
    /// <summary>
    /// Gets the unique identifier of the task schedule execution window.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the name of the task schedule execution window.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// The start time of the window.
    /// </summary>
    public TimeSpan AlowedExecutionTimeBeg { get; set; }

    /// <summary>
    /// The end time of the window.
    /// </summary>
    public TimeSpan AlowedExecutionTimeEnd { get; set; }

    /// <summary>
    /// The Minimal delay between tasks.
    /// </summary>
    public TimeSpan MinDelayBetweenTasks { get; set; }

    /// <summary>
    /// The Maximal delay between tasks.
    /// </summary>
    public TimeSpan MaxDelayBetweenTasks { get; set; }


    public static TaskExecutionWindow WholeDay => new()
    {
        Id = Guid.NewGuid(),
        Name = "Whole Day",
        MinDelayBetweenTasks = TimeSpan.FromMinutes(5),
        MaxDelayBetweenTasks = TimeSpan.FromMinutes(15),
        AlowedExecutionTimeBeg = TimeSpan.Zero,
        AlowedExecutionTimeEnd = TimeSpan.FromHours(24),
    };

    public static TaskExecutionWindow WorkingTime => new()
    {
        Id = Guid.NewGuid(),
        Name = "Working Time",
        MinDelayBetweenTasks = TimeSpan.FromMinutes(5),
        MaxDelayBetweenTasks = TimeSpan.FromMinutes(15),
        AlowedExecutionTimeBeg = TimeSpan.FromHours(8),
        AlowedExecutionTimeEnd = TimeSpan.FromHours(18),
    };

    public static TaskExecutionWindow EveningTime => new()
    {
        Id = Guid.NewGuid(),
        Name = "Evening Time",
        MinDelayBetweenTasks = TimeSpan.FromMinutes(5),
        MaxDelayBetweenTasks = TimeSpan.FromMinutes(15),
        AlowedExecutionTimeBeg = TimeSpan.FromHours(18),
        AlowedExecutionTimeEnd = TimeSpan.FromHours(22),
    };

    public static TaskExecutionWindow NightTime => new()
    {
        Id = Guid.Empty,
        Name = "Night Time",
        MinDelayBetweenTasks = TimeSpan.FromMinutes(5),
        MaxDelayBetweenTasks = TimeSpan.FromMinutes(15),
        AlowedExecutionTimeBeg = TimeSpan.FromHours(22),
        AlowedExecutionTimeEnd = TimeSpan.FromHours(8),
    };
}
