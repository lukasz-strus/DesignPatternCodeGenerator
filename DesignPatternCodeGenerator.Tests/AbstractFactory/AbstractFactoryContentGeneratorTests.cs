using DesignPatternCodeGenerator.AbstractFactory;
using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Tests.AbstractFactory.Data;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.AbstractFactory;

public class AbstractFactoryContentGeneratorTests
{
    [Theory]
    [MemberData(nameof(AbstractFactoryCompilationSources.GetSampleDataToInterfaceTests), MemberType = typeof(AbstractFactoryCompilationSources))]
    internal void GenerateMainInterface_ForValidInputs_ReturnsCorrectMainInterface(string inputSource, string expectedInterface)
    {
        var interfaceGroups = GeneratorTestsHelper.GetInterfaceGroups(inputSource);
        var mainInterfaceGroup = interfaceGroups.GroupByAttribute().First();
        var interfaceGroup = mainInterfaceGroup.GroupByIdentifier();

        var result = AbstractFactoryContentGenerator.GenerateMainInterface(mainInterfaceGroup, interfaceGroup);

        result.RemoveWhitespace().Should().Be(expectedInterface.RemoveWhitespace());
    }

    [Theory]
    [MemberData(nameof(AbstractFactoryCompilationSources.GetSampleDataToClassTests), MemberType = typeof(AbstractFactoryCompilationSources))]
    internal void GenerateFactoryClass_ForValidInputs_ReturnCorrectFactoryClass(string inputSource, string expectedFactoryClass)
    {
        var interfaceGroups = GeneratorTestsHelper.GetInterfaceGroups(inputSource);
        var mainInterfaceGroup = interfaceGroups.GroupByAttribute().First();
        var interfaceGroup = mainInterfaceGroup.GroupByIdentifier();

        var classGroups = GeneratorTestsHelper.GetClassGroups(inputSource);
        var filtredClassGroups = FilterCollectionHelper.FilterClassesByInterface(classGroups, interfaceGroup.First().Key);

        var result = AbstractFactoryContentGenerator.GenerateFactoryClass(mainInterfaceGroup, filtredClassGroups.First());

        result.RemoveWhitespace().Should().Be(expectedFactoryClass.RemoveWhitespace());
    }





}
