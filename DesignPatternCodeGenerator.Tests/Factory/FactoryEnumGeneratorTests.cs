using DesignPatternCodeGenerator.Factory;
using DesignPatternCodeGenerator.Tests.Factory.Data;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Factory;

public class FactoryEnumGeneratorTests
{
    [Theory]
    [MemberData(nameof(FactoryCompilationSources.GetSampleDataToEnumTests), MemberType = typeof(FactoryCompilationSources))]
    internal void GenerateEnum_ForValidInput_ReturnCorrectEnum(string source, string expected)
    {
        var interfaceGroup = GeneratorTestsHelper.GetInterfaceGroup(source);
        var factoryProductsGroups = GeneratorTestsHelper.GetClassGroups(source);

        var result = FactoryEnumGenerator.GenerateEnum(interfaceGroup, factoryProductsGroups);

        result.RemoveWhitespace().Should().Be(expected.RemoveWhitespace());
    }
}
