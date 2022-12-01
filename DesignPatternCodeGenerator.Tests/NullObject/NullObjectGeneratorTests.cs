using DesignPatternCodeGenerator.NullObject;
using DesignPatternCodeGenerator.Tests.Helpers;
using DesignPatternCodeGenerator.Tests.NullObject.Data;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.NullObject;

public class NullObjectGeneratorTests
{
    [Theory]
    [MemberData(nameof(NullObjectCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(NullObjectCompilationSources))]
    public void NullObjectGenerator_ForSource_ReturnEmptyDiagnostics(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new NullObjectGenerator());
        driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out var diagnostics);

        diagnostics.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(NullObjectCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(NullObjectCompilationSources))]
    public void NullObjectGenerator_ForSource_ReturnOutputCompilationWithFourSyntaxTrees(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new NullObjectGenerator());
        driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

        outputCompilation.SyntaxTrees.Should().HaveCount(2);
    }

    [Theory]
    [MemberData(nameof(NullObjectCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(NullObjectCompilationSources))]
    public void NullObjectGenerator_ForSource_ReturnDriverResultWithEmptyDiagnostics(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new NullObjectGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        runResult.Diagnostics.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(NullObjectCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(NullObjectCompilationSources))]
    public void NullObjectGenerator_ForSource_ReturnDriverResultWithCorrectGeneratedTreesLenght(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new NullObjectGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        runResult.GeneratedTrees.Length.Should().Be(1);
    }

    [Theory]
    [MemberData(nameof(NullObjectCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(NullObjectCompilationSources))]
    public void NullObjectGenerator_ForSource_ReturnResultWithFactoryGeneratory(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        var generator = new NullObjectGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Generator.Should().Be(generator);
    }

    [Theory]
    [MemberData(nameof(NullObjectCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(NullObjectCompilationSources))]
    public void NullObjectGenerator_ForSource_ReturnResultWithEmptyDiagnostics(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new NullObjectGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Diagnostics.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(NullObjectCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(NullObjectCompilationSources))]
    public void NullObjectGenerator_ForSource_ReturnResultWithGeneratedSourcesWithCorrectLenght(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new NullObjectGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.GeneratedSources.Length.Should().Be(1);
    }

    [Theory]
    [MemberData(nameof(NullObjectCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(NullObjectCompilationSources))]
    public void NullObjectGenerator_ForSource_NotReturnExceptions(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new NullObjectGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Exception.Should().BeNull();
    }
}
