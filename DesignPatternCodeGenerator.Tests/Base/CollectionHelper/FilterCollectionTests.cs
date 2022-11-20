using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Base.CollectionHelper
{
    public class FilterCollectionTests
    {

        [Theory]
        [InlineData(FACTORY_COMPILATION_SOURCE, FACTORYCHILD_COMPILATION_SOURCE, INTERFACE_NAME)]
        internal void FilterClassesByInterface_ForValidInput_ReturnFiltredCollection(
            string compilationSource,
            string expectedSource,
            string interfaceName)
        {
            var classGroups = GeneratorTestsHelper.GetClassGroups(compilationSource);
            var expectedClassGroups = GeneratorTestsHelper.GetClassGroups(expectedSource);

            var result = FilterCollectionHelper.FilterClassesByInterface(classGroups, interfaceName);

            result.Select(x => x.Key)
                  .Should()
                  .Equal(expectedClassGroups.Select(x => x.Key));
        }

        [Theory]
        [InlineData(ABSTRACT_FACTORY_SOURCE, ATTRIBUTE_TEXT_VALUE, CLASSNAME)]
        internal void FilterClassesByAttributeTextValue_ForValidInput_ReturnFiltredCollection(
            string compilationSource,
            string attributeTextValue,
            string className)
        {
            var classGroups = GeneratorTestsHelper.GetClassGroups(compilationSource);

            var result = FilterCollectionHelper.FilterClassesByAttributeTextValue(classGroups, attributeTextValue);

            result.Select(x => x.Key).Should().Equal(className);
        }

        private const string FACTORY_COMPILATION_SOURCE =
        @"using DesignPatternCodeGenerator.Attributes.Factory;
using System;

namespace DesignPatternCodeGenerator.Tests.Data
{
    [Factory]
    public interface ITest { }

    [FactoryChild]
    public class Test1 : ITest { }

    [FactoryChild]
    public class Test2 : ITest { }

    [FactoryChild]
    public class Test3 : ITest { }

    [Factory]
    public interface IAnotherTest { }

    [FactoryChild]
    public class AnotherTest1 : IAnotherTest { }

    [FactoryChild]
    public class AnotherTest2 : IAnotherTest { }
}";

        private const string FACTORYCHILD_COMPILATION_SOURCE =
        @"using DesignPatternCodeGenerator.Attributes.Factory;
using System;

namespace DesignPatternCodeGenerator.Tests.Data
{
    [Factory]
    public interface ITest { }

    [FactoryChild]
    public class Test1 : ITest { }

    [FactoryChild]
    public class Test2 : ITest { }

    [FactoryChild]
    public class Test3 : ITest { }
}";

        private const string INTERFACE_NAME = "ITest";

        private const string ABSTRACT_FACTORY_SOURCE = @"using DesignPatternCodeGenerator.Attributes.AbstractFactory;

namespace Samples.AbstractFactory
{
    [AbstractFactory(""Gears"")]
    public interface IMonitor
    {
        void On();
        void Off();
    }

    [AbstractFactoryChild(""Samsung"")]
    public class SamsungMonitor : IMonitor
    {
        public void On()
        {
            Console.WriteLine(""Samsung on"");
        }

        public void Off()
        {
            Console.WriteLine(""Samsung off"");
        }
    }

    [AbstractFactoryChild(""Benq"")]
    public class BenqMonitor : IMonitor
    {
        public void On()
        {
            Console.WriteLine(""Benq on"");
        }

        public void Off()
        {
            Console.WriteLine(""Benq off"");
        }
    }
}";

        private const string ATTRIBUTE_TEXT_VALUE = "Benq";
        private const string CLASSNAME = "BenqMonitor";
    }
}
