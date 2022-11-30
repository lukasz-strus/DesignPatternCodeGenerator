﻿using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Facade;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Facade
{
    public class FacadeContentFieldsGeneratorTests
    {
        [Theory]
        [InlineData(INPUT_COMPILATION_SOURCE, EXPECTED_COMPILATION_SOURCE)]
        internal void GenerateClass_ForValidInputs_ReturnInterface(string inputSource, string expectedSource)
        {
            var methodGroups = GeneratorTestsHelper.GetMethodGroups(inputSource);

            var methodGroupsByAttributeText = GroupCollectionHelper.GroupCollectionByAttributeValueText(methodGroups);

            var result = FacadeContentFieldsGenerator.GenerateFileds(methodGroupsByAttributeText.First());

            result.RemoveWhitespace().Should().Be(expectedSource.RemoveWhitespace());
        }

        private const string INPUT_COMPILATION_SOURCE =
@"using DesignPatternCodeGenerator.Attributes.Facade;
using System;

namespace Test.Test
{
    public class QualityScanner
    {
        [FacadeMethod(""Scan"")]
        public IEnumerable<string> QualityScan([FacadeMainParameter(""githubUrl"")] string githubUrl)
        {
            Console.WriteLine(""Quality scan"");

            return new List<string>() { ""Error1"", ""Error2"" };
        }
    }

    public class DependencyScanner
    {
        [FacadeMethod(""Scan"")]
        public IEnumerable<string> DependencyScan([FacadeMainParameter(""githubUrl"")]string githubUrl)
        {
            Console.WriteLine(""Dependency Scan"");

            return new List<string>() { ""Dependency Error1"" };
        }
    }

    public class SecurityScanner
    {
        [FacadeMethod(""Scan"")]
        public IEnumerable<string> SecurityScan([FacadeMainParameter(""githubUrl"")] string githubUrl)
        {
            Console.WriteLine(""Security scan"");

            return new List<string>() { ""security error1"" };
        }
    }

    public class ReportGenerator
    {
        [FacadeMethod(""Scan"")]
        public void GenerateReport(
            [FacadeParameter(""QualityScanner.QualityScan"")] IEnumerable<string> qualityScanErrors,
            [FacadeParameter(""SecurityScanner.SecurityScan"")] IEnumerable<string> securityScanErrors,
            [FacadeParameter(""DependencyScanner.DependencyScan"")] IEnumerable<string> dependencyScanErrors)
        {
            Console.WriteLine(""Quality Scan Errors:"");
            Console.WriteLine(string.Join("", "", qualityScanErrors));

            Console.WriteLine(""Security Scan Errors:"");
            Console.WriteLine(string.Join("", "", securityScanErrors));

            Console.WriteLine(""Dependency Scan Errors:"");
            Console.WriteLine(string.Join("", "", dependencyScanErrors));
        }
    }
}";

        private const string EXPECTED_COMPILATION_SOURCE =
      @"private QualityScanner _qualityScanner = new QualityScanner();
        private DependencyScanner _dependencyScanner = new DependencyScanner();
	    private SecurityScanner _securityScanner = new SecurityScanner();
	    private ReportGenerator _reportGenerator = new ReportGenerator();";
    }
}
