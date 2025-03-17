using Xunit;
using Microsoft.NET.Build.Tasks.AAT;
using System.IO;

namespace Microsoft.NET.Build.Tasks.UnitTests.AAT;

public class AatCliHandlerTests
{
    [Fact]
    public void ShouldHandleGenerateCommandSuccessfully()
    {
        string inputPath = "SampleTest.dsl";
        string outputPath = "GeneratedTest.cs";
        File.WriteAllText(inputPath, "Feature: CLI Test\nScenario: Validate CLI\n    Given input is provided\n    When CLI runs\n    Then output is generated");

        var handler = new AatCliHandler();
        handler.HandleGenerateCommand(inputPath, outputPath);

        Assert.True(File.Exists(outputPath));
        File.Delete(inputPath);
        File.Delete(outputPath);
    }
}
