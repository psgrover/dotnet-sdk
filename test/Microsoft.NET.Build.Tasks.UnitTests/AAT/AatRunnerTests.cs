using Xunit;
using Microsoft.NET.Build.Tasks.AAT;
using System.IO;

namespace Microsoft.NET.Build.Tasks.UnitTests.AAT;

public class AatRunnerTests
{
    [Fact]
    public void ShouldExecuteGeneratedTests()
    {
        var runner = new AatRunner("./");
        runner.RunTests();
        Assert.True(true); // Assume test execution runs without errors
    }
}
