using CLS.Common.ExecutionControl;

namespace CLS.Common.CommandControl;

public static class CommandTaskExtension
{
    public static async Task<CommandExecutionResult> ExecuteAsync(this CommandTask task)
    {
        // Prepare Deployment Directory for the program
        string deploymentDirectory = SampleProgram.DeploymentDirectory;

        // Deploy a simple command line app to the deployment directory
        string exeFileName = SampleProgram.Deploy(deploymentDirectory);

        CommandExecutionResult result =
            await CommandLineWrapper.ExecuteCommand(exeFileName, task.Arguments);

        return result;
    }
}
