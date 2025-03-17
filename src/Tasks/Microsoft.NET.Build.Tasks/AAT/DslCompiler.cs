using System;
using System.Text;
using System.IO;

namespace Microsoft.NET.Build.Tasks.AAT;

public class DslCompiler
{
    public void CompileToCSharp(AatFeature feature, string outputPath)
    {
        StringBuilder sb = new();
        sb.AppendLine("using Xunit;");
        sb.AppendLine("using System.Threading.Tasks;");
        sb.AppendLine("namespace GeneratedAATTests");
        sb.AppendLine("{");

        foreach (var scenario in feature.Scenarios)
        {
            sb.AppendLine($"    public class {feature.Name.Replace(" ", "")}_{scenario.Name.Replace(" ", "")}");
            sb.AppendLine("    {");
            sb.AppendLine("        [Fact]");
            sb.AppendLine("        public async Task TestScenario()");
            sb.AppendLine("        {");

            foreach (var given in scenario.Given)
            {
                sb.AppendLine($"            // Given {given}");
            }
            foreach (var when in scenario.When)
            {
                sb.AppendLine($"            // When {when}");
            }
            foreach (var then in scenario.Then)
            {
                sb.AppendLine($"            // Then {then}");
                sb.AppendLine("            Assert.True(true); // Replace with actual validation");
            }

            sb.AppendLine("        }");
            sb.AppendLine("    }");
        }
        sb.AppendLine("}");

        File.WriteAllText(outputPath, sb.ToString());
    }
}
