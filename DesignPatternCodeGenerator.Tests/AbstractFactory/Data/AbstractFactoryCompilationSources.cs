﻿namespace DesignPatternCodeGenerator.Tests.AbstractFactory.Data;

public static class AbstractFactoryCompilationSources
{
    public static IEnumerable<object[]> GetSampleDataToInterfaceTests()
    {
        yield return new object[] { _inputSourceWithTwoAF, _outputFirstInterfaceSource };
    }

    public static IEnumerable<object[]> GetSampleDataToClassTests()
    {
        yield return new object[] { _inputSourceWithTwoAF, _outputFirstClassesSource };
    }

    public static IEnumerable<object[]> GetSampleDataToGeneratorTests()
    {
        yield return new object[] { _inputSourceWithOneAF };
    }


    private static readonly string _outputFirstInterfaceSource =
        @"// <auto-generated/>
using DesignPatternCodeGenerator.Attributes.AbstractFactory;
using System;

namespace Test.Gear
{
    public interface IGearFactory
    {
	    IMouse CreateMouse();
		IMonitor CreateMonitor();
    }
}";

    private static readonly string _outputFirstClassesSource =
        @"// <auto-generated/>
using DesignPatternCodeGenerator.Attributes.AbstractFactory;
using System;
using Test.Samsung;

namespace Test.Gear
{
    public class SamsungFactory : IGearFactory
    {
        public IMouse CreateMouse()
        {
            return new SamsungMouse();
        }
    }
}
";

    private readonly static string _inputSourceWithTwoAF =
        @"using DesignPatternCodeGenerator.Attributes.AbstractFactory;
using System;

namespace Test.Gear
{
    [AbstractFactory(""Gear"")]
    public interface IMouse
    {
        void Click();
    }

    [AbstractFactory(""Gear"")]
    public interface IMonitor
    {
        void On();
        void Off();
    }

}
namespace Test.Samsung
{
    [AbstractFactoryChild(""Samsung"")]
    public class SamsungMouse : IMouse
    {
        public void Click()
        {
            Console.WriteLine(""Click Samsung"");
        }
    }

    [AbstractFactoryChild(""Samsung"")]
    public class SamsungMonitor : IMonitor
    {
        public void On()
        {
            Console.WriteLine(""On Samsung monitor"");
        }

        public void Off()
        {
            Console.WriteLine(""Off Samsung monitor"");
        }
    }
}
namespace Test.Benq
{
    [AbstractFactoryChild(""Benq"")]
    public class BenqMouse : IMouse
    {
        public void Click()
        {
            Console.WriteLine(""Click Benq"");
        }
    }

    [AbstractFactoryChild(""Benq"")]
    public class BenqMonitor : IMonitor
    {
        public void On()
        {
            Console.WriteLine(""On Benq monitor"");
        }

        public void Off()
        {
            Console.WriteLine(""Off Benq monitor"");
        }
    }
}

namespace Test.UIElement
{
    [AbstractFactory(""UIElement"")]
    public interface ITextBox
    {
        void Render();
        void HandleInput();
    }

    [AbstractFactory(""UIElement"")]
    public interface IButton
    {
        void Render();
        void HandleClick();
    }
}
namespace Test.Windows
{

    [AbstractFactoryChild(""Windows"")]
    public class WindowsTextBox : ITextBox
    {
        public void HandleInput()
        {
            Console.WriteLine(""Handle windows text input"");
        }

        public void Render()
        {
            Console.WriteLine(""Render windows textbox"");
        }
    }

    [AbstractFactoryChild(""Windows"")]
    public class WindowsButton : IButton
    {
        public void HandleClick()
        {
            Console.WriteLine(""Handle windows click event"");
        }

        public void Render()
        {
            Console.WriteLine(""Render windows button"");
        }
    }
}
namespace Test.Mac
{

    [AbstractFactoryChild(""Mac"")]
    public class MacTextBox : ITextBox
    {
        public void HandleInput()
        {
            Console.WriteLine(""Handle mac text input"");
        }

        public void Render()
        {
            Console.WriteLine(""Render mac textbox"");
        }
    }

    [AbstractFactoryChild(""Mac"")]
    public class MacButton : IButton
    {
        public void HandleClick()
        {
            Console.WriteLine(""Handle mac click event"");
        }

        public void Render()
        {
            Console.WriteLine(""Render mac button"");
        }
    }
}";

    private readonly static string _inputSourceWithOneAF =
        @"using DesignPatternCodeGenerator.Attributes.AbstractFactory;

namespace Samples.AbstractFactory
{
    [AbstractFactory(""Gears"")]
    public interface IMonitor
    {
        void On();
        void Off();
    }

    [AbstractFactoryClass(""Samsung"")]
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

    [AbstractFactoryClass(""Benq"")]
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

}
