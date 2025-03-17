using Xunit;
using Microsoft.NET.Build.Tasks.AAT;
using System.IO;

namespace Microsoft.NET.Build.Tasks.UnitTests.AAT;

public class DslParserTests
{
    [Fact]
    public void ShouldParseGivenWhenThenStatements()
    {
        string dsl = "Feature: Sample Feature\nScenario: Sample Scenario\n    Given user is logged in\n    When user submits form\n    Then confirmation is displayed";
        var parser = new DslParser();
        var feature = parser.Parse(dsl);

        Assert.Single(feature.Scenarios);
        Assert.Contains("user is logged in", feature.Scenarios[0].Given);
        Assert.Contains("user submits form", feature.Scenarios[0].When);
        Assert.Contains("confirmation is displayed", feature.Scenarios[0].Then);
    }
}
