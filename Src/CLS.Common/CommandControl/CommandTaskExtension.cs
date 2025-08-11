using CLS.Common.ExecutionControl;

namespace CLS.Common.CommandControl;

public static class CommandTaskExtension
{
    /// <summary>
    /// Executes the command task asynchronously.
    /// </summary>
    /// <remarks>
    /// TODO: Add CancellationToken cancellationToken
    /// </remarks>
    public static async Task<CommandExecutionResult> ExecuteAsync(this CommandTask task)
    {
        if (task.IsStub)
        {
            // Execute test stub
            return await task.ExecuteAsStubAsync();
        }

        else
        {
            // TODO: Execute the command using the CommandLineWrapper
            return CommandExecutionResult.Failed;
        }
    }

    public static async Task<CommandExecutionResult> ExecuteAsStubAsync(this CommandTask task)
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
