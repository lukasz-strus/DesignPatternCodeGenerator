using DesignPatternCodeGenerator.Base;
using DesignPatternCodeGenerator.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Factory
{
    public interface IShape
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void Display();
    }

    public class Shape : IShape
    {
        [Factory]
        public Shape([Parameter] int x, [Parameter] int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }  

        public void Display()
        {
            Console.WriteLine($"Display shape with: X = {X}, Y = {Y}");
        }
    }
}
