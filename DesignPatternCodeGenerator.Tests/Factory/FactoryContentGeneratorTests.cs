using DesignPatternCodeGenerator.Factory;
using DesignPatternCodeGenerator.Tests.Factory.Data;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Factory;

public class FactoryContentGeneratorTests
{
    [Theory]
    [MemberData(nameof(FactoryCompilationSources.GetSampleDataToInterfaceTests), MemberType = typeof(FactoryCompilationSources))]
    internal void GenerateInterface_ForValidInputs_ReturnInterface(string inputSource, string interfaceSource)
    {
        var interfaceGroup = GeneratorTestsHelper.GetInterfaceGroup(inputSource);

        var result = FactoryContentGenerator.GenerateInterface(interfaceGroup);

        result.RemoveWhitespace().Should().Be(interfaceSource.RemoveWhitespace());
    }

    [Theory]
    [MemberData(nameof(FactoryCompilationSources.GetSampleDataToClassTests), MemberType = typeof(FactoryCompilationSources))]
    internal void GenerateClass_ForValidInputs_ReturnInterface(string inputSource, string classSource)
    {
        var interfaceGroup = GeneratorTestsHelper.GetInterfaceGroup(inputSource);
        var childGroups = GeneratorTestsHelper.GetClassGroups(inputSource);

        var result = FactoryContentGenerator.GenerateClass(interfaceGroup, childGroups);

        result.RemoveWhitespace().Should().Be(classSource.RemoveWhitespace());
    }
}
