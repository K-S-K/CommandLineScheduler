using System.Diagnostics;

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
    public void CommandLineWrapperPositiveTest()
    {
        // Prepare Deployment Directory for the program
        string deploymentDirectory =
            Path.Combine(SampleProgram.CurrentDirectory, "Program");

        // Deploy a simple command line app to the deployment directory
        string exeFileName = SampleProgram.Deploy(deploymentDirectory);

        string input = Guid.NewGuid().ToString();
        string output = CommandLineWrapper.ExecuteCommand(exeFileName, input);

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
    public void CommandLineWrapperNegativeTest()
    {
        // Prepare Deployment Directory for the program
        string deploymentDirectory =
            Path.Combine(SampleProgram.CurrentDirectory, "Program");

        // Deploy a simple command line app to the deployment directory
        string exeFileName = SampleProgram.Deploy(deploymentDirectory);

        // Check for the expected exception
        Assert.Throws<Exception>(
            () => CommandLineWrapper.ExecuteCommand(exeFileName, "error")
        );
    }
}

