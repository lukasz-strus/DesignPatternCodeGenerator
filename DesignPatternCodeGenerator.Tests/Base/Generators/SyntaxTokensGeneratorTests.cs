using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Base.Models;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Base.Generators;

public class SyntaxTokensGeneratorTests
{
    [Theory]
    [InlineData(GeneratorAttributeType.Factory)]
    [InlineData(GeneratorAttributeType.Builder)]
    internal void GenerateSyntaxTokens_ForValidInputs_ReturnsCorrectClassName(GeneratorAttributeType generatorAttributeType)
    {
        var group = GeneratorTestsHelper.GetInterfaceGroup(TEST_INTERFACE);

        var result = SyntaxTokensGenerator.GenerateSyntaxTokens(group, generatorAttributeType);

        result.ClassName.Should().Be(_syntaxTokens.ClassName + generatorAttributeType.ToString());
    }

    [Theory]
    [InlineData(GeneratorAttributeType.Factory)]
    [InlineData(GeneratorAttributeType.Builder)]
    internal void GenerateSyntaxTokens_ForValidInputs_ReturnsCorrectInterfaceName(GeneratorAttributeType generatorAttributeType)
    {
        var group = GeneratorTestsHelper.GetInterfaceGroup(TEST_INTERFACE);

        var result = SyntaxTokensGenerator.GenerateSyntaxTokens(group, generatorAttributeType);

        result.InterfaceName.Should().Be(_syntaxTokens.InterfaceName + generatorAttributeType.ToString());
    }

    [Theory]
    [InlineData(GeneratorAttributeType.Factory)]
    [InlineData(GeneratorAttributeType.Builder)]
    internal void GenerateSyntaxTokens_ForValidInputs_ReturnsCorrectNamespace(GeneratorAttributeType generatorAttributeType)
    {
        var group = GeneratorTestsHelper.GetInterfaceGroup(TEST_INTERFACE);

        var result = SyntaxTokensGenerator.GenerateSyntaxTokens(group, generatorAttributeType);

        result.Namespace.Should().Be(_syntaxTokens.Namespace);
    }

    [Theory]
    [InlineData(GeneratorAttributeType.Factory)]
    [InlineData(GeneratorAttributeType.Builder)]
    internal void GenerateSyntaxTokens_ForValidInputs_ReturnsCorrectAccessibility(GeneratorAttributeType generatorAttributeType)
    {
        var group = GeneratorTestsHelper.GetInterfaceGroup(TEST_INTERFACE);

        var result = SyntaxTokensGenerator.GenerateSyntaxTokens(group, generatorAttributeType);

        result.Accessibility.Should().Be(_syntaxTokens.Accessibility);
    }

    [Theory]
    [InlineData(GeneratorAttributeType.Factory)]
    [InlineData(GeneratorAttributeType.Builder)]
    internal void GenerateSyntaxTokens_ForValidInputs_ReturnsCorrectUsings(GeneratorAttributeType generatorAttributeType)
    {
        var group = GeneratorTestsHelper.GetInterfaceGroup(TEST_INTERFACE);

        var result = SyntaxTokensGenerator.GenerateSyntaxTokens(group, generatorAttributeType);

        result.Usings.First().Should().Be(_syntaxTokens.Usings.First());
    }

    private readonly SyntaxTokens _syntaxTokens = new()
    {
        ClassName = "Test",
        InterfaceName = "ITest",
        Namespace = "Test.Test",
        Accessibility = "public",
        Usings = new List<string>() { "System" }
    };

    private const string TEST_INTERFACE =
            "using System;\r\n\r\nnamespace Test.Test;\r\n\r\npublic interface ITest\r\n{\r\n\r\n}";
}
