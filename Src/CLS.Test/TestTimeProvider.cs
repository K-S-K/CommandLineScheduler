using CLS.Common.Times;

namespace CLS.Test;

/// <summary>
/// A test time provider.
/// </summary>
public class TestTimeProvider : ICurrentTimeProvider
{
    /// <summary>
    /// Gets or sets the current time.
    /// Implements <see cref="ICurrentTimeProvider.CurrentTime"/>.
    /// </summary>
    public DateTime CurrentTime { get; set; }

    /// <summary>
    /// Sets the current time for the test purposes.
    /// </summary>
    /// <param name="time">The time to set.</param>
    public void SetTime(DateTime time)
    {
        CurrentTime = time;
    }
}
