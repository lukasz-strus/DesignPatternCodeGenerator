using DesignPatternCodeGenerator.Factory;

namespace Samples.Factory;

public interface IAppleView : IView
{
}

public class AppleView : IAppleView
{
    public AppleView()
    {

    }

    public void Display()
    {
        Console.WriteLine("Display apple view");
    }
}
