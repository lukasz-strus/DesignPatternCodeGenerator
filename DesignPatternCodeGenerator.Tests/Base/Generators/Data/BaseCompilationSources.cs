using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Tests.Helpers;

namespace DesignPatternCodeGenerator.Tests.Base.Generators.Data;

public static class BaseCompilationSources
{
    public static IEnumerable<object[]> GetSampleDataAccesibilityClassTests()
    {
        yield return new object[] { _inputSourcePublicClass, _outputPublic, };
        yield return new object[] { _inputSourceInternalClass, _outputInternal };
    }

    public static IEnumerable<object[]> GetSampleDataAccesibilityInterfaceTests()
    {
        yield return new object[] { _inputSourcePublicInterface, _outputPublic };
        yield return new object[] { _inputSourceInternalInterface, _outputInternal };
    }

    public static IEnumerable<object[]> GetSampleDataAccesibilityMethodTests()
    {
        yield return new object[] { _inputSourcePublicMethod, _outputPublic };
        yield return new object[] { _inputSourceInternalMethod, _outputInternal };
        yield return new object[] { _inputSourceProtectedMethod, _outputProtected };
        yield return new object[] { _inputSourcePrivateMethod, _outputPrivate };
    }

    public static IEnumerable<object[]> GetSampleDataNamespace()
    {
        yield return new object[] { _inputSourcePublicClass, _namespace };
    }

    public static IEnumerable<object[]> GetSampleDataUsings()
    {
        yield return new object[] { _inputSourcePublicClass, _using };
        yield return new object[] { _inputSourceInternalClass, _usings };
    }

    public static IEnumerable<object[]> GetSampleDataClassNameFromClassGroup()
    {
        yield return new object[] { _inputSourcePublicClass, _className, };
    }

    public static IEnumerable<object[]> GetSampleDataClassNameFromInterfaceGroup()
    {
        yield return new object[] { _inputSourcePublicInterface, _className, };
    }

    public static IEnumerable<object[]> GetSampleDataClassNameFromClassGroupAndType()
    {
        yield return new object[] { _inputSourcePublicClass, GeneratorAttributeType.Factory, _classNameFactory, };
        yield return new object[] { _inputSourcePublicClass, GeneratorAttributeType.NullObject, _classNameNullObject, };
        yield return new object[] { _inputSourcePublicClass, GeneratorAttributeType.Singleton, _classNameSingleton, };
        yield return new object[] { _inputSourcePublicClass, GeneratorAttributeType.Prototype, _classNamePrototype, };
    }

    public static IEnumerable<object[]> GetSampleDataClassNameFromInterfaceGroupAndType()
    {
        yield return new object[] { _inputSourcePublicInterface, GeneratorAttributeType.Factory, _classNameFactory, };
        yield return new object[] { _inputSourcePublicInterface, GeneratorAttributeType.NullObject, _classNameNullObject, };
        yield return new object[] { _inputSourcePublicInterface, GeneratorAttributeType.Singleton, _classNameSingleton, };
        yield return new object[] { _inputSourcePublicInterface, GeneratorAttributeType.Prototype, _classNamePrototype, };
    }

    public static IEnumerable<object[]> GetSampleDataInterfaceNameFromInterfaceGroup()
    {
        yield return new object[] { _inputSourcePublicInterface, _interfaceName, };
    }

    public static IEnumerable<object[]> GetSampleDataInterfaceNameFromInterfaceGroupAndType()
    {
        yield return new object[] { _inputSourcePublicInterface, GeneratorAttributeType.Factory, _interfaceNameFactory, };
        yield return new object[] { _inputSourcePublicInterface, GeneratorAttributeType.NullObject, _interfaceNameNullObject, };
        yield return new object[] { _inputSourcePublicInterface, GeneratorAttributeType.Singleton, _interfaceNameSingleton, };
        yield return new object[] { _inputSourcePublicInterface, GeneratorAttributeType.Prototype, _interfaceNamePrototype, };
    }

    public static IEnumerable<object[]> GetSampleDataInterfaceNameFromClassGroupAndType()
    {
        yield return new object[] { _inputSourcePublicClass, GeneratorAttributeType.Factory, _interfaceNameFactory, };
        yield return new object[] { _inputSourcePublicClass, GeneratorAttributeType.NullObject, _interfaceNameNullObject, };
        yield return new object[] { _inputSourcePublicClass, GeneratorAttributeType.Singleton, _interfaceNameSingleton, };
        yield return new object[] { _inputSourcePublicClass, GeneratorAttributeType.Prototype, _interfaceNamePrototype, };
    }

    public static IEnumerable<object[]> GetSampleDataUsingsAndNamespace()
    {
        yield return new object[] { _inputSourcePublicClass, _usingAndNamespace, null};
        yield return new object[] { _inputSourceInternalClass, _usingsAndNamespace, null };
        yield return new object[] { _inputSourceInternalClass, _usingsAndNamespaceWithAdditional1, _inputSourcePublicInterface };
        yield return new object[] { _inputSourceInternalClass, _usingsAndNamespaceWithAdditional2, _inputSourceInternalInterface };
    }

    private static readonly string _inputSourcePublicClass =
        @"using System;

namespace Test.Test
{
    public class TestClass
    {
    }
}";

    private static readonly string _inputSourceInternalClass =
        @"using System;
using FluentAssertions;
using Xunit;

namespace Test.Test
{
    internal class TestClass
    {
    }
}";

    private static readonly string _inputSourcePublicInterface =
        @"using System;

namespace Test.InterfaceTest
{
    public interface ITestClass
    {
    }
}";

    private static readonly string _inputSourceInternalInterface =
        @"using System;

namespace InterfaceTest.InterfaceTest
{
    internal interface ITestClass
    {
    }
}";

    private static readonly string _inputSourcePublicMethod =
        @"using System;

namespace Test.Test
{
    public class TestClass
    {
        public void TestMethod() {}
    }
}";

    private static readonly string _inputSourceInternalMethod =
        @"using System;

namespace Test.Test
{
    public class TestClass
    {
        internal void TestMethod() {}
    }
}";

    private static readonly string _inputSourceProtectedMethod =
        @"using System;

namespace Test.Test
{
    public class TestClass
    {
        protected void TestMethod() {}
    }
}";

    private static readonly string _inputSourcePrivateMethod =
        @"using System;

namespace Test.Test
{
    public class TestClass
    {
        private void TestMethod() {}
    }
}";

    private static readonly string _className = "TestClass";

    private static readonly string _classNameFactory = "TestClassFactory";

    private static readonly string _classNameNullObject = "TestClassNullObject";

    private static readonly string _classNameSingleton = "TestClassSingleton";

    private static readonly string _classNamePrototype = "TestClassPrototype";

    private static readonly string _interfaceName = "ITestClass";

    private static readonly string _interfaceNameFactory = "ITestClassFactory";

    private static readonly string _interfaceNameNullObject = "ITestClassNullObject";

    private static readonly string _interfaceNameSingleton = "ITestClassSingleton";

    private static readonly string _interfaceNamePrototype = "ITestClassPrototype";

    private static readonly string _namespace = "Test.Test";

    private static readonly List<string> _using = new() { "System" };

    private static readonly List<string> _usings = new() { "System", "Xunit", "FluentAssertions" };

    private static readonly string _outputPublic = "public";

    private static readonly string _outputInternal = "internal";

    private static readonly string _outputProtected = "protected";

    private static readonly string _outputPrivate = "private";

    private static readonly string _usingAndNamespace =
        @"// <auto-generated/>
using System;

namespace Test.Test";

    private static readonly string _usingsAndNamespace =
        @"// <auto-generated/>
using System;
using FluentAssertions;
using Xunit;

namespace Test.Test";

    private static readonly string _usingsAndNamespaceWithAdditional1 =
        @"// <auto-generated/>
using System;
using FluentAssertions;
using Xunit;
using Test.InterfaceTest;


namespace Test.Test";

    private static readonly string _usingsAndNamespaceWithAdditional2 =
        @"// <auto-generated/>
using System;
using FluentAssertions;
using Xunit;
using InterfaceTest.InterfaceTest;


namespace Test.Test";
}
