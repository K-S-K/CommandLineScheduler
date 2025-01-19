using CLS.Common.CommandControl;
using CLS.Common.DTO;

namespace CLS.Site.Services;

/// <summary>
/// The service to communicate with the CLS control API.
/// </summary>
public interface ICLSAPIService
{
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
}
