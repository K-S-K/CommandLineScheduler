namespace CLS.Common.RequestsAndResponses;

public enum DutyControlCommandType
{
    /// <summary>
    /// The command to enable the queue.
    /// </summary>
    EnableQueue,

    /// <summary>
    /// The command to disable the queue with cancellation of the current task.
    /// </summary>
    CancelCurrentAndDisableQueue,

    /// <summary>
    /// The command to complete the current task and then disable the queue.
    /// </summary>
    CompleteCurrentAndDisableQueue,
}

/// <summary>
/// The command to control the duty queue.
/// </summary>
public class CmdDutyControl
{
    /// <summary>
    /// The new duty control value.
    /// </summary>
    public required DutyControlCommandType Command { get; set; }
}
