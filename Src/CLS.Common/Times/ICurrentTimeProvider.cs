namespace CLS.Common.Times;

/// <summary>
/// Provides the current time.
/// </summary>
/// <remarks>
/// This interface is used to provide the current time 
/// for the <see cref="TimeController"/> class
/// in production environment and for 
/// the test environment.
/// </remarks>
public interface ICurrentTimeProvider
{
    /// <summary>
    /// Gets the current time.
    /// </summary>
    DateTime CurrentTime { get; }
}
