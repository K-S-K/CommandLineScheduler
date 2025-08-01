using Microsoft.Extensions.Logging;

namespace CLS.Common.CommandControl;


/// <summary>
/// The log of commands.
/// </summary>
public class CommandLog : ICommandLog
{
    #region -> Fields
    /// <summary>
    /// The optional logger.
    /// </summary>
    private readonly ILogger? _logger;

    /// <summary>
    /// The list of tasks.
    /// </summary>
    private readonly List<CommandTask> _items = [];

    /// <summary>
    /// The set of updated task IDs.
    /// This is used to track which tasks have been updated.
    /// </summary>
    private readonly HashSet<Guid> _updatedTaskIds = [];

    /// <summary>
    /// The semaphore to control multithreading access to the class members.
    /// </summary>
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    #endregion


    #region -> Constructors
    /// <summary>
    /// The log of commands
    /// </summary>
    /// <param name="logger">The optional logger.</param>
    public CommandLog(ILogger? logger = null)
    {
        _logger = logger;
    }
    #endregion


    #region -> Properties
    /// <summary>
    /// Get the list of tasks.
    /// </summary>
    public IReadOnlyList<CommandTask> Items => _items;
    #endregion


    #region -> Methods
    /// <summary>
    /// Add a single task to the log.
    /// </summary>
    /// <param name="task">The task to be added.</param>
    public void AddTask(CommandTask task)
    {
        _semaphore.Wait();
        try
        {
            _items.Add(task);

            LogInformation($"Task {task.Id} added.");
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Add a list of tasks to the log.
    /// </summary>
    /// <param name="tasks">The tasks to be added.</param>
    public void AddTasks(IEnumerable<CommandTask> tasks)
    {
        _semaphore.Wait();
        try
        {
            _items.AddRange(tasks);
            tasks.Select(t => t.Id).ToList()
                .ForEach(id => _updatedTaskIds.Add(id));

            LogInformation($"{tasks.Count()} tasks added.");
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Get the next available task from the log.
    /// </summary>
    /// <param name="task">The next available task.</param>
    /// <returns>True if the task was found, otherwise false.</returns>
    public bool GetNextAvailableTask(out CommandTask? task)
    {
        _semaphore.Wait();
        try
        {
            task =
                _items.FirstOrDefault(t => t.Status == CommandStatus.Pending);

            return task is not null;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Remove a task from the log.
    /// </summary>
    /// <param name="id">The ID of the task to be removed.</param>
    public bool RemoveTask(Guid id)
    {
        _semaphore.Wait();
        try
        {
            CommandTask? task =
                _items.FirstOrDefault(t => t.Id == id);

            if (task is null)
            {
                LogInformation($"Task {id} not found.");
            }
            else
            {
                _items.Remove(task);

                LogInformation($"Task {task.Id} removed.");
            }

            return task is not null;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Update the status of a task.
    /// </summary>
    /// <param name="id">The ID of the task to be canceled.</param>
    /// <param name="status">The new status of the task.</param>
    public bool UpdateTaskStatus(Guid id, CommandStatus status)
    {
        _semaphore.Wait();
        try
        {
            CommandTask? task = _items.FirstOrDefault(t => t.Id == id);

            if (task is not null)
            {
                task.Status = status;

                _updatedTaskIds.Add(task.Id);
            }

            return task is not null;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Clear the log.
    /// </summary>
    public void Clear()
    {
        _semaphore.Wait();
        try
        {
            _items.Clear();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Get the list of recently updated items.
    /// </summary>
    /// <returns>A read-only list of recently updated items.</returns>
    /// <remarks>
    /// This method returns a list of tasks that have been updated recently.
    /// The list is cleared after each call to this method.
    /// </remarks>
    public IReadOnlyList<CommandTask> GetRecentlyUpdatedItems()
    {
        _semaphore.Wait();
        try
        {
            // Prepare the copy of the updated task IDs
            List<CommandTask> _updatedTasks = _items
                .Where(t => _updatedTaskIds.Contains(t.Id))
                .ToList();

            // Clear the updated task IDs after retrieving the tasks
            _updatedTaskIds.Clear();

            return _updatedTasks;
        }
        finally
        {
            _semaphore.Release();
        }
    }
    #endregion


    #region -> Implementations
    /// <summary>
    /// Logs the information.
    /// </summary>
    private void LogInformation(string message)
    {
        Console.WriteLine(message);
        _logger?.LogInformation(message);
    }
    #endregion
}
