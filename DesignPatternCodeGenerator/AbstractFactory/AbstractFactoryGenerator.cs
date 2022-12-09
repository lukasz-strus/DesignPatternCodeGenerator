using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
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

            var mainInterfaceGroups = DeclarationsSyntaxGenerator.GetInterfaceGroups(
                context.Compilation,
                context.CancellationToken,
                factoryAttribute);

            var interfaceGroups = mainInterfaceGroups.GroupByAttribute();

            var factoryChildAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.AbstractFactoryClass);

            var classGroups = DeclarationsSyntaxGenerator.GetClassGroups(
                context.Compilation,
                context.CancellationToken,
                factoryChildAttribute);

            foreach (var interfaceGroup in interfaceGroups)
            {
                GenerateAbstractFactoryInterface(context, interfaceGroup);

                var factoryClassGroups = GetFactoryClassGroups(interfaceGroup, classGroups);

                foreach (var classGroup in factoryClassGroups)
                {
                    GenerateFactoryClass(context, interfaceGroup, classGroup);
                }
            }
        }

        private void GenerateAbstractFactoryInterface(
            GeneratorExecutionContext context,
            IGrouping<string, InterfaceDeclarationSyntax> interfaceGroup)
        {
            var hintName = $"{BaseNamesGenerator.GetInterfaceName(interfaceGroup, GeneratorAttributeType.Factory)}.g.cs";

            var interfaceGroupsByIdentifier = interfaceGroup.GroupByIdentifier();

            var mainInterfaceContent = AbstractFactoryContentGenerator.GenerateMainInterface(
                interfaceGroup,
                interfaceGroupsByIdentifier);

            context.AddSource(hintName, SourceText.From(mainInterfaceContent, Encoding.UTF8));
        }

        private IEnumerable<IGrouping<string, ClassDeclarationSyntax>> GetFactoryClassGroups(
            IGrouping<string, InterfaceDeclarationSyntax> interfaceGroup,
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> classGroups)
        {
            var interfaceNames = interfaceGroup.Select(x => x.Identifier.Text);
            var filtredClassGroups = classGroups.FilterByInterface(interfaceNames);

            return filtredClassGroups.GroupByAttribute();
        }

        private void GenerateFactoryClass(
            GeneratorExecutionContext context,
            IGrouping<string, InterfaceDeclarationSyntax> interfaceGroup,
            IGrouping<string, ClassDeclarationSyntax> classGroup)
        {
            var hintName = $"{BaseNamesGenerator.GetClassName(classGroup, GeneratorAttributeType.Factory)}.g.cs";

            var factoryClassContent = AbstractFactoryContentGenerator.GenerateFactoryClass(
                interfaceGroup,
                classGroup);

            context.AddSource(hintName, SourceText.From(factoryClassContent, Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
