using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace DesignPatternCodeGenerator.AbstractFactory
{
    [Generator]
    public class AbstractFactoryGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {

            var factoryAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.AbstractFactory);
            var factoryChildAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.AbstractFactoryChild);

            var mainInterfaceGroups = DeclarationsSyntaxGenerator.GetInterfaceGroups(
                context.Compilation,
                context.CancellationToken,
                factoryAttribute);

            var groupedMainInterfaceGroups = GroupCollectionHelper.GroupCollectionByAttributeValueText(mainInterfaceGroups);

            var classGroups = DeclarationsSyntaxGenerator.GetClassGroups(
                context.Compilation,
                context.CancellationToken,
                factoryChildAttribute);

            foreach (var mainInterfaceGroup in groupedMainInterfaceGroups)
            {
                var interfaceGroups = GroupCollectionHelper.GroupByIdentifierText(mainInterfaceGroup);

                var mainInterfaceContent = MainInterfaceAbstractFactoryContentGenerator.GenerateMainInterface(
                    mainInterfaceGroup,
                    interfaceGroups);

                context.AddSource(
                    $"{BaseNamesGenerator.GetInterfaceName(mainInterfaceGroup, GeneratorAttributeType.Factory, true)}.g.cs",
                    SourceText.From(mainInterfaceContent, Encoding.UTF8));
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }
}
