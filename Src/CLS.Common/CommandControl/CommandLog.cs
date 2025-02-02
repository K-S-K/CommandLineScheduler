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
        _items.Add(task);

        LogInformation($"Task {task.Id} added.");
    }

    /// <summary>
    /// Add a list of tasks to the log.
    /// </summary>
    /// <param name="tasks">The tasks to be added.</param>
    public void AddTasks(IEnumerable<CommandTask> tasks)
    {
        _items.AddRange(tasks);

        LogInformation($"{tasks.Count()} tasks added.");
    }

    /// <summary>
    /// Remove a task from the log.
    /// </summary>
    /// <param name="id">The ID of the task to be removed.</param>
    public bool RemoveTask(Guid id)
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

    /// <summary>
    /// Update the status of a task.
    /// </summary>
    /// <param name="id">The ID of the task to be canceled.</param>
    /// <param name="status">The new status of the task.</param>
    public bool UpdateTaskStatus(Guid id, CommandStatus status)
    {
        CommandTask? task = _items.FirstOrDefault(t => t.Id == id);
        if (task is not null)
            task.Status = status;

        return task is not null;
    }

    /// <summary>
    /// Clear the log.
    /// </summary>
    public void Clear()
        => _items.Clear();
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
