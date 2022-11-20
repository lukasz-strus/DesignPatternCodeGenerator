using Samples.AbstractFactory;

var uiApplication = new Application(new MacFactory());

uiApplication.RenderUI();
