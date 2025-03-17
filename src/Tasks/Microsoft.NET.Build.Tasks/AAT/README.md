# Automated Acceptance Testing (AAT) in .NET SDK

## Overview
Automated Acceptance Testing (AAT) is a crucial part of modern software development, ensuring that applications meet business requirements while remaining easy to maintain. This feature introduces **first-class support for AATs in the .NET SDK** using a **Domain-Specific Language (DSL)**, enabling developers to define and execute Given-When-Then (GWT) style tests seamlessly.

With this addition, any **.NET Core-based web API or application** can now define and run acceptance tests using a human-readable DSL format, with full integration into the existing .NET CLI and testing frameworks.

## Key Features
- **DSL-Based Test Definitions**: Write tests in a structured, human-readable format.
- **Automatic Test Generation**: Convert DSL test cases into **executable C# test classes**.
- **Seamless Integration**: Works with existing `.NET test` commands and **xUnit/NUnit** frameworks.
- **CLI Support**: Introduces `dotnet aat generate` for **DSL compilation** and test scaffolding.
- **CI/CD Ready**: Generates structured reports in JSON, HTML, or JUnit formats for test automation.
- **VS Code & Visual Studio Support**: Syntax highlighting and debugging for `.dsl` test files.

## Getting Started

### 1️⃣ Define Your AAT Tests in DSL Format
Create a `.dsl` file with **Given-When-Then** style test cases:

```dsl
Feature: Product API Testing

Scenario: Create a new product
    Given a product with name "Keyboard" and price 50.00
    When the product is created via API
    Then the product should be available with a generated ID
```

### 2️⃣ Generate C# Test Code
Use the new **dotnet CLI command** to compile your `.dsl` file into executable C# tests:

```sh
dotnet aat generate --input tests/ProductTests.dsl --output tests/ProductTests.cs
```

### 3️⃣ Execute the Generated AATs
Run the tests using the familiar `.NET test` workflow:

```sh
dotnet test --filter Category=AAT
```

## File Structure
```
/repo/dotnet-sdk/
│── src/
│   ├── Tasks/Microsoft.NET.Build.Tasks/AAT/
│   │   ├── DslParser.cs  # Parses DSL test definitions
│   │   ├── DslCompiler.cs  # Converts DSL to C# test classes
│   │   ├── AatRunner.cs  # Executes AATs and generates reports
│   │   ├── AatCliHandler.cs  # Integrates with dotnet CLI
│   │   ├── README.md  # Documentation for AAT feature
│   │   ├── usage-guide.md  # Example app usage and instructions
│── test/
│   ├── Microsoft.NET.Build.Tasks.UnitTests/AAT/
│   │   ├── DslParserTests.cs  # Unit tests for DSL parser
│   │   ├── DslCompilerTests.cs  # Ensures correct C# test generation
│   │   ├── AatRunnerTests.cs  # Validates test execution and reporting
│   │   ├── AatCliHandlerTests.cs  # Verifies CLI commands
│── .editorconfig  # DSL syntax rules for better editor support
```

## Why It Matters
- **Brings Business & Engineering Together**: Enables clear, human-readable test cases that reflect real business rules.
- **Reduces Manual Testing Overhead**: Automates high-level validation, ensuring fewer bugs slip into production.
- **Works Seamlessly with CI/CD**: Ensures only well-tested code moves forward in your deployment pipeline.

## Additional Resources
📌 **Example App Usage Guide:** [src/Tasks/Microsoft.NET.Build.Tasks/AAT/usage-guide.md](./usage-guide.md)

## Future Roadmap
- **Expand DSL Syntax**: Support more complex scenarios like data-driven testing.
- **Parallel Execution**: Speed up test execution for large projects.
- **Deeper CI/CD Integration**: Add structured reporting for GitHub Actions, Azure DevOps, and other platforms.

---
Now you can write **clear, business-readable acceptance tests** directly inside the .NET SDK workflow! 🚀
