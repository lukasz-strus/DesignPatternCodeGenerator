﻿using DesignPatternCodeGenerator.AbstractFactory;
using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Tests.Helpers;
using FluentAssertions;
using Xunit;

namespace DesignPatternCodeGenerator.Tests.AbstractFactory;

public class AbstractFactoryContentGeneratorTests
{
    [Theory]
    [InlineData(INPUT_SOURCE_CODE, EXPECTED_MAIN_INTERFACE)]
    internal void GenerateMainInterface_ForValidInputs_ReturnsCorrectMainInterface(string inputSource, string expectedInterface)
    {
        var interfaceGroups = GeneratorTestsHelper.GetInterfaceGroups(inputSource);
        var mainInterfaceGroup = GroupCollectionHelper.GroupCollectionByAttributeValueText(interfaceGroups);
        var interfaceGroup = GroupCollectionHelper.GroupByIdentifierText(mainInterfaceGroup.First());

        var result = AbstractFactoryContentGenerator.GenerateMainInterface(mainInterfaceGroup.First(), interfaceGroup);

        result.RemoveWhitespace().Should().Be(expectedInterface.RemoveWhitespace());
    }

    [Theory]
    [InlineData(INPUT_SOURCE_CODE, EXPECTE_CLASSES)]
    internal void GenerateFactoryClass_ForValidInputs_ReturnCorrectFactoryClass(string inputSource, string expectedFactoryClass)
    {
        var interfaceGroups = GeneratorTestsHelper.GetInterfaceGroups(inputSource);
        var mainInterfaceGroup = GroupCollectionHelper.GroupCollectionByAttributeValueText(interfaceGroups);
        var interfaceGroup = GroupCollectionHelper.GroupByIdentifierText(mainInterfaceGroup.First());

        var classGroups = GeneratorTestsHelper.GetClassGroups(inputSource);
        var filtredClassGroups = FilterCollectionHelper.FilterClassesByInterface(classGroups, interfaceGroup.First().Key);

        var result = AbstractFactoryContentGenerator.GenerateFactoryClass(mainInterfaceGroup.First(), filtredClassGroups.First());

        result.RemoveWhitespace().Should().Be(expectedFactoryClass.RemoveWhitespace());
    }


    private const string INPUT_SOURCE_CODE =
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

    private const string EXPECTED_MAIN_INTERFACE =
@"// <auto-generated/>
using DesignPatternCodeGenerator.Attributes.AbstractFactory;
using System;

namespace Test.Test
{
    public interface IGearFactory
    {
	    IMouse CreateMouse();
		IMonitor CreateMonitor();
    }
}";

    private const string EXPECTE_CLASSES =
@"// <auto-generated/>
using DesignPatternCodeGenerator.Attributes.AbstractFactory;
using System;

namespace Test.Test
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
}
