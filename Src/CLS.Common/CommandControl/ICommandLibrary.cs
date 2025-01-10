namespace CLS.Common.CommandControl;

/// <summary>
/// This interface is a library of commands.
/// It contains the basic structure of a command:
/// The name of the template;
/// The command to be executed;
/// The directory where the command must be executed.
/// </summary>
public interface ICommandLibrary
{
    List<CommandTemplate> Items { get; }
    CommandTemplate GetCommand(string name);
    void AddCommand(CommandTemplate command);
}