using CLS.Common.CommandControl;

namespace CLS.Common.ScriptProcessing;

/// <summary>
/// Parses the text of the playlist to extract the URLs of the videos.
/// </summary>
public class YTDLParser
{
    #region -> Fields
    private string cmdName = "DownloadFromYoutube";
    private string cmdWord = "yt-dlp";
    #endregion


    #region -> Properties
    public string Input { get; init; } = string.Empty;
    #endregion

    #region -> Constructors
    public YTDLParser(string output)
    {
        ICommandTypeLibrary commandLibrary = CommandTypeLibrary.Default;

        Input = output;
    }
    #endregion

    #region -> Methods
    public List<string> GetVideoUrls()
    {
        List<string> urls = [];
        // Split the output into not empty lines.
        string[] lines = Input.Split(
            ["\r\n", "\r", "\n"],
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        foreach (string line in lines.Where(l => l.Contains("http")))
        {
            urls.Add(line);
        }
        return urls;
    }

    public List<CommandTask> GetDownloadTasks(string directory)
    {
        List<CommandTask> tasks = [];
        List<string> urls = GetVideoUrls();
        foreach (string url in urls)
        {
            CommandTask task = new(cmdName, cmdWord, directory, url);
            tasks.Add(task);
        }
        return tasks;
    }
    #endregion
}
