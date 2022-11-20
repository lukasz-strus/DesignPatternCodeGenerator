using DesignPatternCodeGenerator.Attributes.AbstractFactory;

namespace Samples.AbstractFactory;

[AbstractFactory("UIElement")]
public interface ITextBox
{
    void Render();
    void HandleInput();
}

[AbstractFactoryClass("Windows")]
public class WindowsTextBox : ITextBox
{
    public void HandleInput()
    {
        Console.WriteLine("Handle windows text input");
    }

    public void Render()
    {
        Console.WriteLine("Render windows textbox");
    }
}

[AbstractFactoryClass("Mac")]
public class MacTextBox : ITextBox
{
    public void HandleInput()
    {
        Console.WriteLine("Handle mac text input");
    }

    public void Render()
    {
        Console.WriteLine("Render mac textbox");
    }
}
