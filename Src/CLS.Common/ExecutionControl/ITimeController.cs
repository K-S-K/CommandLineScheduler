namespace CLS.Common.TimeControl;

/// <summary>
/// The time controller.
/// </summary>
public interface ITimeController : IDisposable
{
    #region -> Events
    /// <summary>
    /// Occurs when the time for the next task execution is reached.
    /// </summary>
    /// <remarks>
    /// The event is raised when the time for the next task execution is reached.
    /// </remarks>
    event EventHandler<TimeToExecuteTaskEventArgs>? TimeToExecuteTask;

    /// <summary>
    /// Occurs when the time controller sends a heart beat notification.
    /// </summary>
    event EventHandler<HeartBeatEventArgs>? HeartBeat;
    #endregion


    #region -> Properties
    /// <summary>
    /// Gets the actual task schedule.
    /// </summary>
    TaskExecutionSchedule Schedule { get; }

    /// <summary>
    /// Gets the status of the task execution.
    /// </summary>
    TaskExecutionStatus TaskStatus { get; }

    /// <summary>
    /// Gets the status of the time controller.
    /// </summary>
    TimeControllerStatus ControllerStatus { get; }
    #endregion


    #region -> Methods
    /// <summary>
    /// Resumes awaiting of the time for the next task execution.
    /// </summary>
    Task ResumeDuty();

    /// <summary>
    /// Pauses awaiting of the time for the next task execution.
    /// </summary>
    Task PauseDuty();

    /// <summary>
    /// Updates the schedule of the task execution.
    /// </summary>
    Task UpdateSchedule(TaskExecutionSchedule schedule);

    /// <summary>
    /// Set the random time delay 
    /// between the task executions 
    /// as prescribed at Schedule.
    /// </summary>
    /// <remarks>
    /// TODO: This method should not be called directly.
    /// </remarks>
    void UpdateRandomTimeDelay();
    #endregion
}
