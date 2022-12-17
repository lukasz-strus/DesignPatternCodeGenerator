using DesignPatternCodeGenerator.Attributes.ContainerIoC;

namespace Samples.ContainerIoC;

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

[Container("AddViewModels", ObjectLifeTime.Transient, new string[] { "IViewModel4" })]
public class ViewModel : IViewModel1, IViewModel3, IViewModel4
{
}

[Container("AddViewModels", ObjectLifeTime.Scoped, new string[] { "IViewModel4", "IViewModel1" })]
public class MainViewModelExtension : MainViewModel
{
}
