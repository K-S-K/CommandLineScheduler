namespace CLS.Common.CommandControl;


/// <summary>
/// The log of commands.
/// </summary>
public class CommandLog : ICommandLog
{
    #region -> Fields
    private readonly List<CommandTask> _items = [];
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
        => _items.Add(task);

    /// <summary>
    /// Add a list of tasks to the log.
    /// </summary>
    /// <param name="tasks">The tasks to be added.</param>
    public void AddTasks(IEnumerable<CommandTask> tasks)
        => _items.AddRange(tasks);

    /// <summary>
    /// Clear the log.
    /// </summary>
    public void Clear()
        => _items.Clear();
    #endregion
}
