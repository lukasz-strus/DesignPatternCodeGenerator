using DesignPatternCodeGenerator.Attributes.IoCContainer;
using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.IoCContainer;
using DesignPatternCodeGenerator.Tests.ContainerIOC.Data;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;
namespace DesignPatternCodeGenerator.Tests.ContainerIOC;

public class ContainerContentGeneratorTests
{
    [Theory]
    [MemberData(nameof(ContainerCompilationSources.GetSampleDataToClassTests), MemberType = typeof(ContainerCompilationSources))]
    internal void GenerateClass_ForValidInputs_ReturnInterface(string inputSource, string expectedSource)
    {
        var compilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        var source = new CancellationTokenSource();
        var token = source.Token;

        var classGroups = DeclarationsSyntaxGenerator.GetClassGroups(compilation, token, typeof(ContainerAttribute));
        classGroups = classGroups.GroupByAttribute();

        var result = ContainerContentGenerator.GenerateClass(classGroups.First(), compilation);

        result.RemoveWhitespace().Should().Be(expectedSource.RemoveWhitespace());
    }


}
