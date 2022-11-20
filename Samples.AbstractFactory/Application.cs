namespace Samples.AbstractFactory;

public class Application
{
    private IUIElementFactory _elementFactory;

    public Application(IUIElementFactory elementFactory)
    {
        _elementFactory = elementFactory;
    }

    public void RenderUI()
    {
        var createNewFileButton = _elementFactory.CreateButton();

        createNewFileButton.Render();

        var textBox = _elementFactory.CreateTextBox();

        textBox.Render();
    }
}
