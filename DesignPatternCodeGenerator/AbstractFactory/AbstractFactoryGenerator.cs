using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Linq;
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

                var mainInterfaceContent = AbstractFactoryContentGenerator.GenerateMainInterface(
                    mainInterfaceGroup,
                    interfaceGroups);

                context.AddSource(
                    $"{BaseNamesGenerator.GetInterfaceName(mainInterfaceGroup, GeneratorAttributeType.Factory, true)}.g.cs",
                    SourceText.From(mainInterfaceContent, Encoding.UTF8));

                var filtredClassGroups = FilterCollectionHelper.FilterClassesByInterface(
                    classGroups,
                    mainInterfaceGroup.Select(x => x.Identifier.Text));

                var groupedClassGroups = GroupCollectionHelper.GroupCollectionByAttributeValueText(filtredClassGroups);

                foreach (var classGroup in groupedClassGroups)
                {
                    var factoryClassContent = AbstractFactoryContentGenerator.GenerateFactoryClass(
                            mainInterfaceGroup,
                            classGroup);

                    context.AddSource(
                        $"{classGroup.Key}Factory.g.cs",
                        SourceText.From(factoryClassContent, Encoding.UTF8));
                }
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }
}
