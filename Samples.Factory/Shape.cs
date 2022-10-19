using DesignPatternCodeGenerator.Attributes.Factory;
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
        public int X { get; set; }
        public int Y { get; set; }
        [Property]
        public int Radius { get; set; }

        public void Display();
    }

    [FactoryChild(typeof(IShape))]
    public class Circle : IShape
    {
        
        public Circle(int x, int y, int radius)
        {
            X = x;
            Y = y;
            Radius = radius;

        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }

        public void Display()
        {
            Console.WriteLine($"Display circle with: X = {X}, Y = {Y}, Radius = {Radius}");
        }
    }

    [FactoryChild(typeof(IShape))]
    public class Triangle : IShape
    {

        public Triangle(int x, int y, int radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Radius { get; set; }

        public void Display()
        {
            Console.WriteLine($"Display triangle with: X = {X}, Y = {Y}, Radius = {Radius}");
        }
    }
}
