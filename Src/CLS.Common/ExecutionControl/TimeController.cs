using CLS.Common.Times;

namespace CLS.Common.TimeControl;

/// <summary>
/// The time controller.
/// </summary>
/// <seealso cref="ITimeController" />
public class TimeController : ITimeController
{
    #region -> Events
    /// <summary>
    /// Occurs when the time for the next task execution is reached.
    /// </summary>
    /// <remarks>
    /// The event is raised when the time for the next task execution is reached.
    /// </remarks>
    public event EventHandler<TimeToExecuteTaskEventArgs>? TimeToExecuteTask;

    /// <summary>
    /// Occurs when the time controller sends a heart beat notification.
    /// </summary>
    public event EventHandler<HeartBeatEventArgs>? HeartBeat;
    #endregion


    #region -> Fields
    private readonly ICurrentTimeProvider _currentTimeProvider;

    /// <summary>
    /// The time checking task.
    /// </summary>
    /// <remarks>
    /// The task is responsible for checking the time for the next task execution.
    /// </remarks>
    private Task _timeCheckingTask = null!;

    /// <summary>
    /// The lock object.
    /// </summary>
    private readonly SemaphoreSlim _lock = new(1);

    /// <summary>
    /// Randomizer 
    /// for the time delay time 
    /// between the task executions.
    private readonly Random _random = new();
    #endregion


    #region -> Properties
    /// <summary>
    /// Gets the actual task schedule.
    /// </summary>
    public TaskExecutionSchedule Schedule { get; private set; }

    /// <summary>
    /// Gets the status of the task execution.
    /// </summary>
    public TaskExecutionStatus TaskStatus { get; private set; }

    /// <summary>
    /// Gets the status of the time controller.
    /// </summary>
    public TimeControllerStatus ControllerStatus { get; private set; }

    /// <summary>
    /// Gets the time of the previous task completion.
    /// </summary>
    public DateTime PreviousTaskCompletionTime { get; private set; }

    /// <summary>
    /// Gets the time delay between the task executions.
    public TimeSpan RandomTimeDelay { get; private set; }
    #endregion


    #region -> Constructors
    public TimeController(ICurrentTimeProvider currentTimeProvider)
    {
        _currentTimeProvider = currentTimeProvider;

        Schedule = TaskExecutionSchedule.Allways;
        UpdateRandomTimeDelay();

        TaskStatus = TaskExecutionStatus.Waiting;
        ControllerStatus = TimeControllerStatus.Paused;
        PreviousTaskCompletionTime =
            _currentTimeProvider.CurrentTime;

        _timeCheckingTask = Task.Run(async () =>
        {
            while (true)
            {
                await CheckStatusAndSchedule();

                await Task.Delay(1000);
            }
        });
    }
    #endregion


    #region -> Methods
    public async Task PauseDuty()
    {
        // Enter the critical section asynchronously.
        await _lock.WaitAsync();

        // Pause the time controller.
        ControllerStatus = TimeControllerStatus.Paused;

        // Exit the critical section.
        _lock.Release();
    }

    public async Task ResumeDuty()
    {
        // Enter the critical section.
        await _lock.WaitAsync();

        // Resume the time controller.
        ControllerStatus = TimeControllerStatus.Running;

        // Exit the critical section.
        _lock.Release();
    }

    public async Task UpdateSchedule(TaskExecutionSchedule schedule)
    {
        // Enter the critical section.
        await _lock.WaitAsync();

        // Update the schedule.
        Schedule = schedule;

        // Exit the critical section.
        _lock.Release();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _timeCheckingTask.Dispose();
    }
    #endregion


    #region -> Implementation
    /// <summary>
    /// Set the random time delay 
    /// between the task executions 
    /// as prescribed at Schedule.
    /// </summary>
    private void UpdateRandomTimeDelay()
    {
        RandomTimeDelay = TimeSpan.FromSeconds(
            _random.Next(
                (int)Schedule.ExecutionWindow.MinDelayBetweenTasks.TotalSeconds,
                (int)Schedule.ExecutionWindow.MaxDelayBetweenTasks.TotalSeconds
            )
        );
    }

    /// <summary>
    /// Checks the status of the time controller and the schedule of the task execution.
    /// </summary>
    private async Task CheckStatusAndSchedule()
    {
        // Enter the critical section.
        await _lock.WaitAsync();

        DateTime now = _currentTimeProvider.CurrentTime;

        TimeSpan actualDelay = now - PreviousTaskCompletionTime;

        bool b = true;

        if (ControllerStatus != TimeControllerStatus.Running)
        {
            b = false;
        }

        else if (TaskStatus != TaskExecutionStatus.Waiting)
        {
            b = false;
        }

        else if (actualDelay < RandomTimeDelay)
        {
            b = false;
        }

        else if (Schedule.DateOfBeg > now.Date)
        {
            b = false;
        }

        else if (Schedule.DateOfEnd < now.Date)
        {
            b = false;
        }

        else if (Schedule.ExecutionWindow.AlowedExecutionTimeBeg > now.TimeOfDay)
        {
            b = false;
        }

        else if (Schedule.ExecutionWindow.AlowedExecutionTimeEnd < now.TimeOfDay)
        {
            b = false;
        }

        bool isTimeToExecuteTask =

            // Check the status of the time controller.
            ControllerStatus == TimeControllerStatus.Running &&

            // Check the status of the task execution.
            TaskStatus == TaskExecutionStatus.Waiting &&

            // Check the time since the last task execution completion.
            actualDelay >= RandomTimeDelay &&

            // Check the date and time of the task execution.
            Schedule.DateOfBeg <= now.Date &&
            Schedule.DateOfEnd >= now.Date &&
            Schedule.ExecutionWindow.AlowedExecutionTimeBeg <= now.TimeOfDay &&
            Schedule.ExecutionWindow.AlowedExecutionTimeEnd >= now.TimeOfDay;

        isTimeToExecuteTask = b;

        if (isTimeToExecuteTask)
        {
            // Change the status of the task execution.
            TaskStatus = TaskExecutionStatus.Running;
        }

        #region -> Send the heart beat notification
        try
        {
            HeartBeat?.Invoke(this,
                new HeartBeatEventArgs
                {
                    Time = now,
                    TaskStatus = TaskStatus,
                    ControllerStatus = ControllerStatus,
                }
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        #endregion


        #region -> Send the time to execute task notification
        if (isTimeToExecuteTask)
        {
            try
            {
                Console.WriteLine("Time to execute task.");

                // Notify user code about the time to execute the task.
                TimeToExecuteTask?.Invoke(this,
                    new TimeToExecuteTaskEventArgs
                    {
                        Time = now,
                    }
                );

                Console.WriteLine("Task executed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Set previous task completion time.
                PreviousTaskCompletionTime =
                    _currentTimeProvider.CurrentTime;

                // Reset the status of the task execution.
                TaskStatus = TaskExecutionStatus.Waiting;

                // Update the random time delay.
                UpdateRandomTimeDelay();
            }
        }
        #endregion

        // Exit the critical section.
        _lock.Release();
    }
    #endregion
}