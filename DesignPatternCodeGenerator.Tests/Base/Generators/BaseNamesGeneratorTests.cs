using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Tests.Base.Generators.Data;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Base.Generators;

public class BaseNamesGeneratorTests
{
    [Theory]
    [MemberData(nameof(BaseCompilationSources.GetSampleDataAccesibilityClassTests), MemberType = typeof(BaseCompilationSources))]
    internal void GetAccesibility_ForClassGroup_ReturnsCorrectAccesibility(string source, string expected)
    {
        var classGroup = GeneratorTestsHelper.GetClassGroup(source);

        var result = BaseNamesGenerator.GetAccesibility(classGroup);

        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(BaseCompilationSources.GetSampleDataAccesibilityClassTests), MemberType = typeof(BaseCompilationSources))]
    internal void GetAccesibility_ForClass_ReturnsCorrectAccesibility(string source, string expected)
    {
        var @class = GeneratorTestsHelper.GetClassGroup(source).First();

        var result = BaseNamesGenerator.GetAccesibility(@class);

        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(BaseCompilationSources.GetSampleDataAccesibilityInterfaceTests), MemberType = typeof(BaseCompilationSources))]
    internal void GetAccesibility_ForInterfaceGroup_ReturnsCorrectAccesibility(string source, string expected)
    {
        var interfaceGroup = GeneratorTestsHelper.GetInterfaceGroup(source);

        var result = BaseNamesGenerator.GetAccesibility(interfaceGroup);

        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(BaseCompilationSources.GetSampleDataAccesibilityInterfaceTests), MemberType = typeof(BaseCompilationSources))]
    internal void GetAccesibility_ForInterface_ReturnsCorrectAccesibility(string source, string expected)
    {
        var @interface = GeneratorTestsHelper.GetInterfaceGroup(source).First();

        var result = BaseNamesGenerator.GetAccesibility(@interface);

        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(BaseCompilationSources.GetSampleDataAccesibilityMethodTests), MemberType = typeof(BaseCompilationSources))]
    internal void GetAccesibility_ForMethodGroup_ReturnsCorrectAccesibility(string source, string expected)
    {
        var methodGroup = GeneratorTestsHelper.GetMethodGroup(source);

        var result = BaseNamesGenerator.GetAccesibility(methodGroup);

        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(BaseCompilationSources.GetSampleDataAccesibilityMethodTests), MemberType = typeof(BaseCompilationSources))]
    internal void GetAccesibility_ForMethod_ReturnsCorrectAccesibility(string source, string expected)
    {
        var method = GeneratorTestsHelper.GetMethodGroup(source).First();

        var result = BaseNamesGenerator.GetAccesibility(method);

        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(BaseCompilationSources.GetSampleDataNamespace), MemberType = typeof(BaseCompilationSources))]
    internal void GetNamespace_ForClassGroup_ReturnsCorrectNamespace(string source, string expected)
    {
        var classGroup = GeneratorTestsHelper.GetClassGroup(source);

        var result = BaseNamesGenerator.GetNamespace(classGroup);

        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(BaseCompilationSources.GetSampleDataUsings), MemberType = typeof(BaseCompilationSources))]
    internal void GetUsings_ForClassGroup_ReturnsCorrectUsings(string source, IEnumerable<string> expected)
    {
        var classGroup = GeneratorTestsHelper.GetClassGroup(source);

        var result = BaseNamesGenerator.GetUsings(classGroup);

        result.Should().BeEquivalentTo(expected);
    }

    [Theory]
    [MemberData(nameof(BaseCompilationSources.GetSampleDataClassNameFromClassGroup), MemberType = typeof(BaseCompilationSources))]
    internal void GetClassName_ForClassGroup_ReturnsCorrectClassName(string source, string expected)
    {
        var classGroup = GeneratorTestsHelper.GetClassGroup(source);

        var result = BaseNamesGenerator.GetClassName(classGroup);

        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(BaseCompilationSources.GetSampleDataClassNameFromInterfaceGroup), MemberType = typeof(BaseCompilationSources))]
    internal void GetClassName_ForInterfaceGroup_ReturnsCorrectClassName(string source, string expected)
    {
        var classGroup = GeneratorTestsHelper.GetInterfaceGroup(source);

        var result = BaseNamesGenerator.GetClassName(classGroup);

        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(BaseCompilationSources.GetSampleDataClassNameFromClassGroupAndType), MemberType = typeof(BaseCompilationSources))]
    internal void GetClassName_ForClassGroupAndType_ReturnsCorrectClassName(string source, GeneratorAttributeType type, string expected)
    {
        var classGroup = GeneratorTestsHelper.GetClassGroup(source);

        var result = BaseNamesGenerator.GetClassName(classGroup, type);

        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(BaseCompilationSources.GetSampleDataClassNameFromInterfaceGroupAndType), MemberType = typeof(BaseCompilationSources))]
    internal void GetClassName_ForInterfaceGroupAndType_ReturnsCorrectClassName(string source, GeneratorAttributeType type, string expected)
    {
        var classGroup = GeneratorTestsHelper.GetInterfaceGroup(source);

        var result = BaseNamesGenerator.GetClassName(classGroup, type);

        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(BaseCompilationSources.GetSampleDataInterfaceNameFromInterfaceGroup), MemberType = typeof(BaseCompilationSources))]
    internal void GetInterfaceName_ForInterfaceGroup_ReturnsCorrectInterfaceName(string source, string expected)
    {
        var classGroup = GeneratorTestsHelper.GetInterfaceGroup(source);

        var result = BaseNamesGenerator.GetInterfaceName(classGroup);

        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(BaseCompilationSources.GetSampleDataInterfaceNameFromInterfaceGroupAndType), MemberType = typeof(BaseCompilationSources))]
    internal void GetInterfaceName_ForInterfaceGroupAndType_ReturnsCorrectInterfaceName(string source, GeneratorAttributeType type, string expected)
    {
        var classGroup = GeneratorTestsHelper.GetInterfaceGroup(source);

        var result = BaseNamesGenerator.GetInterfaceName(classGroup, type);

        result.Should().Be(expected);
    }

    [Theory]
    [MemberData(nameof(BaseCompilationSources.GetSampleDataInterfaceNameFromClassGroupAndType), MemberType = typeof(BaseCompilationSources))]
    internal void GetInterfaceName_ForClassGroupAndType_ReturnsCorrectInterfaceName(string source, GeneratorAttributeType type, string expected)
    {
        var classGroup = GeneratorTestsHelper.GetClassGroup(source);

        var result = BaseNamesGenerator.GetInterfaceName(classGroup, type);

        result.Should().Be(expected);
    }
}
