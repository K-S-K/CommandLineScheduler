using CLS.Common.CommandControl;
using CLS.Common.DTO;
using CLS.Common.RequestsAndResponces;

namespace CLS.Site.Services;

/// <summary>
/// The service to communicate with the CLS control API.
/// </summary>
public class CLSAPIService : ICLSAPIService
{
    #region -> Private Fields
    /// <summary>
    /// The URL of the API server
    /// </summary>
    private readonly string _apiURL = null!;
    #endregion


    #region -> Constructors
    /// <summary>
    /// The constructor of the CLSControlService.
    /// </summary>
    /// <param name="apiURL">The URL of the API server</param>
    public CLSAPIService(string apiURL)
    {
        _apiURL = apiURL;
    }
    #endregion


    #region -> Public Methods
    /// <summary>
    /// Request the command log from the API.
    /// </summary>
    public async Task<CommandTaskCollectionDto> RequestCommandLog()
    {
        CommandTaskCollectionDto commands =
            await CallAPI<CommandTaskCollectionDto>(RequestType.Get, "/cmd-log");

        return commands;
    }

    /// <summary>
    /// Reqeust for the update of the command status.
    /// </summary>
    /// <param name="id">The ID of the command to be canceled</param>
    /// <param name="status">The new status of the command</param>
    /// <returns>True if the request was successful</returns>
    public async Task<bool> UpdateCommandStatus(Guid id, CommandStatus status)
    {
        CmdUpdateRequest requestData = new() { Id = id, Status = status };
        CmdRelatedResponce result =
            await CallAPI<CmdRelatedResponce>(RequestType.Put, $"/cmd-upd", requestData);

        return result.Success;
    }

    #endregion


    #region -> Implementation
    /// <summary>
    /// Call the API to control the CLS.
    /// </summary>
    /// <typeparam name="T">The type of the response data</typeparam>
    /// <param name="requestType">The type of the request</param>
    /// <param name="path">The path of the API</param>
    /// <param name="requestData">The optional data to send to the API</param>
    /// <returns>The response data from the API</returns>
    private async Task<T> CallAPI<T>(RequestType requestType, string path, object? requestData = null) where T : class, new()
    {
        T? responseData = null;
        string url = $"{_apiURL}{path}/";
        using HttpClient client = new();

        try
        {
            using HttpResponseMessage response =
                requestType == RequestType.Post ? await client.PostAsJsonAsync(url, requestData) :
                requestType == RequestType.Put ? await client.PutAsJsonAsync(url, requestData) :
                await client.GetAsync(url);

            responseData = await response.Content.ReadFromJsonAsync<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // TODO: Show the error message to the user
        }

        responseData ??= new T();

        return responseData;
    }
    #endregion
}
