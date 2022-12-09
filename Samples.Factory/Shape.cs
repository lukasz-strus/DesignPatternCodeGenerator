using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Attributes.Factory;

namespace Samples.Factory
{
    [Factory]
    public interface IShape
    {
        [Parameter]
        public int X { get; set; }
        [Parameter]
        public int Y { get; set; }
        public int Radius { get; set; }

        public void Display();
    }

    [FactoryProduct]
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

    [FactoryProduct]
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


    [FactoryProduct]
    public class Square : IShape
    {

        public Square(int x, int y, int radius)
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
            Console.WriteLine($"Display Square with: X = {X}, Y = {Y}, Radius = {Radius}");
        }
    }
}
