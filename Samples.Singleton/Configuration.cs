using DesignPatternCodeGenerator.Attributes.Singleton;

namespace Samples.Singleton;

[Singleton]
public partial class Configuration
{
    public string SomeProperty { get; set; }
}
