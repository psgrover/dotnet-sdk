using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Microsoft.NET.Build.Tasks.AAT;

public class DslParser
{
    private static readonly Regex FeatureRegex = new(@"Feature:\s*(.+)", RegexOptions.Compiled);
    private static readonly Regex ScenarioRegex = new(@"Scenario:\s*(.+)", RegexOptions.Compiled);
    private static readonly Regex GivenRegex = new(@"Given\s*(.+)", RegexOptions.Compiled);
    private static readonly Regex WhenRegex = new(@"When\s*(.+)", RegexOptions.Compiled);
    private static readonly Regex ThenRegex = new(@"Then\s*(.+)", RegexOptions.Compiled);

    public AatFeature Parse(string dslContent)
    {
        var lines = dslContent.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        AatFeature feature = new();
        AatScenario currentScenario = null;

        foreach (var line in lines)
        {
            if (FeatureRegex.IsMatch(line))
            {
                feature.Name = FeatureRegex.Match(line).Groups[1].Value.Trim();
            }
            else if (ScenarioRegex.IsMatch(line))
            {
                currentScenario = new AatScenario { Name = ScenarioRegex.Match(line).Groups[1].Value.Trim() };
                feature.Scenarios.Add(currentScenario);
            }
            else if (currentScenario != null)
            {
                if (GivenRegex.IsMatch(line))
                    currentScenario.Given.Add(GivenRegex.Match(line).Groups[1].Value.Trim());
                else if (WhenRegex.IsMatch(line))
                    currentScenario.When.Add(WhenRegex.Match(line).Groups[1].Value.Trim());
                else if (ThenRegex.IsMatch(line))
                    currentScenario.Then.Add(ThenRegex.Match(line).Groups[1].Value.Trim());
            }
        }
        return feature;
    }
}

public class AatFeature
{
    public string Name { get; set; }
    public List<AatScenario> Scenarios { get; set; } = new();
}

public class AatScenario
{
    public string Name { get; set; }
    public List<string> Given { get; set; } = new();
    public List<string> When { get; set; } = new();
    public List<string> Then { get; set; } = new();
}
