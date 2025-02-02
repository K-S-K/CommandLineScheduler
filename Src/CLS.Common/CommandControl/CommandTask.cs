namespace CLS.Common.CommandControl;

public record CommandTask
{
    #region -> Properties
    /// <summary>
    /// The unique identifier of the command.
    /// This is used to identify the command in the log.
    /// It is generated when the command is created.
    /// It is necessary to identify the command in the log 
    /// during modifications from the user interface.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The name of the command.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// The command to be executed.
    /// </summary>
    public string Command { get; init; }

    /// <summary>
    /// The directory where the command must be executed.
    /// </summary>
    public string Directory { get; init; }

    /// <summary>
    /// The arguments to be passed to the command.
    /// </summary>
    public string Arguments { get; init; }

    /// <summary>
    /// The status of the command.
    /// </summary>
    public CommandStatus Status { get; set; } = CommandStatus.Pending;
    #endregion


    #region -> Constructors
    public CommandTask(string name, string command, string directory, string arguments)
    {
        Name = name;
        Command = command;
        Id = Guid.NewGuid();
        Directory = directory;
        Arguments = arguments;
    }
    #endregion


    #region -> Methods
    public override string ToString()
        => $"{Status}: {base.ToString()}, Arguments: {Arguments}, Directory: {Directory}";
    #endregion
}
