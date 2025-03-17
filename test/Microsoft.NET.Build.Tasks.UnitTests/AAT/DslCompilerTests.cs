using Xunit;
using Microsoft.NET.Build.Tasks.AAT;
using System.IO;

namespace Microsoft.NET.Build.Tasks.UnitTests.AAT;

public class DslCompilerTests
{
    [Fact]
    public void ShouldGenerateValidCSharpFromDsl()
    {
        var feature = new AatFeature { Name = "TestFeature" };
        feature.Scenarios.Add(new AatScenario { Name = "TestScenario", Given = { "data exists" }, When = { "action is taken" }, Then = { "result occurs" } });

        string outputPath = "TestFeature.cs";
        var compiler = new DslCompiler();
        compiler.CompileToCSharp(feature, outputPath);

        Assert.True(File.Exists(outputPath));
        File.Delete(outputPath);
    }
}
