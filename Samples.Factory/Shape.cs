using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base;
using DesignPatternCodeGenerator.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samples.Factory
{
    [Factory]
    public interface IShape
    {
        [Property]
        public int X { get; set; }
        public int Y { get; set; }

        public void Display();
    }

    public class Shape : IShape
    {
        
        public Shape(int x, int y)
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
