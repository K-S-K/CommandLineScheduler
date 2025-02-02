using System.Text.Json.Serialization;
using CLS.Common.CommandControl;

namespace CLS.Common.DTO;

/// <summary>
/// The data transfer object for a command task.
/// </summary>
public class CommandTaskDto
{
    #region -> CommandTemplate Properties
    public required string Directory { get; init; }
    public required string Arguments { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CommandStatus Status { get; set; } = CommandStatus.Pending;
    #endregion


    #region -> CommandTask Properties
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Command { get; init; }
    #endregion



    public CommandTaskDto()
    {
    }
}


public static class CommandTaskDtoExtensions
{
    public static CommandTaskDto ToDto(this CommandTask commandTask)
    {
        return new CommandTaskDto
        {
            Id = commandTask.Id,
            Name = commandTask.Name,
            Command = commandTask.Command,
            Directory = commandTask.Directory,
            Arguments = commandTask.Arguments,
            Status = commandTask.Status
        };
    }

    public static CommandTask ToCommandTask(this CommandTaskDto dto)
    {
        return new CommandTask(
            new CommandTemplate(dto.Name, dto.Command),
            dto.Directory,
            dto.Arguments)
        {
            Id = dto.Id,
            Status = dto.Status
        };
    }
}