using DesignPatternCodeGenerator.Tests.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Xunit;
using FluentAssertions;
using DesignPatternCodeGenerator.Prototype;

namespace DesignPatternCodeGenerator.Tests.Prototype;

public class PrototypeGeneratorTests
{
    [Fact]
    public void FactoryGenerator_ForSource_ReturnEmptyDiagnostics()
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(PROTOTYPE_COMPILATION_SOURCE);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new PrototypeGenerator());
        driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out var diagnostics);

        diagnostics.Should().BeEmpty();
    }

    [Fact]
    public void FactoryGenerator_ForSource_ReturnOutputCompilationWithFourSyntaxTrees()
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(PROTOTYPE_COMPILATION_SOURCE);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new PrototypeGenerator());
        driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

        outputCompilation.SyntaxTrees.Should().HaveCount(2);
    }

    [Fact]
    public void FactoryGenerator_ForSource_ReturnDriverResultWithEmptyDiagnostics()
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(PROTOTYPE_COMPILATION_SOURCE);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new PrototypeGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        runResult.Diagnostics.Should().BeEmpty();
    }

    [Fact]
    public void FactoryGenerator_ForSource_ReturnDriverResultWithCorrectGeneratedTreesLenght()
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(PROTOTYPE_COMPILATION_SOURCE);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new PrototypeGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        runResult.GeneratedTrees.Length.Should().Be(1);
    }

    [Fact]
    public void FactoryGenerator_ForSource_ReturnResultWithFactoryGeneratory()
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(PROTOTYPE_COMPILATION_SOURCE);
        var generator = new PrototypeGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Generator.Should().Be(generator);
    }

    [Fact]
    public void FactoryGenerator_ForSource_ReturnResultWithEmptyDiagnostics()
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(PROTOTYPE_COMPILATION_SOURCE);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new PrototypeGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Diagnostics.Should().BeEmpty();
    }

    [Fact]
    public void FactoryGenerator_ForSource_ReturnResultWithGeneratedSourcesWithCorrectLenght()
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(PROTOTYPE_COMPILATION_SOURCE);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new PrototypeGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.GeneratedSources.Length.Should().Be(1);
    }

    [Fact]
    public void FactoryGenerator_ForSource_NotReturnExceptions()
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(PROTOTYPE_COMPILATION_SOURCE);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new PrototypeGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Exception.Should().BeNull();
    }

    private const string PROTOTYPE_COMPILATION_SOURCE =
@"using DesignPatternCodeGenerator.Attributes.Prototype;

namespace Test.Test
{
    [Prototype]
    public partial class Person 
    { 
        public string Name {get; set;}
        
        public Address Address {get; set;}
    }

    public class Address
    {
        public string City { get; set; }
    }
}";
}
