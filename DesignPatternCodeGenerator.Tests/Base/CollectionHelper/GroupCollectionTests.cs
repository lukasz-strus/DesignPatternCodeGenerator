using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.Base.CollectionHelper
{
    public class GroupCollectionTests
    {
        [Theory]
        [InlineData(INPUT_SOURCE)]
        internal void GroupByAttributeValueText_ForValidInputs_ReturnGroupedCollection(string inputSource)
        {
            var interfaceGroup = GeneratorTestsHelper.GetInterfaceGroups(inputSource);

            var result = GroupCollectionHelper.GroupCollectionByAttributeValueText(interfaceGroup);

            result.First().Key.Should().Be("Gear");
        }

        [Theory]
        [InlineData(INPUT_SOURCE)]
        internal void GroupByIdentifierText_ForValidInputs_ReturnGroupedCollection(string inputSource)
        {
            var interfaceGroup = GeneratorTestsHelper.GetInterfaceGroups(inputSource);
            var groupByAttribute = GroupCollectionHelper.GroupCollectionByAttributeValueText(interfaceGroup);

            var result = GroupCollectionHelper.GroupByIdentifierText(groupByAttribute.First());

            result.First().Key.Should().Be("IMouse");
        }

        private const string INPUT_SOURCE = @"using DesignPatternCodeGenerator.Attributes.AbstractFactory;
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
            Console.WriteLine(""Click Samsung"");
        }
    }

    [AbstractFactoryChild(""Benq"")]
    public class BenqMouse : IMouse
    {
        public void Click()
        {
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
        public void On()
        {
            Console.WriteLine(""On Samsung monitor"");
        }

        public void Off()
        {
            Console.WriteLine(""Off Samsung monitor"");
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

    [AbstractFactory(""UIElement"")]
    public interface ITextBox
    {
        void Render();
        void HandleInput();
    }

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

    [AbstractFactory(""UIElement"")]
    public interface IButton
    {
        void Render();
        void HandleClick();
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
    }
}
