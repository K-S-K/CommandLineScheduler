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
}
