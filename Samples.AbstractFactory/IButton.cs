using DesignPatternCodeGenerator.Attributes.AbstractFactory;

namespace Samples.AbstractFactory;

[AbstractFactory("UIElement")]
public interface IButton
{
    void Render();
    void HandleClick();
}

[AbstractFactoryChild("Windows")]
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

[AbstractFactoryChild("Mac")]
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
