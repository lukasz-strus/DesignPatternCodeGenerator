using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Diagnostics;
using System.Text;

namespace DesignPatternCodeGenerator.Factory
{
    [Generator]
    public class FactoryGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var factoryAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.Factory);
            var factoryChildAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.FactoryProduct);

            var interfaceGroups = DeclarationsSyntaxGenerator.GetInterfaceGroups(
                context.Compilation,
                context.CancellationToken,
                factoryAttribute);

            var classGroups = DeclarationsSyntaxGenerator.GetClassGroups(
                context.Compilation,
                context.CancellationToken,
                factoryChildAttribute);

            foreach (var group in interfaceGroups)
            {
                var factoryProductsGroups = FilterCollectionHelper.FilterClassesByInterface(
                    classGroups,
                    BaseNamesGenerator.GetInterfaceName(group, GeneratorAttributeType.Factory));

                var enumContent = FactoryEnumGenerator.GenerateEnum(group, factoryProductsGroups);
                context.AddSource(
                    $"{BaseNamesGenerator.GetClassName(group, GeneratorAttributeType.Factory, true, true)}Type.g.cs",
                    SourceText.From(enumContent, Encoding.UTF8));

                var interfaceContent = FactoryContentGenerator.GenerateInterface(group);
                context.AddSource(
                    $"{BaseNamesGenerator.GetInterfaceName(group, GeneratorAttributeType.Factory, true)}.g.cs",
                    SourceText.From(interfaceContent, Encoding.UTF8));

                var classContent = FactoryContentGenerator.GenerateClass(group, factoryProductsGroups);
                context.AddSource(
                    $"{BaseNamesGenerator.GetClassName(group, GeneratorAttributeType.Factory, true, true)}.g.cs",
                    SourceText.From(classContent, Encoding.UTF8));
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
