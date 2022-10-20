using Samples.Factory;

Console.WriteLine("Test Fabryki");

var shapeFactoryY10 = new ShapeFactory(100);

var circle = shapeFactoryY10.Create(ShapeFactoryType.Circle,10, 10);
var triangle = shapeFactoryY10.Create(ShapeFactoryType.Triangle, 20, 20);

circle.Display();
triangle.Display();
