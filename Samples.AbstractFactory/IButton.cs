using DesignPatternCodeGenerator.Attributes.AbstractFactory;

namespace Samples.AbstractFactory;

[AbstractFactory("UIElement")]
public interface IButton
{
    void Render();
    void HandleClick();
}

[AbstractFactoryClass("Windows")]
public class WindowsButton : IButton
{
    public void HandleClick()
    {
        Console.WriteLine("Handle windows click event");
    }

    public void Render()
    {
        Console.WriteLine("Render windows button");
    }
}

[AbstractFactoryClass("Mac")]
public class MacButton : IButton
{
    public void HandleClick()
    {
        Console.WriteLine("Handle mac click event");
    }

    public void Render()
    {
        Console.WriteLine("Render mac button");
    }
}
