using DesignPatternCodeGenerator.AbstractFactory;
using DesignPatternCodeGenerator.Tests.AbstractFactory.Data;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.AbstractFactory;

public class AbstractFactoryGeneratorTests
{
    [Theory]
    [MemberData(nameof(AbstractFactoryCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(AbstractFactoryCompilationSources))]
    public void AbstractFactoryGenerator_ForSource_ReturnEmptyDiagnostics(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new AbstractFactoryGenerator());
        driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out var diagnostics);

        diagnostics.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(AbstractFactoryCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(AbstractFactoryCompilationSources))]
    public void AbstractFactoryGenerator_ForSource_ReturnOutputCompilationWithFourSyntaxTrees(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new AbstractFactoryGenerator());
        driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

        outputCompilation.SyntaxTrees.Should().HaveCount(4);
    }

    [Theory]
    [MemberData(nameof(AbstractFactoryCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(AbstractFactoryCompilationSources))]
    public void AbstractFactoryGenerator_ForSource_ReturnDriverResultWithEmptyDiagnostics(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new AbstractFactoryGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        runResult.Diagnostics.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(AbstractFactoryCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(AbstractFactoryCompilationSources))]
    public void AbstractFactoryGenerator_ForSource_ReturnDriverResultWithCorrectGeneratedTreesLenght(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new AbstractFactoryGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        runResult.GeneratedTrees.Length.Should().Be(3);
    }

    [Theory]
    [MemberData(nameof(AbstractFactoryCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(AbstractFactoryCompilationSources))]
    public void FAbstractFactoryGenerator_ForSource_ReturnResultWithFactoryGeneratory(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        var generator = new AbstractFactoryGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Generator.Should().Be(generator);
    }

    [Theory]
    [MemberData(nameof(AbstractFactoryCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(AbstractFactoryCompilationSources))]
    public void AbstractFactoryGenerator_ForSource_ReturnResultWithEmptyDiagnostics(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new AbstractFactoryGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Diagnostics.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(AbstractFactoryCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(AbstractFactoryCompilationSources))]
    public void AbstractFactoryGenerator_ForSource_ReturnResultWithGeneratedSourcesWithCorrectLenght(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new AbstractFactoryGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.GeneratedSources.Length.Should().Be(3);
    }

    [Theory]
    [MemberData(nameof(AbstractFactoryCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(AbstractFactoryCompilationSources))]
    public void AbstractFactoryGenerator_ForSource_NotReturnExceptions(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new AbstractFactoryGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Exception.Should().BeNull();
    }


}
