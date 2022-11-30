namespace DesignPatternCodeGenerator.Tests.Facade.Data;

public static class FacadeCompilationSources
{
    //public static IEnumerable<object> GetSampleDataToFieldsTests()
    //{

    //}

    private string INPUT_COMPILATION_SOURCE =
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
}";
}
