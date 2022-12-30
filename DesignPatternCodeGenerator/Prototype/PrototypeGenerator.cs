using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternCodeGenerator.Prototype
{
    [Generator]
    public class PrototypeGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var prototypeAttribute = AttributeTypeGenerator.CreateGeneratorAttributeType(GeneratorAttributeType.Prototype);

            var classGroups = DeclarationsSyntaxGenerator.GetClassGroups(
                context.Compilation,
                context.CancellationToken,
                prototypeAttribute);

            var allClassGroups = DeclarationsSyntaxGenerator.GetAllClassGroups(
                context.Compilation,
                context.CancellationToken);

            foreach (var group in classGroups)
            {
                GeneratePrototyp(context, group, allClassGroups);
            }
        }

        private void GeneratePrototyp(
            GeneratorExecutionContext context,
            IGrouping<string, ClassDeclarationSyntax> group,
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> allClassGroups)
        {
            var hintName = $"{BaseNamesGenerator.GetClassName(group)}.g.cs";
            var classContent = PrototypeContentGenerator.GenerateClass(group, allClassGroups);

            context.AddSource(hintName, SourceText.From(classContent, Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }
}
