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
    /// Get the list of recently updated items.
    /// </summary>
    /// <returns>A read-only list of recently updated items.</returns>
    /// <remarks>
    /// This method returns a list of tasks that have been updated recently.
    /// The list is cleared after each call to this method.
    /// </remarks>
    IReadOnlyList<CommandTask> GetRecentlyUpdatedItems();

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
    /// Update the status of a task.
    /// </summary>
    /// <param name="id">The ID of the task to be removed.</param>
    /// <param name="status">The new status of the task.</param>
    bool UpdateTaskStatus(Guid id, CommandStatus status);

    /// <summary>
    /// Clear the log.
    /// </summary>
    void Clear();

    /// <summary>
    /// Get the next available task from the log.
    /// </summary>
    /// <param name="task">The next available task.</param>
    /// <returns>True if the task was found, otherwise false.</returns>
    bool GetNextAvailableTask(out CommandTask? task);
}
