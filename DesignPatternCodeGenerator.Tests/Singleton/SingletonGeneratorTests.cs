﻿using DesignPatternCodeGenerator.Singleton;
using DesignPatternCodeGenerator.Tests.Helpers;
using DesignPatternCodeGenerator.Tests.Singleton.Data;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Singleton;

public class SingletonGeneratorTests
{
    [Theory]
    [MemberData(nameof(SingletonCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(SingletonCompilationSources))]
    public void SingletonGenerator_ForSource_ReturnEmptyDiagnostics(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SingletonGenerator());
        driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out var diagnostics);

        diagnostics.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(SingletonCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(SingletonCompilationSources))]
    public void SingletonGenerator_ForSource_ReturnOutputCompilationWithFourSyntaxTrees(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SingletonGenerator());
        driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

        outputCompilation.SyntaxTrees.Should().HaveCount(2);
    }

    [Theory]
    [MemberData(nameof(SingletonCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(SingletonCompilationSources))]
    public void SingletonGenerator_ForSource_ReturnDriverResultWithEmptyDiagnostics(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SingletonGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        runResult.Diagnostics.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(SingletonCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(SingletonCompilationSources))]
    public void SingletonGenerator_ForSource_ReturnDriverResultWithCorrectGeneratedTreesLenght(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SingletonGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        runResult.GeneratedTrees.Length.Should().Be(1);
    }

    [Theory]
    [MemberData(nameof(SingletonCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(SingletonCompilationSources))]
    public void SingletonGenerator_ForSource_ReturnResultWithFactoryGeneratory(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        var generator = new SingletonGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Generator.Should().Be(generator);
    }

    [Theory]
    [MemberData(nameof(SingletonCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(SingletonCompilationSources))]
    public void SingletonGenerator_ForSource_ReturnResultWithEmptyDiagnostics(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SingletonGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Diagnostics.Should().BeEmpty();
    }

    [Theory]
    [MemberData(nameof(SingletonCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(SingletonCompilationSources))]
    public void SingletonGenerator_ForSource_ReturnResultWithGeneratedSourcesWithCorrectLenght(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SingletonGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.GeneratedSources.Length.Should().Be(1);
    }

    [Theory]
    [MemberData(nameof(SingletonCompilationSources.GetSampleDataToGeneratorTests), MemberType = typeof(SingletonCompilationSources))]
    public void SingletonGenerator_ForSource_NotReturnExceptions(string inputSource)
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new SingletonGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Exception.Should().BeNull();
    }


}
