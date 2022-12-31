namespace DesignPatternCodeGenerator.PerformanceTests
{
    public class ClassGenerator
    {
        public static void GenerateClasses(int classCount)
        {
            var classContent = $@"using DesignPatternCodeGenerator.Attributes.Factory;

namespace DesignPatternCodeGenerator.PerformanceTests;
";

            for (int i = 1; i <= classCount; i++)
            {
                var classDeclaration = $@"
[Factory]
public interface ICar{i}
{{
    [Parameter]
    public string Name {{ get; set; }}        
    public int HorsePower {{ get; set; }}

}}

[FactoryProduct]
class Bmw{i} : ICar{i}
{{
    public string Name {{ get; set; }}
    public int HorsePower {{ get; set; }}

    public Bmw{i}(string name, int horsePower)
    {{
        Name = name;
        HorsePower = horsePower;
    }}
}}

[FactoryProduct]
partial class Audi{i} : ICar{i}
{{
    public string Name {{ get; set; }}
    public int HorsePower {{ get; set; }}

    public Audi{i}(string name, int horsePower)
    {{
        Name = name;
        HorsePower = horsePower;
    }}
}}

";
                classContent += classDeclaration;
            }

            var filepath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent + @"\Classes.cs";

            File.WriteAllText(filepath, classContent);
        }
    }

}
