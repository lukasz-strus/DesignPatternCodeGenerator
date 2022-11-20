using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Attributes.Factory;

namespace Samples.Factory;

[Factory]
public interface ICar
{
    [Parameter]
    public string Name { get; set; }        
    public int HorsePower { get; set; }

}

[FactoryProduct]
class Bmw : ICar
{
    public string Name { get; set; }
    public int HorsePower { get; set; }

    public Bmw(string name, int horsePower)
    {
        Name = name;
        HorsePower = horsePower;
    }
}

[FactoryProduct]
partial class Audi : ICar
{
    public string Name { get; set; }
    public int HorsePower { get; set; }

    public Audi(string name, int horsePower)
    {
        Name = name;
        HorsePower = horsePower;
    }
}


