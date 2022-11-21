using DesignPatternCodeGenerator.Attributes.AbstractFactory;
using Samples.AbstractFactory;

namespace Samples.AbstractFactory 
{
    [AbstractFactory("UIElement")]
    public interface ITextBox
    {
        void Render();
        void HandleInput();
    }
}

namespace Samples.UIWindows
{
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
}

namespace Samples.UIMac
{
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
}


