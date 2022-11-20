using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace DesignPatternCodeGenerator.Singleton
{
    [Generator]
    public class SingletonGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var singletonAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.Singleton);

            var classGroups = DeclarationsSyntaxGenerator.GetClassGroups(
                context.Compilation,
                context.CancellationToken,
                singletonAttribute);

            foreach (var group in classGroups)
            {
                var classContent = SingletonContentGenerator.GenerateClass(group);

                context.AddSource(
                    $"{BaseNamesGenerator.GetClassName(group, GeneratorAttributeType.Singleton).Replace("Singleton", "")}.g.cs",
                    SourceText.From(classContent, Encoding.UTF8));
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
