namespace CLS.Common.CommandControl;

/// <summary>
/// This class is a library of commands.
/// It contains the basic structure of a command:
/// The name of the template;
/// The command to be executed;
/// The directory where the command must be executed.
/// </summary>
public class CommandLibrary : ICommandLibrary
{
    #region -> Data
    private Dictionary<string, CommandTemplate> _commands = [];
    #endregion


    #region -> Properties
    public List<CommandTemplate> Items => _commands.Values.ToList();
    #endregion


    #region -> Constructors
    public static CommandLibrary Default
    {
        get
        {
            CommandLibrary lib = new();
            lib.AddCommand(new CommandTemplate("GetCurrentDirectory", "pwd", CommandRequirements.None));
            lib.AddCommand(new CommandTemplate("ListDirectoryContent", "ls -la", CommandRequirements.Directory));
            lib.AddCommand(new CommandTemplate("SetCurrentDirectory", "cd", CommandRequirements.Arguments | CommandRequirements.Directory));
            lib.AddCommand(new CommandTemplate("CreateDirectory", "mkdir", CommandRequirements.Arguments | CommandRequirements.Directory));
            lib.AddCommand(new CommandTemplate("Remove Directory", "rmdir", CommandRequirements.Directory));
            lib.AddCommand(new CommandTemplate("DownloadFromYoutube", "yt-dlp", CommandRequirements.Arguments | CommandRequirements.Directory));
            lib.AddCommand(new CommandTemplate("Copy", "cp", CommandRequirements.Directory | CommandRequirements.Arguments));
            lib.AddCommand(new CommandTemplate("Move", "mv", CommandRequirements.Directory | CommandRequirements.Arguments));
            lib.AddCommand(new CommandTemplate("Remove", "rm", CommandRequirements.Directory));
            lib.AddCommand(new CommandTemplate("Rename", "ren", CommandRequirements.Directory | CommandRequirements.Arguments));
            lib.AddCommand(new CommandTemplate("Execute", "exec", CommandRequirements.Arguments | CommandRequirements.Directory));
            return lib;
        }
    }
    #endregion


    #region -> Methods
    public CommandTemplate GetCommand(string name)
        => _commands[name];

    public void AddCommand(CommandTemplate command)
        => _commands.Add(command.Name, command);
    #endregion
}