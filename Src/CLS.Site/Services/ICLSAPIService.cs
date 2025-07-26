using CLS.Common.DTO;
using CLS.Common.CommandControl;
using CLS.Common.RequestsAndResponses;

namespace CLS.Site.Services;

/// <summary>
/// The service to communicate with the CLS control API.
/// </summary>
public interface ICLSAPIService
{
    /// <summary>
    /// The List of command tasks.
    /// </summary>
    List<CommandTaskDto> Items { get; }

    /// <summary>
    /// Add URLs to the download plan.
    /// </summary>
    /// <param name="urls">The URLs to be added</param>
    /// <returns>True if the request was successful</returns>
    /// <remarks>
    /// The URLs should be separated by new lines.
    /// The backend will split the string into a list of URLs.
    /// </remarks>
    Task<bool> AddUrlsToDownloadPlan(string urls);

    /// <summary>
    /// Request for the update of the command status.
    /// </summary>
    /// <param name="id">The ID of the command to be canceled</param>
    /// <param name="status">The new status of the command</param>
    /// <returns>True if the request was successful</returns>
    Task<bool> UpdateCommandStatus(Guid id, CommandStatus status);

    /// <summary>
    /// Request the command log from the API.
    /// </summary>
    Task<CommandTaskCollectionDto> RequestCommandLog();

    /// <summary>
    /// Set the queue status in the API.
    /// </summary>
    /// <param name="cmnd">The command to set the queue status</param>
    Task<bool> SetQueueStatus(DutyControlCommandType cmnd);

    /// <summary>
    /// Get the list of updated command tasks from the API.
    /// </summary>
    /// <returns>A list of updated command tasks</returns>
    Task<List<CommandTaskDto>> GetItemUpdatesAsync();
}
