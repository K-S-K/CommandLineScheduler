namespace CLS.Common.TimeControl;

/// <summary>
/// Represents the status of the task execution.
/// </summary>
public enum TaskExecutionStatus
{
    /// <summary>
    /// The task is waiting for the next execution.
    /// </summary>
    Waiting = 0,

    /// <summary>
    /// The task is running.
    /// </summary>
    Running,
}
