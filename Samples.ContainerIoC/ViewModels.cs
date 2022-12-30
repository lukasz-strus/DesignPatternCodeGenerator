using DesignPatternCodeGenerator.Attributes.IoCContainer;

namespace Samples.IoCContainer;

interface IViewModel1
{
}

interface IViewModel2
{
}

interface IViewModel3 : IViewModel2
{
}

interface IViewModel4
{
}

[Container("AddViewModels", ObjectLifeTime.Singleton, new string[] { "IViewModel4" })]
public class MainViewModel : IViewModel1, IViewModel3, IViewModel4, IDisposable
{
    public void Dispose()
    {
    }
}

//[Container("AddViewModels", ObjectLifeTime.Transient)]
//public class ViewModel : IViewModel1
//{
//}

//[Container("AddViewModels", ObjectLifeTime.Scoped, new string[] { "IViewModel4", "IViewModel1" })]
//public class MainViewModelExtension : MainViewModel
//{
//}
