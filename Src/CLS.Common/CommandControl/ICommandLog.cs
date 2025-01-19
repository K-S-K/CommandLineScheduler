namespace CLS.Common.CommandControl;

/// <summary>
/// The log of commands.
/// </summary>
public interface ICommandLog
{
    /// <summary>   
    /// The list of tasks.
    /// </summary>
    IReadOnlyList<CommandTask> Items { get; }

    /// <summary>
    /// Add a single task to the log.
    /// </summary>
    /// <param name="task">The task to be added.</param>
    void AddTask(CommandTask task);

    /// <summary>
    /// Add a list of tasks to the log.
    /// </summary>
    /// <param name="tasks">The tasks to be added.</param>
    void AddTasks(IEnumerable<CommandTask> tasks);

    /// <summary>
    /// Remove a task from the log.
    /// </summary>
    /// <param name="id">The ID of the task to be removed.</param>
    bool RemoveTask(Guid id);

    /// <summary>
    /// Clear the log.
    /// </summary>
    void Clear();
}
