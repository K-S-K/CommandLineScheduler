namespace CLS.Common.RequestsAndResponces;

/// <summary>
/// The responce to a command cancel request.
/// </summary>
public class CmdCancelResponce
{
    /// <summary>
    /// The ID of the command.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The successfullness sign of the request completion.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The response message (for example, the reason of the failure).
    /// </summary>
    public string? Message { get; set; }
}
