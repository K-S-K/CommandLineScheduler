namespace CLS.Common.CommandControl;

/// <summary>
/// The state of a command.
/// </summary>
public enum CommandStatus
{
    /// <summary>
    /// The command is ready
    /// </summary>
    Pending = 0,

    /// <summary>
    /// The command is running
    /// </summary>
    Running = 1,

    /// <summary>
    /// The command has finished
    /// </summary>
    Completed = 2,

    /// <summary>
    /// The command has been canceled
    /// </summary>
    Canceled = 3,

    /// <summary>
    /// The command has failed
    /// </summary>
    Failed = 4,
}
