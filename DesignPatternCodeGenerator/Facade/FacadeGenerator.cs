using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Linq;
using System.Text;

namespace DesignPatternCodeGenerator.Facade
{
    [Generator]
    public class FacadeGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var facadeMethodAttribute = AttributeTypeGenerator.CreateGeneratorAttributeType(GeneratorAttributeType.FacadeMethod);

            var methodGroups = DeclarationsSyntaxGenerator.GetMethodGroups(
                context.Compilation,
                context.CancellationToken,
                facadeMethodAttribute);

            methodGroups = methodGroups.GroupByAttribute();

            foreach (var methodGroup in methodGroups)
            {
                GenerateFacadeClass(context, methodGroup);
            }
        }

        private void GenerateFacadeClass(
            GeneratorExecutionContext context,
            IGrouping<string, MethodDeclarationSyntax> methodGroup)
        {
            var hintName = $"{methodGroup.Key}Facade.g.cs";

            var facadeContent = FacadeContentGenerator.GenerateClass(methodGroup);

            context.AddSource(hintName, SourceText.From(facadeContent, Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
