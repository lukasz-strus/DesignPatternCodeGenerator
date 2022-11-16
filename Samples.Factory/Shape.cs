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

    [FactoryChild]
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

    [FactoryChild]
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


    [FactoryChild]
    public class Kwadrat : IShape
    {

        public Kwadrat(int x, int y, int radius)
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
            Console.WriteLine($"Display Kwadrat with: X = {X}, Y = {Y}, Radius = {Radius}");
        }
    }
}
