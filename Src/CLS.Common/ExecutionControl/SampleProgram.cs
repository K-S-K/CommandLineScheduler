using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace CLS.Common.ExecutionControl;

public class SampleProgram
{
    #region Code
    /// <summary>
    /// This is the code that will be used to test the Command Line Task Controller.
    /// This sample program must perform the following tasks:
    /// 1. Accept a string as a command line argument.
    /// 2. Print to console a small greeting.
    /// 3. Wait for 0,1 second.
    /// 4. Print to console a task progress imitation messages from 0% to 100% in 20% increments with 20 millisecond delay between each message.
    /// 5. Print to console an input string.
    /// 6. Print to console a small goodbye message.
    /// 7. If the input string is "exit", then exit the program.
    /// 8. If the input string is "error", then throw an exception instead of printing the input string.
    /// </summary>
    public static string SimpleCommandLineAppCode
    {
        get
        {
            StringBuilder sbCode = new();

            sbCode.AppendLine("using System;");
            sbCode.AppendLine("using System.Threading;");
            sbCode.AppendLine("using System.Threading.Tasks;");
            sbCode.AppendLine("using System.Text;");
            sbCode.AppendLine();
            sbCode.AppendLine("public class Program");
            sbCode.AppendLine("{");
            sbCode.AppendLine("    public static async Task Main(string[] args)");
            sbCode.AppendLine("    {");
            sbCode.AppendLine("        Console.WriteLine(\"Hello, World!\");");
            sbCode.AppendLine("        await Task.Delay(100);");
            sbCode.AppendLine();
            sbCode.AppendLine("        for (int i = 0; i <= 100; i += 20)");
            sbCode.AppendLine("        {");
            sbCode.AppendLine("            Console.WriteLine($\"Task progress: {i}%\");");
            sbCode.AppendLine("            await Task.Delay(500);");
            sbCode.AppendLine("        }");
            sbCode.AppendLine();
            sbCode.AppendLine("        if(args.Length > 0)");
            sbCode.AppendLine("        {");
            sbCode.AppendLine("            Console.WriteLine(args[0]);");
            sbCode.AppendLine();
            sbCode.AppendLine("            if (args[0] == \"exit\")");
            sbCode.AppendLine("            {");
            sbCode.AppendLine("                return;");
            sbCode.AppendLine("            }");
            sbCode.AppendLine();
            sbCode.AppendLine("            if (args[0] == \"error\")");
            sbCode.AppendLine("            {");
            sbCode.AppendLine("                throw new Exception(\"An intentional error occurred!\");");
            sbCode.AppendLine("            }");
            sbCode.AppendLine("        }");
            sbCode.AppendLine();
            sbCode.AppendLine("        Console.WriteLine(\"Goodbye!\");");
            sbCode.AppendLine("    }");
            sbCode.AppendLine("}");

            return sbCode.ToString();
        }
    }

    /// <summary>
    /// This is the project file 
    /// that will be used to test 
    /// the Command Line Task Controller.
    /// </summary>
    /// <remarks>
    /// Prepare project file to compile C# 12 code 
    /// from the file for linux executable
    /// </remarks>
    public static string SimpleCommandLineAppProject
    {
        get
        {
            StringBuilder sbCode = new();

            sbCode.AppendLine("<Project Sdk=\"Microsoft.NET.Sdk\">");
            sbCode.AppendLine("    <PropertyGroup>");
            sbCode.AppendLine("        <OutputType>Exe</OutputType>");
            sbCode.AppendLine("        <TargetFramework>net8.0</TargetFramework>");
            sbCode.AppendLine("        <LangVersion>12</LangVersion>");
            sbCode.AppendLine("    </PropertyGroup>");
            sbCode.AppendLine("</Project>");

            return sbCode.ToString();
        }
    }
    #endregion


    /// <summary>
    /// Get the current execution directory
    /// </summary>
    public static string DeploymentDirectory
    {
        get
        {
            string currentDirectory =
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                ?? throw new Exception("Failed to get the current directory");

            string deploymentDirectory = Path.Combine(currentDirectory, "TestConsoleProgram");

            // Create the directory if it does not exist
            if (!Directory.Exists(deploymentDirectory))
            {
                Directory.CreateDirectory(deploymentDirectory);
            }

            return deploymentDirectory;
        }
    }

    /// <summary>
    /// Deploy the code to a directory and compile it
    /// </summary>
    /// <param name="deploymentDirectory">The directory to deploy the code</param>
    /// <returns>The path to the compiled executable</returns>
    public static string Deploy(string deploymentDirectory)
    {
        // Create the directory if it does not exist
        if (!Directory.Exists(deploymentDirectory))
        {
            Directory.CreateDirectory(deploymentDirectory);
        }

        // Prepare the file names
        string projectFile = Path.Combine(deploymentDirectory, "Program.csproj");
        string csFileName = Path.Combine(deploymentDirectory, "Program.cs");
        string exeFileName = Path.Combine(deploymentDirectory, "Program");

        // Write the code to a file
        File.WriteAllText(csFileName, SimpleCommandLineAppCode);
        File.WriteAllText(projectFile, SimpleCommandLineAppProject);

        // Prepare command to compile the C# code
        // Set permissions to execute the compiled program
        // The program must be compiled and executed in macos or linux
        ProcessStartInfo startInfo = new()
        {
            FileName = "dotnet",
            Arguments = $"publish -c Release -r osx-arm64 -o {deploymentDirectory} {projectFile}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        // Execute the compilation command
        Process compiler = Process.Start(startInfo)
            ?? throw new Exception("Failed to start the compilation process");

        // Read the output of the compilation process
        string compilerOutput = compiler.StandardOutput.ReadToEnd();
        string compilerError = compiler.StandardError.ReadToEnd();

        // Check if the compilation process has any errors
        if (!string.IsNullOrEmpty(compilerError))
        {
            throw new Exception(compilerError);
        }

        return exeFileName;
    }
}
