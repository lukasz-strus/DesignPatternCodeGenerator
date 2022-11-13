using DesignPatternCodeGenerator.Attributes.Factory;
using DesignPatternCodeGenerator.Base.Generators;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Base.Generators;

public class DeclarationsSyntaxGeneratorTests
{
    private const string FACTORY_COMPILATION_SOURCE =
@"using DesignPatternCodeGenerator.Attributes.Factory;
using System;

namespace DesignPatternCodeGenerator.Tests.Data
{

    [Factory]
    public interface ITest
    {

    }

    [FactoryChild]
    public class Test : ITest
    {

    }
}";


    [Fact]
    internal void GetInterfaceGroups_ForValidInputs_ReturnsCorrectKeyValue()
    {
        var compilation = CreateCompilation(FACTORY_COMPILATION_SOURCE);
        var source = new CancellationTokenSource();
        var token = source.Token;

        var result = DeclarationsSyntaxGenerator.GetInterfaceGroups(compilation, token, typeof(FactoryAttribute));

        result.Select(x => x.Key).First().Should().Be("ITest");
    }

    [Fact]
    internal void GetClassGroups_ForValidInputs_ReturnsCorrectKeyValue()
    {
        var compilation = CreateCompilation(FACTORY_COMPILATION_SOURCE);
        var source = new CancellationTokenSource();
        var token = source.Token;

        var result = DeclarationsSyntaxGenerator.GetClassGroups(compilation, token, typeof(FactoryChildAttribute));

        result.Select(x => x.Key).First().Should().Be("Test");
    }

    private static Compilation CreateCompilation(string sourceCode)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
        var references = AppDomain.CurrentDomain.GetAssemblies()
                                  .Where(assembly => !assembly.IsDynamic)
                                  .Select(assembly => MetadataReference
                                                      .CreateFromFile(assembly.Location))
                                  .Cast<MetadataReference>();

        var compilation = CSharpCompilation.Create("SourceGeneratorTests",
                      new[] { syntaxTree },
                      references,
                      new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));


        return compilation;
    }

}
