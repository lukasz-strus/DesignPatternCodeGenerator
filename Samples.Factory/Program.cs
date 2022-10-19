using Samples.Factory;

Console.WriteLine("Test Fabryki");

var shapeFactoryY10 = new ShapeFactory(10, 10);

var circle = shapeFactoryY10.Create(ShapeFactoryType.Circle,100);
var triangle = shapeFactoryY10.Create(ShapeFactoryType.Triangle,100);

circle.Display();
triangle.Display();
