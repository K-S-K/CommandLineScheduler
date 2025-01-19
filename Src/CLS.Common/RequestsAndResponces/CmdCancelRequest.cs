namespace CLS.Common.RequestsAndResponces;

/// <summary>
/// The request to cancel a command.
/// </summary>
public class CmdCancelRequest
{
    /// <summary>
    /// The ID of the command to be canceled.
    /// </summary>
    public Guid Id { get; set; }
}
