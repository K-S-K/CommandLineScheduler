namespace CLS.Site.Services;

/// <summary>
/// The type of the HTTP request.
/// </summary>
public enum RequestType
{
    /// <summary>
    /// Undefined request type.
    /// </summary>
    Undefined = 0,

    /// <summary>
    /// The POST request.
    /// </summary>
    Post,

    /// <summary>
    /// The GET request.
    /// </summary>
    Get,
}
