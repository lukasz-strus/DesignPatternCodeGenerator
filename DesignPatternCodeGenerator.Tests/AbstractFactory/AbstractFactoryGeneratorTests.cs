using DesignPatternCodeGenerator.AbstractFactory;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.AbstractFactory;

public class AbstractFactoryGeneratorTests
{
    [Fact]
    public void AbstractFactoryGenerator_ForSource_ReturnEmptyDiagnostics()
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(ABSTRACT_FACTORY_SOURCE);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new AbstractFactoryGenerator());
        driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out var diagnostics);

        diagnostics.Should().BeEmpty();
    }

    [Fact]
    public void AbstractFactoryGenerator_ForSource_ReturnOutputCompilationWithFourSyntaxTrees()
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(ABSTRACT_FACTORY_SOURCE);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new AbstractFactoryGenerator());
        driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out _);

        outputCompilation.SyntaxTrees.Should().HaveCount(4);
    }

    [Fact]
    public void AbstractFactoryGenerator_ForSource_ReturnDriverResultWithEmptyDiagnostics()
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(ABSTRACT_FACTORY_SOURCE);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new AbstractFactoryGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        runResult.Diagnostics.Should().BeEmpty();
    }

    [Fact]
    public void AbstractFactoryGenerator_ForSource_ReturnDriverResultWithCorrectGeneratedTreesLenght()
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(ABSTRACT_FACTORY_SOURCE);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new AbstractFactoryGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();

        runResult.GeneratedTrees.Length.Should().Be(3);
    }

    [Fact]
    public void FAbstractFactoryGenerator_ForSource_ReturnResultWithFactoryGeneratory()
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(ABSTRACT_FACTORY_SOURCE);
        var generator = new AbstractFactoryGenerator();
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Generator.Should().Be(generator);
    }

    [Fact]
    public void AbstractFactoryGenerator_ForSource_ReturnResultWithEmptyDiagnostics()
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(ABSTRACT_FACTORY_SOURCE);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new AbstractFactoryGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Diagnostics.Should().BeEmpty();
    }

    [Fact]
    public void AbstractFactoryGenerator_ForSource_ReturnResultWithGeneratedSourcesWithCorrectLenght()
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(ABSTRACT_FACTORY_SOURCE);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new AbstractFactoryGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.GeneratedSources.Length.Should().Be(3);
    }

    [Fact]
    public void AbstractFactoryGenerator_ForSource_NotReturnExceptions()
    {
        Compilation inputCompilation = GeneratorTestsHelper.CreateCompilation(ABSTRACT_FACTORY_SOURCE);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(new AbstractFactoryGenerator());
        driver = driver.RunGeneratorsAndUpdateCompilation(inputCompilation, out _, out _);
        GeneratorDriverRunResult runResult = driver.GetRunResult();
        GeneratorRunResult generatorResult = runResult.Results[0];

        generatorResult.Exception.Should().BeNull();
    }

    private const string ABSTRACT_FACTORY_SOURCE = @"using DesignPatternCodeGenerator.Attributes.AbstractFactory;

namespace Samples.AbstractFactory
{
    [AbstractFactory(""Gears"")]
    public interface IMonitor
    {
        void On();
        void Off();
    }

    [AbstractFactoryClass(""Samsung"")]
    public class SamsungMonitor : IMonitor
    {
        public void On()
        {
            Console.WriteLine(""Samsung on"");
        }

        public void Off()
        {
            Console.WriteLine(""Samsung off"");
        }
    }

    [AbstractFactoryClass(""Benq"")]
    public class BenqMonitor : IMonitor
    {
        public void On()
        {
            Console.WriteLine(""Benq on"");
        }

        public void Off()
        {
            Console.WriteLine(""Benq off"");
        }
    }
}";
}
