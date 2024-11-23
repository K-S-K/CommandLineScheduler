namespace CLS.Common.TimeControl;

/// <summary>
/// Represents the event arguments for the heart beat event.
/// </summary>
public class HeartBeatEventArgs : EventArgs
{
    /// <summary>
    /// Gets the time of the event.
    /// </summary>
    public DateTime Time { get; init; }

    /// <summary>
    /// Gets the status of the time controller.
    /// </summary>
    public TimeControllerStatus ControllerStatus { get; init; }

    /// <summary>
    /// Gets the status of the task.
    /// </summary>
    public TaskExecutionStatus TaskStatus { get; init; }
}
