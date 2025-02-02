namespace CLS.Common.CommandLibrary;

/// <summary>
/// This class is a library of commands.
/// It contains the basic structure of a command:
/// The name of the template;
/// The command to be executed;
/// The directory where the command must be executed.
/// </summary>
public class CommandTemplateLibrary : ICommandTemplateLibrary
{
    #region -> Data
    private Dictionary<string, CommandTemplate> _commands = [];
    #endregion


    #region -> Properties
    public IReadOnlyList<CommandTemplate> Items => _commands.Values.ToList();
    #endregion


    #region -> Constructors
    public static CommandTemplateLibrary Default
    {
        get
        {
            CommandTemplateLibrary lib = new();
            lib.AddCommand(new CommandTemplate("GetCurrentDirectory", "pwd"));
            lib.AddCommand(new CommandTemplate("ListDirectoryContent", "ls -la"));
            lib.AddCommand(new CommandTemplate("SetCurrentDirectory", "cd"));
            lib.AddCommand(new CommandTemplate("CreateDirectory", "mkdir"));
            lib.AddCommand(new CommandTemplate("Remove Directory", "rmdir"));
            lib.AddCommand(new CommandTemplate("DownloadFromYoutube", "yt-dlp"));
            lib.AddCommand(new CommandTemplate("Copy", "cp"));
            lib.AddCommand(new CommandTemplate("Move", "mv"));
            lib.AddCommand(new CommandTemplate("Remove", "rm"));
            lib.AddCommand(new CommandTemplate("Rename", "ren"));
            lib.AddCommand(new CommandTemplate("Execute", "exec"));
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
