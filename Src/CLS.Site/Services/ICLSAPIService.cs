using CLS.Common.DTO;
using CLS.Common.CommandControl;
using CLS.Common.RequestsAndResponces;

namespace CLS.Site.Services;

/// <summary>
/// The service to communicate with the CLS control API.
/// </summary>
public interface ICLSAPIService
{
    /// <summary>
    /// The event that is raised when the command log is updated.
    /// </summary>
    event Action? OnUpdated;

    /// <summary>
    /// The List of command tasks.
    /// </summary>
    List<CommandTaskDto> Items { get; }

    /// <summary>
    /// Request the command log from the API.
    /// </summary>
    Task<CommandTaskCollectionDto> RequestCommandLog();

    /// <summary>
    /// Reqeust for the update of the command status.
    /// </summary>
    /// <param name="id">The ID of the command to be canceled</param>
    /// <param name="status">The new status of the command</param>
    /// <returns>True if the request was successful</returns>
    Task<bool> UpdateCommandStatus(Guid id, CommandStatus status);

    /// <summary>
    /// Set the queue status in the API.
    /// </summary>
    /// <param name="cmnd">The command to set the queue status</param>
    Task<bool> SetQueueStatus(DutyControlCommandType cmnd);
}
