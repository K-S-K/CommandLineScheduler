namespace CLS.Common.DTO;

/// <summary>
/// The data transfer object for a collection of command tasks.
/// </summary>
public class CommandTaskCollectionDto
{
    /// <summary>
    /// The collection of tasks.
    /// </summary>
    public List<CommandTaskDto> Tasks { get; set; } = [];

    public CommandTaskCollectionDto()
    {

    }
}
