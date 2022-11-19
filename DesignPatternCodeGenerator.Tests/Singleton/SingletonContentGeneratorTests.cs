﻿using DesignPatternCodeGenerator.Singleton;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Singleton;

public class SingletonContentGeneratorTests
{
    [Theory]
    [InlineData(SINGLETON_COMPILATION_SOURCE, EXPECTED_COMPILATION_SOURCE)]
    internal void GenerateClass_ForValidInputs_ReturnInterface(string inputSource, string expectedSource)
    {
        var classGroup = GeneratorTestsHelper.GetClassGroup(inputSource);

        var result = SingletonContentGenerator.GenerateClass(classGroup);

        result.RemoveWhitespace().Should().Be(expectedSource.RemoveWhitespace());

    }

    private const string SINGLETON_COMPILATION_SOURCE =
    @"using DesignPatternCodeGenerator.Attributes.Singleton;
using System;

namespace Test.Test
{
    [Singleton]
    public partial class Test
    {

    }
}";

    private const string EXPECTED_COMPILATION_SOURCE =
@"// <auto-generated/>
using DesignPatternCodeGenerator.Attributes.Singleton;
using System;

namespace Test.Test
{
    public partial class Test
    {
        private static Test _instance = null;
        private static object obj = new object();

        private Test() { }

        public static Test GetInstance()
        {
            lock(obj)
            {
                if (_instance == null)
                {
                    _instance = new Test();
                }
            }
            return _instance;
        }        
    }
}";
}
