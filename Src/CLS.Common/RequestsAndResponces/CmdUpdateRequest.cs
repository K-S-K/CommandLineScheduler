using CLS.Common.CommandControl;

namespace CLS.Common.RequestsAndResponses;

/// <summary>
/// The request to cancel a command.
/// </summary>
public class CmdUpdateRequest
{
    /// <summary>
    /// The ID of the command to be canceled.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// The new status of the command.
    /// </summary>
    public required CommandStatus Status { get; set; }
}
