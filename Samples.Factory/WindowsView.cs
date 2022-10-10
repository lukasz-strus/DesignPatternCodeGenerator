using DesignPatternCodeGenerator.Factory;

namespace Samples.Factory;

public interface IWindowsView : IView
{
}

public class WindowsView : IWindowsView
{
    [Factory]
    public WindowsView()
    {

    }

    public void Display()
    {
        Console.WriteLine("Display windows view");
    }
}
