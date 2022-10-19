using DesignPatternCodeGenerator.Factory;

namespace Samples.Factory;

public interface IWindowsView : IView
{
}

public class WindowsView : IWindowsView
{
    public WindowsView()
    {

    }

    public void Display()
    {
        Console.WriteLine("Display windows view");
    }
}
