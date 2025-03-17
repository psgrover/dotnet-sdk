using System.IO;
using System.Text.Json;
using Xunit;

namespace Microsoft.NET.Build.Tasks.UnitTests.AAT;

public class ReportValidationTests
{
    private const string ReportPath = "TestResults.json";

    [Fact]
    public void ShouldGenerateValidJsonReport()
    {
        // Ensure the report exists
        Assert.True(File.Exists(ReportPath), "Test results JSON report not found.");

        // Read and parse the JSON report
        string jsonContent = File.ReadAllText(ReportPath);
        var report = JsonSerializer.Deserialize<JsonElement>(jsonContent);

        // Validate expected fields exist
        Assert.True(report.TryGetProperty("Status", out var status), "Status field missing in JSON report.");
        Assert.True(report.TryGetProperty("Timestamp", out var timestamp), "Timestamp field missing in JSON report.");

        // Ensure values are properly set
        Assert.False(string.IsNullOrEmpty(status.GetString()), "Status value is empty.");
        Assert.False(string.IsNullOrEmpty(timestamp.GetString()), "Timestamp value is empty.");
    }
}
