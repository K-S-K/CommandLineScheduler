using System.Diagnostics;

namespace CLS.Common.ExecutionControl;

/// <summary>
/// Command line wrapper for 
/// executing command line commands with output streaming.
/// </summary>
public static class CommandLineWrapper
{
    /// <summary>
    /// Executes a command and raises events for each output/error line.
    /// </summary>
    public static async Task<CommandExecutionResult> ExecuteCommand(
        string cmd, string args,
        Action<string>? OnOutputLineReceived = null,
        Action<string>? OnErrorLineReceived = null
    )
    {
        using Process process = new();

        process.StartInfo = new ProcessStartInfo
        {
            FileName = cmd,
            Arguments = args,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        process.OutputDataReceived += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
                OnOutputLineReceived?.Invoke(e.Data);
        };

        process.ErrorDataReceived += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
                OnErrorLineReceived?.Invoke(e.Data);
        };

        process.Start();

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        await process.WaitForExitAsync();

        CommandExecutionResult result = process.ExitCode == 0 ?
        CommandExecutionResult.Success : CommandExecutionResult.Failed;

        return result;
    }
}
