using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Attributes.Singleton;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Singleton;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Text;

namespace DesignPatternCodeGenerator.Prototype
{
    [Generator]
    public class PrototypeGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var prototypeAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.Prototype);

            var classGroups = DeclarationsSyntaxGenerator.GetClassGroups(
                context.Compilation,
                context.CancellationToken,
                prototypeAttribute);

            var allClassGroups = DeclarationsSyntaxGenerator.GetAllClassGroups(
                context.Compilation,
                context.CancellationToken);

            foreach (var group in classGroups)
            {
                var classContent = PrototypeContentGenerator.GenerateClass(group, allClassGroups);

                context.AddSource(
                    $"{BaseNamesGenerator.GetClassName(group, GeneratorAttributeType.Prototype).Replace("Prototype", "")}.g.cs",
                    SourceText.From(classContent, Encoding.UTF8));
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {            
        }
    }
}
