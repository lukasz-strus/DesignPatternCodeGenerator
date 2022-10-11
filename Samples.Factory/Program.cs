using Samples.Factory;

var appleFactory = new AppleViewFactory();
var windowFactory = new WindowsViewFactory();

var appleView = appleFactory.Create();
var windowView = windowFactory.Create();

appleView.Display();
windowView.Display();

var shapeFactory = new ShapeFactory(10, 10);

var shape = shapeFactory.Create();

shape.Display();
