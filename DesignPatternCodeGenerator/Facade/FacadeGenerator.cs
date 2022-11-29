using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Text;

namespace DesignPatternCodeGenerator.Facade
{
    [Generator]
    public class FacadeGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var facadeMethodAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.FacadeMethod);

            var methodGroups = DeclarationsSyntaxGenerator.GetMethodGroups(
                context.Compilation,
                context.CancellationToken,
                facadeMethodAttribute);

            var methodGroupsByAttributeText = GroupCollectionHelper.GroupCollectionByAttributeValueText(methodGroups);

            foreach (var group in methodGroupsByAttributeText)
            {
                var facadeContent = FacadeContentGenerator.GenerateClass(group);

                context.AddSource($"{group.Key}Facade.g.cs", SourceText.From(facadeContent, Encoding.UTF8));
            }

        }

        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }
}
