using System.Text;
using CLS.Common.ExecutionControl;

namespace CLS.Test;

public class CommandLineWrapperTests
{
    /// <summary>
    /// This test tests the SimpleCommandLineAppCode.
    /// It will run the code and check if the output is correct.
    /// It must check the following:
    /// 1. The output must contain the greeting message.
    /// 2. The output must contain the task progress messages.
    /// 3. The output must contain the input string.
    /// 4. The output must contain the goodbye message.
    /// 5. If the input string is "exit", then the program must exit.
    /// 6. If the input string is "error", then the program must throw an exception.
    /// </summary>
    [Fact]
    public async Task CommandLineWrapperPositiveTest()
    {
        // Prepare Deployment Directory for the program
        string deploymentDirectory = SampleProgram.DeploymentDirectory;

        // Deploy a simple command line app to the deployment directory
        string exeFileName = SampleProgram.Deploy(deploymentDirectory);

        StringBuilder sbOutput = new();
        string input = Guid.NewGuid().ToString();
        CommandExecutionResult result = await CommandLineWrapper.ExecuteCommand(exeFileName, input,
            OnOutputLineReceived: line => sbOutput.AppendLine(line),
            OnErrorLineReceived: line => sbOutput.AppendLine(line)
        );
        string output = sbOutput.ToString();

        // Check if the return code is zero
        Assert.Equal(CommandExecutionResult.Success, result);

        // Check output contains expected messages
        Assert.Contains("Hello, World!", output);
        Assert.Contains("Task progress: 0%", output);
        Assert.Contains("Task progress: 20%", output);
        Assert.Contains("Task progress: 40%", output);
        Assert.Contains("Task progress: 60%", output);
        Assert.Contains("Task progress: 80%", output);
        Assert.Contains("Task progress: 100%", output);
        Assert.Contains(input, output);
        Assert.Contains("Goodbye!", output);
    }

    /// <summary>
    /// This test tests the negative scenario of SimpleCommandLineAppCode.
    /// It will run the code without any input and check if the expected exception is thrown.
    /// It must check the following:
    /// 1. The program must throw an exception.
    /// </summary>
    [Fact]
    public async Task CommandLineWrapperNegativeTest()
    {
        // Prepare Deployment Directory for the program
        string deploymentDirectory = SampleProgram.DeploymentDirectory;

        // Deploy a simple command line app to the deployment directory
        string exeFileName = SampleProgram.Deploy(deploymentDirectory);

        StringBuilder sbOutput = new();
        StringBuilder sbError = new();

        // Execute the command
        CommandExecutionResult result = await CommandLineWrapper.ExecuteCommand(exeFileName, "error",
            OnOutputLineReceived: line => sbOutput.AppendLine(line),
            OnErrorLineReceived: line => sbError.AppendLine(line)
        );

        string error = sbError.ToString();
        // Unhandled exception. System.Exception: An intentional error occurred!
        // at Program.Main(String[] args) in /Users/ksk-work/Projects/CommandLineScheduler/Bin/Debug/Test/net8.0/TestConsoleProgram/Program.cs:line 30
        // at Program.<Main>(String[] args)

        // Check if the output contains the expected error message
        Assert.Contains("An intentional error occurred!", error);

        // Check if the return code is non-zero
        Assert.NotEqual(CommandExecutionResult.Success, result);
    }
}

