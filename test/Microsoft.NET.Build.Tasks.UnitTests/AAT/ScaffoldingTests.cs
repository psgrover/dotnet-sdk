using System.IO;
using Xunit;

namespace Microsoft.NET.Build.Tasks.UnitTests.AAT;

public class ScaffoldingTests
{
    private const string DslFile = "SampleTest.dsl";
    private const string GeneratedTestFile = "GeneratedTest.cs";

    [Fact]
    public void ShouldGenerateCSharpTestFromDsl()
    {
        // Arrange: Create a sample DSL file
        File.WriteAllText(DslFile, "Feature: Sample Feature\nScenario: Validate DSL to C#\n    Given input is provided\n    When CLI runs\n    Then output is generated");

        // Act: Run the CLI command to generate the test
        var cliHandler = new DotNetSdk.AAT.AatCliHandler();
        cliHandler.HandleGenerateCommand(DslFile, GeneratedTestFile);

        // Assert: Check if the generated test file exists
        Assert.True(File.Exists(GeneratedTestFile), "Generated C# test file was not created.");

        // Cleanup
        File.Delete(DslFile);
        File.Delete(GeneratedTestFile);
    }
}
