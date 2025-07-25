using CLS.Common.CommandControl;

namespace CLS.Common.RequestsAndResponses;

/// <summary>
/// The response to a command modification request.
/// </summary>
public class CmdRelatedResponse
{
    /// <summary>
    /// The ID of the command.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The sign of success of the command modification.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The response message (for example, the reason of the failure).
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// The new status of the command.
    /// </summary>
    public CommandStatus Status { get; set; }
}
