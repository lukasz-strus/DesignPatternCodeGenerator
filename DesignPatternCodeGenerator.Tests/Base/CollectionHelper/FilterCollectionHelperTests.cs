using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Tests.Base.CollectionHelper.Data;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Base.CollectionHelper;

public class FilterCollectionHelperTests
{
    [Theory]
    [MemberData(nameof(CompilationSources.GetSampleDataToFilterClassByInterface), MemberType = typeof(CompilationSources))]
    internal void FilterClassesByInterface_ForValidInput_ReturnFiltredCollection(
        string compilationSource,
        string interfaceName,
        string outputGroupKey)
    {
        var classGroups = GeneratorTestsHelper.GetClassGroups(compilationSource);

        var result = classGroups.FilterByInterface(interfaceName);

        result.First().Key
              .Should()
              .Be(outputGroupKey);
    }

    [Theory]
    [MemberData(nameof(CompilationSources.GetSampleDataToFilterClassByInterfaces), MemberType = typeof(CompilationSources))]
    internal void FilterClassesByInterfaces_ForValidInput_ReturnFiltredCollection(
        string compilationSource,
        IEnumerable<string> interfaceNames,
        string outputGroupKey)
    {
        var classGroups = GeneratorTestsHelper.GetClassGroups(compilationSource);

        var result = classGroups.FilterByInterface(interfaceNames);

        result.First().Key
              .Should()
              .Be(outputGroupKey);
    }

}
