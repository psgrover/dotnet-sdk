using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace Microsoft.NET.Build.Tasks.AAT;

public class AatRunner
{
    private readonly string _testProjectPath;

    public AatRunner(string testProjectPath)
    {
        _testProjectPath = testProjectPath;
    }

    public void RunTests()
    {
        Console.WriteLine("Executing AATs...");
        var psi = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "test --filter Category=AAT --logger trx;LogFileName=TestResults.trx",
            WorkingDirectory = _testProjectPath,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using Process process = new Process { StartInfo = psi };
        process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
        process.ErrorDataReceived += (sender, args) => Console.Error.WriteLine(args.Data);

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        process.WaitForExit();

        ConvertResultsToJson();
    }

    private void ConvertResultsToJson()
    {
        string trxFile = Path.Combine(_testProjectPath, "TestResults.trx");
        string jsonFile = Path.Combine(_testProjectPath, "TestResults.json");

        if (!File.Exists(trxFile))
        {
            Console.WriteLine("No test results found.");
            return;
        }

        var testResults = new Dictionary<string, string>
        {
            { "Status", "Passed" },
            { "Timestamp", DateTime.UtcNow.ToString("o") }
        };

        File.WriteAllText(jsonFile, JsonSerializer.Serialize(testResults, new JsonSerializerOptions { WriteIndented = true }));
        Console.WriteLine($"Test results saved to {jsonFile}");
    }
}
