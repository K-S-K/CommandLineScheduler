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
    public TimeSpan AllowedExecutionTimeBeg { get; set; }

    /// <summary>
    /// The end time of the window.
    /// </summary>
    public TimeSpan AllowedExecutionTimeEnd { get; set; }

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
        AllowedExecutionTimeBeg = TimeSpan.Zero,
        AllowedExecutionTimeEnd = TimeSpan.FromHours(24),
    };

    public static TaskExecutionWindow WorkingTime => new()
    {
        Id = Guid.NewGuid(),
        Name = "Working Time",
        MinDelayBetweenTasks = TimeSpan.FromMinutes(5),
        MaxDelayBetweenTasks = TimeSpan.FromMinutes(15),
        AllowedExecutionTimeBeg = TimeSpan.FromHours(8),
        AllowedExecutionTimeEnd = TimeSpan.FromHours(18),
    };

    public static TaskExecutionWindow EveningTime => new()
    {
        Id = Guid.NewGuid(),
        Name = "Evening Time",
        MinDelayBetweenTasks = TimeSpan.FromMinutes(5),
        MaxDelayBetweenTasks = TimeSpan.FromMinutes(15),
        AllowedExecutionTimeBeg = TimeSpan.FromHours(18),
        AllowedExecutionTimeEnd = TimeSpan.FromHours(22),
    };

    public static TaskExecutionWindow NightTime => new()
    {
        Id = Guid.Empty,
        Name = "Night Time",
        MinDelayBetweenTasks = TimeSpan.FromMinutes(5),
        MaxDelayBetweenTasks = TimeSpan.FromMinutes(15),
        AllowedExecutionTimeBeg = TimeSpan.FromHours(22),
        AllowedExecutionTimeEnd = TimeSpan.FromHours(8),
    };
}
