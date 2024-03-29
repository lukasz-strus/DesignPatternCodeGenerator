﻿using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Facade;
using DesignPatternCodeGenerator.Tests.Facade.Data;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Facade;

public class FacadeContentGeneratorTests
{
    [Theory]
    [MemberData(nameof(FacadeCompilationSources.GetSampleDataToContentTests), MemberType = typeof(FacadeCompilationSources))]
    internal void GenerateClass_ForValidInputs_ReturnInterface(string inputSource, string expectedSource)
    {
        var methodGroups = GeneratorTestsHelper.GetMethodGroups(inputSource);

        var result = FacadeContentGenerator.GenerateClass(methodGroups.GroupByAttribute().First());

        result.RemoveWhitespace().Should().Be(expectedSource.RemoveWhitespace());
    }
}
