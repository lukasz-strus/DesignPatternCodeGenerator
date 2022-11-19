using DesignPatternCodeGenerator.Attributes.AbstractFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.AbstractFactory
{
    [AbstractFactory("UIGears")]
    public interface IMonitor
    {
        void On();
        void Off();
    }

    [AbstractFactoryChild("Benq")]
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
