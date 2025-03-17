using System;
using System.IO;

namespace Microsoft.NET.Build.Tasks.AAT;

public class AatCliHandler
{
    private readonly DslParser _parser;
    private readonly DslCompiler _compiler;

    public AatCliHandler()
    {
        _parser = new DslParser();
        _compiler = new DslCompiler();
    }

    public void HandleGenerateCommand(string inputPath, string outputPath)
    {
        if (!File.Exists(inputPath))
        {
            Console.WriteLine("Error: DSL file not found.");
            return;
        }

        string dslContent = File.ReadAllText(inputPath);
        var feature = _parser.Parse(dslContent);

        _compiler.CompileToCSharp(feature, outputPath);
        Console.WriteLine($"Generated C# test file at {outputPath}");
    }
}
