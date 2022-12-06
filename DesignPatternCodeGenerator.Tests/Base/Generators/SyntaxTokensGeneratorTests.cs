using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Base.Generators;

public class SyntaxTokensGeneratorTests
{
    [Theory]
    [InlineData(TEST_INTERFACE, GeneratorAttributeType.Factory)]
    [InlineData(TEST_INTERFACE, GeneratorAttributeType.Builder)]
    internal void GenerateSyntaxTokens_ForValidInputs_ReturnsCorrectClassName(
        string inputSource, 
        GeneratorAttributeType generatorAttributeType)
    {
        var group = GeneratorTestsHelper.GetInterfaceGroup(inputSource);

        var result = BaseNamesGenerator.GetClassName(group, generatorAttributeType, true, true);

        result.Should().Be("Test" + generatorAttributeType.ToString());
    }

    [Theory]
    [InlineData(TEST_INTERFACE, GeneratorAttributeType.Factory)]
    [InlineData(TEST_INTERFACE, GeneratorAttributeType.Builder)]
    internal void GenerateSyntaxTokens_ForValidInputs_ReturnsCorrectInterfaceName(
        string inputSource, 
        GeneratorAttributeType generatorAttributeType)
    {
        var group = GeneratorTestsHelper.GetInterfaceGroup(inputSource);

        var result = BaseNamesGenerator.GetInterfaceName(group, generatorAttributeType);

        result.Should().Be("ITest" + generatorAttributeType.ToString());
    }

    [Theory]
    [InlineData(TEST_INTERFACE)]
    internal void GenerateSyntaxTokens_ForValidInputs_ReturnsCorrectNamespace(string inputSource)
    {
        var group = GeneratorTestsHelper.GetInterfaceGroup(inputSource);

        var result = BaseNamesGenerator.GetNamespace(group);

        result.Should().Be("Test.Test");
    }

    [Theory]
    [InlineData(TEST_INTERFACE)]
    internal void GenerateSyntaxTokens_ForValidInputs_ReturnsCorrectAccessibility(string inputSource)
    {
        var group = GeneratorTestsHelper.GetInterfaceGroup(inputSource);

        var result = BaseNamesGenerator.GetAccesibility(group);

        result.Should().Be("public");
    }

    [Theory]
    [InlineData(TEST_INTERFACE)]
    internal void GenerateSyntaxTokens_ForValidInputs_ReturnsCorrectUsings(string inputSource)
    {
        var group = GeneratorTestsHelper.GetInterfaceGroup(inputSource);
        var expected = new List<string>() { "System" };

        var result = BaseNamesGenerator.GetUsings(group);

        result.First().Should().Be(expected.First());
    }

    private const string TEST_INTERFACE =
            "using System;\r\n\r\nnamespace Test.Test;\r\n\r\npublic interface ITest\r\n{\r\n\r\n}";
}
