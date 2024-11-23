namespace CLS.Common.Times;

/// <summary>
/// Provides the current time.
/// </summary>
public class CurrentTimeProvider : ICurrentTimeProvider
{
    /// <summary>
    /// Gets the current time.
    /// </summary>
    public DateTime CurrentTime => DateTime.Now;
}
