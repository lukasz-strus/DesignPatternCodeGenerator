using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Text;

namespace DesignPatternCodeGenerator.NullObject
{
    [Generator]
    public class NullObjectGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var nullObjectAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.NullObject);

            var interfaceGroups = DeclarationsSyntaxGenerator.GetInterfaceGroups(
                context.Compilation,
                context.CancellationToken,
                nullObjectAttribute);

            foreach (var group in interfaceGroups)
            {
                var classContent = NullObjectContentGenerator.GenerateClass(group);

                context.AddSource(
                $"{BaseNamesGenerator.GetClassName(group, GeneratorAttributeType.NullObject, true, true)}.g.cs",
                SourceText.From(classContent, Encoding.UTF8));
            }

        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
