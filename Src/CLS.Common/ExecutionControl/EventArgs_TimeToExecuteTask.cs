namespace CLS.Common.TimeControl;

/// <summary>
/// The task execution signal event arguments.
/// </summary>
public class TimeToExecuteTaskEventArgs : EventArgs
{
    /// <summary>
    /// Gets the time of the event.
    /// </summary>
    public DateTime Time { get; init; }
}
