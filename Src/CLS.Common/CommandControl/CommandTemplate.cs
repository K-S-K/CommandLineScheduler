namespace CLS.Common.CommandControl;

/// <summary>
/// This class is a template for creating a new command.
/// It contains the basic structure of a command:
/// The name of the template;
/// The command to be executed;
/// The directory where the command must be executed.
/// </summary>
public record CommandTemplate
{
    #region -> Properties
    public string Name { get; init; }
    public string Command { get; init; }
    public CommandRequirements Requirements { get; init; }  
    #endregion


    #region -> Constructors
    public CommandTemplate(string name, string command, CommandRequirements requirements)
    {
        Name = name;
        Command = command;
        Requirements = requirements;
    }

    public CommandTemplate()
    {
        Name = string.Empty;
        Command = string.Empty;
    }
    #endregion


    #region -> Methods
    public CommandTemplate WithCommandOnly(
        string name, string command, 
        CommandRequirements requirements)
        => new(name, command, requirements);

    public override string ToString()
        => $"{Name} ({Command})";
    #endregion
}
