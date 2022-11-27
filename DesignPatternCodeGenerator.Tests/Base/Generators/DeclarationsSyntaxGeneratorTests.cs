using DesignPatternCodeGenerator.Attributes.Factory;
using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Base.Generators;

public class DeclarationsSyntaxGeneratorTests
{
    [Theory]
    [InlineData(FACTORY_COMPILATION_SOURCE)]
    internal void GetInterfaceGroups_ForValidInputs_ReturnsCorrectKeyValue(string inputSource)
    {
        var compilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        var source = new CancellationTokenSource();
        var token = source.Token;

        var result = DeclarationsSyntaxGenerator.GetInterfaceGroups(compilation, token, typeof(FactoryAttribute));

        result.Select(x => x.Key)
            .First()
            .Should()
            .Be("ITest");
    }

    [Theory]
    [InlineData(FACTORY_COMPILATION_SOURCE)]
    internal void GetClassGroups_ForValidInputs_ReturnsCorrectKeyValue(string inputSource)
    {
        var compilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        var source = new CancellationTokenSource();
        var token = source.Token;

        var result = DeclarationsSyntaxGenerator.GetClassGroups(compilation, token, typeof(FactoryProductAttribute));

        result.Select(x => x.Key)
            .First()
            .Should()
            .Be("Test");
    }

    [Theory]
    [InlineData(CLASS_COMPILATION_SOURCE)]
    internal void GetAllClassGroups_ForValidInputs_ReturnsCorrectKeyValue(string inputSource)
    {
        var compilation = GeneratorTestsHelper.CreateCompilation(inputSource);
        var source = new CancellationTokenSource();
        var token = source.Token;

        var result = DeclarationsSyntaxGenerator.GetAllClassGroups(compilation, token);

        result.Select(x => x.Key)
            .First()
            .Should()
            .Be("Test1");
    }

    private const string FACTORY_COMPILATION_SOURCE =
    @"using DesignPatternCodeGenerator.Attributes.Factory;
using System;

namespace DesignPatternCodeGenerator.Tests.Data
{

    [Factory]
    public interface ITest
    {

    }

    [FactoryProduct]
    public class Test : ITest
    {

    }
}";

    private const string CLASS_COMPILATION_SOURCE =
@"using System;

namespace DesignPatternCodeGenerator.Tests.Data
{    
    public class Test1
    {

    }

    public class Test2
    {

    }

    public class Test3
    {

    }
}";

}
