namespace CLS.Common.CommandControl;

public record CommandTask : CommandTemplate
{
    #region -> Properties
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
    public CommandTask(CommandTemplate commandTemplate, string directory, string arguments)
        : base(commandTemplate.Name, commandTemplate.Command,
                CommandRequirements.Arguments | CommandRequirements.Directory)
    {
        Directory = directory;
        Arguments = arguments;
    }

    public CommandTask(CommandTemplate commandTemplate, string directory)
        : base(commandTemplate.Name, commandTemplate.Command,
                CommandRequirements.Directory)
    {
        // Check if the command requires arguments.
        if (commandTemplate.Requirements.HasFlag(CommandRequirements.Arguments))
            throw new ArgumentException("The command requires arguments.");

        Directory = directory;
        Arguments = string.Empty;
    }

    public CommandTask(CommandTemplate commandTemplate)
        : base(commandTemplate.Name, commandTemplate.Command,
                CommandRequirements.None)
    {
        // Check if the command requires arguments.
        if (commandTemplate.Requirements.HasFlag(CommandRequirements.Arguments))
            throw new ArgumentException("The command requires arguments.");

        // Check if the command requires a directory.
        if (commandTemplate.Requirements.HasFlag(CommandRequirements.Directory))
            throw new ArgumentException("The command requires a directory.");

        Directory = string.Empty;
        Arguments = string.Empty;
    }
    #endregion


    #region -> Methods
    public override string ToString()
        => $"{Status}: {base.ToString()}, Arguments: {Arguments}, Directory: {Directory}";
    #endregion
}
