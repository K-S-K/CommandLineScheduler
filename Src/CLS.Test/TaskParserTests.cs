using System.Text;
using CLS.Common.CommandControl;
using CLS.Common.ScriptProcessing;

namespace CLS.Test;

public class TaskParserTests
{
    [Fact]
    public void YTDLParserTest()
    {
        // Arrange
        StringBuilder sb = new();
        sb.AppendLine("My downlosd list");
        sb.AppendLine("https://www.youtube.com/watch?v=6Dh-RL__uN4");
        sb.AppendLine("https://www.youtube.com/watch?v=6Dh-RL__uN5");
        sb.AppendLine("");
        sb.AppendLine("https://www.youtube.com/watch?v=6Dh-RL__uN6");
        string inputText = sb.ToString();

        YTDLParser parser = new(inputText);

        // Act
        List<string> urls = parser.GetVideoUrls();

        // Assert
        Assert.Equal(3, urls.Count);
        Assert.Equal("https://www.youtube.com/watch?v=6Dh-RL__uN4", urls[0]);
        Assert.Equal("https://www.youtube.com/watch?v=6Dh-RL__uN5", urls[1]);
        Assert.Equal("https://www.youtube.com/watch?v=6Dh-RL__uN6", urls[2]);

        // Act
        List<CommandTask> tasks = parser.GetDownloadTasks("C:\\Temp");

        // Assert
        Assert.Equal(3, tasks.Count);
        Assert.Equal("C:\\Temp", tasks[0].Directory);
        Assert.Equal("C:\\Temp", tasks[1].Directory);
        Assert.Equal("C:\\Temp", tasks[2].Directory);
        Assert.Equal("https://www.youtube.com/watch?v=6Dh-RL__uN4", tasks[0].Arguments);
        Assert.Equal("https://www.youtube.com/watch?v=6Dh-RL__uN5", tasks[1].Arguments);
        Assert.Equal("https://www.youtube.com/watch?v=6Dh-RL__uN6", tasks[2].Arguments);
    }
}