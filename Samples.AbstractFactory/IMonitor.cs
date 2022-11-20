using DesignPatternCodeGenerator.Attributes.AbstractFactory;

namespace Samples.AbstractFactory
{
    [AbstractFactory("Gears")]
    public interface IMonitor
    {
        void On();
        void Off();
    }

    [AbstractFactoryClass("Benq")]
    public class BenqMonitor : IMonitor
    {
        public void On()
        {
            Console.WriteLine("Benq on");
        }

        public void Off()
        {
            Console.WriteLine("Benq off");
        }
    }
}
