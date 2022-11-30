using DesignPatternCodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Xunit;
using FluentAssertions;
using DesignPatternCodeGenerator.Prototype;
using DesignPatternCodeGenerator.Tests.Prototype.Data;

namespace DesignPatternCodeGenerator.Tests.Prototype;

public class PrototypeGeneratorTests
{
    [Theory]
    [MemberData(nameof(PrototypeCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(PrototypeCompilationSources))]
    public void PrototypeGenerator_ForSource_ReturnEmptyDiagnostics(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new PrototypeGenerator());
        driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out var diagnostics);

        diagnostics.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(PrototypeCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(PrototypeCompilationSources))]
    public void PrototypeGenerator_ForSource_ReturnOutputCompilationWithFourSyntaxTrees(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new PrototypeGenerator());
        driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

        outputCompilation.SyntaxTrees.Should().HaveCount(2);
    }

    [Theory]
    [MemberData(nameof(PrototypeCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(PrototypeCompilationSources))]
    public void PrototypeGenerator_ForSource_ReturnDriverResultWithEmptyDiagnostics(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new PrototypeGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        runResult.Diagnostics.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(PrototypeCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(PrototypeCompilationSources))]
    public void PrototypeGenerator_ForSource_ReturnDriverResultWithCorrectGeneratedTreesLenght(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new PrototypeGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        runResult.GeneratedTrees.Length.Should().Be(1);
    }

    [Theory]
    [MemberData(nameof(PrototypeCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(PrototypeCompilationSources))]
    public void PrototypeGenerator_ForSource_ReturnResultWithFactoryGeneratory(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        var generator = new PrototypeGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Generator.Should().Be(generator);
    }

    [Theory]
    [MemberData(nameof(PrototypeCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(PrototypeCompilationSources))]
    public void PrototypeGenerator_ForSource_ReturnResultWithEmptyDiagnostics(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new PrototypeGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Diagnostics.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(PrototypeCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(PrototypeCompilationSources))]
    public void PrototypeGenerator_ForSource_ReturnResultWithGeneratedSourcesWithCorrectLenght(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new PrototypeGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.GeneratedSources.Length.Should().Be(1);
    }

    [Theory]
    [MemberData(nameof(PrototypeCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(PrototypeCompilationSources))]
    public void PrototypeGenerator_ForSource_NotReturnExceptions(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new PrototypeGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Exception.Should().BeNull();
    }
}
