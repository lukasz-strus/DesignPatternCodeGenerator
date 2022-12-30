using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Linq;
using System.Text;

namespace DesignPatternCodeGenerator.Singleton
{
    [Generator]
    public class SingletonGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var singletonAttribute = AttributeTypeGenerator.CreateGeneratorAttributeType(GeneratorAttributeType.Singleton);

            var classGroups = DeclarationsSyntaxGenerator.GetClassGroups(
                context.Compilation,
                context.CancellationToken,
                singletonAttribute);

            foreach (var group in classGroups)
            {
                GenerateSingleton(context, group);
            }
        }

        private void GenerateSingleton(
            GeneratorExecutionContext context,
            IGrouping<string, ClassDeclarationSyntax> group)
        {
            var hintName = $"{BaseNamesGenerator.GetClassName(group)}.g.cs";
            var classContent = SingletonContentGenerator.GenerateClass(group);

            context.AddSource(hintName, SourceText.From(classContent, Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
