using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Tests.Base.CollectionHelper.Data;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Base.CollectionHelper;

public class GroupCollectionHelperTests
{
    [Theory]
    [MemberData(nameof(CompilationSources.GetSampleDataToGroupInterfaceByAttribute), MemberType = typeof(CompilationSources))]
    internal void GroupByAttribute_ForValidInputs_ReturnGroupedInterfaceCollection(string inputSource, string outputGroupKey)
    {
        var interfaceGroup = GeneratorTestsHelper.GetInterfaceGroups(inputSource);

        var result = interfaceGroup.GroupByAttribute();

        result.First().Key
              .Should()
              .Be(outputGroupKey);
    }

    [Theory]
    [MemberData(nameof(CompilationSources.GetSampleDataToGroupClassByAttribute), MemberType = typeof(CompilationSources))]
    internal void GroupByAttribute_ForValidInputs_ReturnGroupedClassCollection(string inputSource, string outputGroupKey)
    {
        var classGroup = GeneratorTestsHelper.GetClassGroups(inputSource);

        var result = classGroup.GroupByAttribute();

        result.First().Key
              .Should()
              .Be(outputGroupKey);
    }

    [Theory]
    [MemberData(nameof(CompilationSources.GetSampleDataToGroupMehtodByAttribute), MemberType = typeof(CompilationSources))]
    internal void GroupByAttribute_ForValidInputs_ReturnGroupedMethodCollection(string inputSource, string outputGroupKey)
    {
        var methodGroups = GeneratorTestsHelper.GetMethodGroups(inputSource);

        var result = methodGroups.GroupByAttribute();

        result.First().Key
              .Should()
              .Be(outputGroupKey);
    }

    [Theory]
    [MemberData(nameof(CompilationSources.GetSampleDataToGroupInterfaceByIdentifier), MemberType = typeof(CompilationSources))]
    internal void GroupByIdentifier_ForValidInputs_ReturnGroupedInterfaceCollection(string inputSource, string outputGroupKey)
    {
        var interfaceGroup = GeneratorTestsHelper.GetInterfaceGroups(inputSource);

        var groupByAttribute = interfaceGroup.GroupByAttribute().First();

        var result = groupByAttribute.GroupByIdentifier();

        result.First().Key
              .Should()
              .Be(outputGroupKey);
    }

    [Theory]
    [MemberData(nameof(CompilationSources.GetSampleDataToGroupClassByIdentifier), MemberType = typeof(CompilationSources))]
    internal void GroupByIdentifier_ForValidInputs_ReturnGroupedClassCollection(string inputSource, string outputGroupKey)
    {
        var classGroup = GeneratorTestsHelper.GetClassGroups(inputSource);

        var groupByAttribute = classGroup.GroupByAttribute().First();

        var result = groupByAttribute.GroupByIdentifier();

        result.First().Key
              .Should()
              .Be(outputGroupKey);
    }
}
