using DesignPatternCodeGenerator.Attributes.AbstractFactory;

namespace Samples.AbstractFactory;

[AbstractFactory("UIElement")]
public interface ITextBox
{
    void Render();
    void HandleInput();
}

[AbstractFactoryChild("Windows")]
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

[AbstractFactoryChild("Mac")]
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
