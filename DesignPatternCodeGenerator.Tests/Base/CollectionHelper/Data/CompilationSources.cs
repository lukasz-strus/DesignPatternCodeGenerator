namespace DesignPatternCodeGenerator.Tests.Base.CollectionHelper.Data;

public class CompilationSources
{
    public static IEnumerable<object[]> GetSampleDataToGroupInterfaceByAttribute()
    {
        yield return new object[] { _inputGearSource, _attributeGearInterface };
        yield return new object[] { _inputUISource, _attributeUIInterface };
    }

    public static IEnumerable<object[]> GetSampleDataToGroupClassByAttribute()
    {
        yield return new object[] { _inputGearSource, _attributeGearClass };
        yield return new object[] { _inputUISource, _attributeUIClass };
    }

    public static IEnumerable<object[]> GetSampleDataToGroupMehtodByAttribute()
    {
        yield return new object[] { _inputGearSource, _attributeGearMethod };
        yield return new object[] { _inputUISource, _attributeUIMethod };
    }

    public static IEnumerable<object[]> GetSampleDataToGroupInterfaceByIdentifier()
    {
        yield return new object[] { _inputGearSource, _identifierGearInterface };
        yield return new object[] { _inputUISource, _identifierUIInterface };
    }

    public static IEnumerable<object[]> GetSampleDataToGroupClassByIdentifier()
    {
        yield return new object[] { _inputGearSource, _identifierGearClass };
        yield return new object[] { _inputUISource, _identifierUIClass };
    }

    public static IEnumerable<object[]> GetSampleDataToFilterClassByInterface()
    {
        yield return new object[] { _inputSource, _identifierGearInterface, _identifierGearClass };
        yield return new object[] { _inputSource, _identifierUIInterface, _identifierUIClass };
    }

    public static IEnumerable<object[]> GetSampleDataToFilterClassByInterfaces()
    {
        yield return new object[] { _inputSource, _gearInterfaces, _identifierGearClass };
        yield return new object[] { _inputSource, _uiInterfaces, _identifierUIClass };
    }

    private static readonly string _identifierGearClass = "SamsungMouse";

    private static readonly string _identifierUIClass = "WindowsTextBox";

    private static readonly string _identifierGearInterface = "IMouse";

    private static readonly string _identifierUIInterface = "ITextBox";

    private static readonly string _attributeGearInterface= "Gear";

    private static readonly string _attributeUIInterface = "UIElement";

    private static readonly string _attributeGearClass = "Samsung";

    private static readonly string _attributeUIClass = "Windows";

    private static readonly string _attributeGearMethod = "Computer";

    private static readonly string _attributeUIMethod = "UI";

    private static readonly List<string> _gearInterfaces = new() { "IMouse", "IMonitor" };

    private static readonly List<string> _uiInterfaces = new() { "ITextBox", "IButton" };

    private static readonly string _inputGearSource =
        @"using DesignPatternCodeGenerator.Attributes.AbstractFactory;
using System;

namespace Test.Test
{
    [AbstractFactory(""Gear"")]
    public interface IMouse
    {
        [Facade(""Computer"")]
        void Click();
    }

    [AbstractFactoryChild(""Samsung"")]
    public class SamsungMouse : IMouse
    {
        [Facade(""Computer"")]
        public void Click()
        {
            Console.WriteLine(""Click Samsung"");
        }
    }

    [AbstractFactoryChild(""Benq"")]
    public class BenqMouse : IMouse
    {
        [Facade(""Computer"")]
        public void Click()
        {
            Console.WriteLine(""Click Benq"");
        }
    }

    [AbstractFactory(""Gear"")]
    public interface IMonitor
    {
        [Facade(""Computer"")]
        void On();

        [Facade(""Computer"")]
        void Off();
    }

    [AbstractFactoryChild(""Samsung"")]
    public class SamsungMonitor : IMonitor
    {
        [Facade(""Computer"")]
        public void On()
        {
            Console.WriteLine(""On Samsung monitor"");
        }

        [Facade(""Computer"")]
        public void Off()
        {
            Console.WriteLine(""Off Samsung monitor"");
        }
    }

    [AbstractFactoryChild(""Benq"")]
    public class BenqMonitor : IMonitor
    {
        [Facade(""Computer"")]
        public void On()
        {
            Console.WriteLine(""On Benq monitor"");
        }

        [Facade(""Computer"")]
        public void Off()
        {
            Console.WriteLine(""Off Benq monitor"");
        }
    }
}";

    private static readonly string _inputUISource =
        @"using DesignPatternCodeGenerator.Attributes.AbstractFactory;
using System;

namespace Test.Test
{
    [AbstractFactory(""UIElement"")]
    public interface ITextBox
    {
        [Facade(""UI"")]
        void Render();

        [Facade(""UI"")]
        void HandleInput();
    }

    [AbstractFactoryChild(""Windows"")]
    public class WindowsTextBox : ITextBox
    {
        [Facade(""UI"")]
        public void HandleInput()
        {
            Console.WriteLine(""Handle windows text input"");
        }

        [Facade(""UI"")]
        public void Render()
        {
            Console.WriteLine(""Render windows textbox"");
        }
    }

    [AbstractFactoryChild(""Mac"")]
    public class MacTextBox : ITextBox
    {
        [Facade(""UI"")]
        public void HandleInput()
        {
            Console.WriteLine(""Handle mac text input"");
        }

        [Facade(""UI"")]
        public void Render()
        {
            Console.WriteLine(""Render mac textbox"");
        }
    }

    [AbstractFactory(""UIElement"")]
    public interface IButton
    {
        [Facade(""UI"")]
        void Render();

        [Facade(""UI"")]
        void HandleClick();
    }

    [AbstractFactoryChild(""Windows"")]
    public class WindowsButton : IButton
    {
        [Facade(""UI"")]
        public void HandleClick()
        {
            Console.WriteLine(""Handle windows click event"");
        }

        [Facade(""UI"")]
        public void Render()
        {
            Console.WriteLine(""Render windows button"");
        }
    }

    [AbstractFactoryChild(""Mac"")]
    public class MacButton : IButton
    {
        [Facade(""UI"")]
        public void HandleClick()
        {
            Console.WriteLine(""Handle mac click event"");
        }

        [Facade(""UI"")]
        public void Render()
        {
            Console.WriteLine(""Render mac button"");
        }
    }
}";

    private static readonly string _inputSource = 
        @"using DesignPatternCodeGenerator.Attributes.AbstractFactory;
using System;

namespace Test.Test
{
    [AbstractFactory(""Gear"")]
    public interface IMouse
    {
        void Click();
    }

    [AbstractFactoryChild(""Samsung"")]
    public class SamsungMouse : IMouse
    {
        public void Click()
        {
            [Facade(""Computer"")]
            Console.WriteLine(""Click Samsung"");
        }
    }

    [AbstractFactoryChild(""Benq"")]
    public class BenqMouse : IMouse
    {
        public void Click()
        {
            [Facade(""Computer"")]
            Console.WriteLine(""Click Benq"");
        }
    }

    [AbstractFactory(""Gear"")]
    public interface IMonitor
    {
        void On();
        void Off();
    }

    [AbstractFactoryChild(""Samsung"")]
    public class SamsungMonitor : IMonitor
    {
        [Facade(""Computer"")]
        public void On()
        {
            Console.WriteLine(""On Samsung monitor"");
        }

        [Facade(""Computer"")]
        public void Off()
        {
            Console.WriteLine(""Off Samsung monitor"");
        }
    }

    [AbstractFactoryChild(""Benq"")]
    public class BenqMonitor : IMonitor
    {
        [Facade(""Computer"")]
        public void On()
        {
            Console.WriteLine(""On Benq monitor"");
        }

        [Facade(""Computer"")]
        public void Off()
        {
            Console.WriteLine(""Off Benq monitor"");
        }
    }

    [AbstractFactory(""UIElement"")]
    public interface ITextBox
    {
        void Render();
        void HandleInput();
    }

    [AbstractFactoryChild(""Windows"")]
    public class WindowsTextBox : ITextBox
    {
        [Facade(""UI"")]
        public void HandleInput()
        {
            Console.WriteLine(""Handle windows text input"");
        }

        [Facade(""UI"")]
        public void Render()
        {
            Console.WriteLine(""Render windows textbox"");
        }
    }

    [AbstractFactoryChild(""Mac"")]
    public class MacTextBox : ITextBox
    {
        [Facade(""UI"")]
        public void HandleInput()
        {
            Console.WriteLine(""Handle mac text input"");
        }

        [Facade(""UI"")]
        public void Render()
        {
            Console.WriteLine(""Render mac textbox"");
        }
    }

    [AbstractFactory(""UIElement"")]
    public interface IButton
    {
        void Render();
        void HandleClick();
    }

    [AbstractFactoryChild(""Windows"")]
    public class WindowsButton : IButton
    {
        [Facade(""UI"")]
        public void HandleClick()
        {
            Console.WriteLine(""Handle windows click event"");
        }

        [Facade(""UI"")]
        public void Render()
        {
            Console.WriteLine(""Render windows button"");
        }
    }

    [AbstractFactoryChild(""Mac"")]
    public class MacButton : IButton
    {
        [Facade(""UI"")]
        public void HandleClick()
        {
            Console.WriteLine(""Handle mac click event"");
        }

        [Facade(""UI"")]
        public void Render()
        {
            Console.WriteLine(""Render mac button"");
        }
    }
}";
}
