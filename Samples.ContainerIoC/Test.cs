using Samples.ContainerIoC;

namespace Samples.ContainerIoC;

interface ITest
{ 

}

interface ITest2
{
    public int test123 { get; set; }
}

interface ITest22 : ITest2
{
    public int test123 { get; set; }
    public int test124 { get; set; }

}

// olać interfejsy z System

//[IoC("")]
public class Test : ITest, ITest22
{
    public int test123 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public int test124 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
