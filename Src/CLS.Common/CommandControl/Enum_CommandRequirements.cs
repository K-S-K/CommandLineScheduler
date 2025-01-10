namespace CLS.Common.CommandControl;

[Flags]
public enum CommandRequirements
{
    /// <summary>
    /// The command does not require any arguments.
    /// </summary>
    None=0,

    /// <summary>
    /// The command requires arguments to be executed.
    /// </summary>
    /// 
    Arguments=0b_0001,

    /// <summary>
    /// The command requires be executed at a specified directory.
    /// </summary>
    Directory=0b_0010,
}
