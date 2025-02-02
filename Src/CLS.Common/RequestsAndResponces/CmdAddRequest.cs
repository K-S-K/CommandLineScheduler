namespace CLS.Common.RequestsAndResponces;

public class CmdAddRequest
{
    public required string Name { get; init; }
    public required string Command { get; init; }

    public required string Directory { get; init; }
    public required string Arguments { get; init; }
}
