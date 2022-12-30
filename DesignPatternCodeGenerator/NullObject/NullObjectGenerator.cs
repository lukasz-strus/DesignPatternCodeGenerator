using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Linq;
using System.Text;

namespace DesignPatternCodeGenerator.NullObject
{
    [Generator]
    public class NullObjectGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var nullObjectAttribute = AttributeTypeGenerator.CreateGeneratorAttributeType(GeneratorAttributeType.NullObject);

            var interfaceGroups = DeclarationsSyntaxGenerator.GetInterfaceGroups(
                context.Compilation,
                context.CancellationToken,
                nullObjectAttribute);

            foreach (var group in interfaceGroups)
            {
                GenerateNullObject(context, group);
            }
        }

        private static void GenerateNullObject(GeneratorExecutionContext context, IGrouping<string, InterfaceDeclarationSyntax> group)
        {
            var hintName = $"{BaseNamesGenerator.GetClassName(group, GeneratorAttributeType.NullObject)}.g.cs";
            var classContent = NullObjectContentGenerator.GenerateClass(group);

            context.AddSource(hintName, SourceText.From(classContent, Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
