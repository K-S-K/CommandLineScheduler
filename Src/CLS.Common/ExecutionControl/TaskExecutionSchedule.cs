namespace CLS.Common.TimeControl;

/// <summary>
/// Represents a task schedule.
/// </summary>
public class TaskExecutionSchedule
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
    /// The start date of the window.
    /// </summary>
    public DateTime DateOfBeg { get; set; }

    /// <summary>
    /// The end date of the window.
    /// </summary>
    public DateTime DateOfEnd { get; set; }

    /// <summary>
    /// The execution window of the time of the day.
    /// </summary>
    public TaskExecutionWindow ExecutionWindow { get; set; } = null!;


    public static TaskExecutionSchedule Always => new()
    {
        Id = Guid.NewGuid(),
        Name = "Always",
        DateOfBeg = DateTime.MinValue,
        DateOfEnd = DateTime.MaxValue,
        ExecutionWindow = TaskExecutionWindow.WholeDay,
    };

    public static TaskExecutionSchedule Nightly => new()
    {
        Id = Guid.NewGuid(),
        Name = "Nightly",
        DateOfBeg = DateTime.MinValue,
        DateOfEnd = DateTime.MaxValue,
        ExecutionWindow = TaskExecutionWindow.NightTime,
    };


}
