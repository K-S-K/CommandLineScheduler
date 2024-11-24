using System.Diagnostics;

namespace CLS.Common.CommandControl;

/// <summary>
/// Command line wrapper for 
/// executing command line commands.
/// </summary>
public static class CommandLine
{
    public static string ExecuteCommand(string cmd, string args)
    {
        // Prepare a ProcessStartInfo for linux with arguments to execute the compiled program
        ProcessStartInfo startInfo = new()
        {
            FileName = cmd,
            Arguments = args,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        // Execute the compiled program
        Process program = Process.Start(startInfo)
            ?? throw new Exception("Failed to start the program process");

        // Read the output of the program
        string output = program.StandardOutput.ReadToEnd();
        string error = program.StandardError.ReadToEnd();

        // Check if the program has any errors
        if (!string.IsNullOrEmpty(error))
        {
            throw new Exception(error);
        }

        return output;
    }
}
