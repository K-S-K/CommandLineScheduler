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
    /// Request the command to be canceled.
    /// </summary>
    /// <param name="id">The ID of the command to be canceled</param>
    Task<bool> RequestCommandEsc(Guid id);
}
