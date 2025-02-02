using System.Text.Json.Serialization;
using CLS.Common.CommandControl;

namespace CLS.Common.RequestsAndResponces;

public class CmdAddRequest
{
    public required string Name { get; init; }
    public required string Command { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public CommandRequirements Requirements { get; init; }

    public required string Directory { get; init; }
    public required string Arguments { get; init; }
}
