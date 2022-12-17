using DesignPatternCodeGenerator.Attributes.ContainerIoC;

namespace Samples.ContainerIoC;

interface ITest1
{

}

interface ITest2 : ITest3
{
}

interface ITest3
{

}

interface ITest123
{

}

[Container("AddViewModels", ObjectLifeTime.Singleton, new string[] { "ITest123", "ITest1" })]
public class Test : ITest1, ITest2, ITest123, IDisposable
{
    public void Dispose()
    {

    }
}

[Container("AddViewModels", ObjectLifeTime.Transient, new string[] { "ITest123" })]
public class Test10 : ITest1, ITest2, ITest123
{
}

