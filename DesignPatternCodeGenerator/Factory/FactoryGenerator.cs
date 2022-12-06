﻿using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base.CollectionHelper;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatternCodeGenerator.Factory
{
    [Generator]
    public class FactoryGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var factoryAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.Factory);

            var interfaceGroups = DeclarationsSyntaxGenerator.GetInterfaceGroups(
                context.Compilation,
                context.CancellationToken,
                factoryAttribute);

            var factoryChildAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.FactoryProduct);

            var classGroups = DeclarationsSyntaxGenerator.GetClassGroups(
                context.Compilation,
                context.CancellationToken,
                factoryChildAttribute);

            foreach (var group in interfaceGroups)
            {
                var interfaceName = BaseNamesGenerator.GetInterfaceName(group);
                var factoryProductsGroups = FilterCollectionHelper.FilterClassesByInterface(classGroups, interfaceName);

                GenerateEnumFactory(context, group, factoryProductsGroups);

                GenerateInterfaceFactory(context, group);

                GenerateClassFactory(context, group, factoryProductsGroups);
            }
        }

        private void GenerateEnumFactory(
            GeneratorExecutionContext context,
            IGrouping<string, InterfaceDeclarationSyntax> group,
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> factoryProductsGroups)
        {
            var hintName = $"{BaseNamesGenerator.GetClassName(group, GeneratorAttributeType.Factory)}Type.g.cs";
            var sourceTxt = FactoryContentGenerator.GenerateEnum(group, factoryProductsGroups);

            context.AddSource(hintName, SourceText.From(sourceTxt, Encoding.UTF8));
        }

        private void GenerateInterfaceFactory(
            GeneratorExecutionContext context,
            IGrouping<string, InterfaceDeclarationSyntax> group)
        {
            var hintName = $"{BaseNamesGenerator.GetInterfaceName(group, GeneratorAttributeType.Factory)}.g.cs";
            var sourceTxt = FactoryContentGenerator.GenerateInterface(group);

            context.AddSource(hintName, SourceText.From(sourceTxt, Encoding.UTF8));
        }

        private void GenerateClassFactory(
            GeneratorExecutionContext context,
            IGrouping<string, InterfaceDeclarationSyntax> group,
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> factoryProductsGroups)
        {
            var hintName = $"{BaseNamesGenerator.GetClassName(group, GeneratorAttributeType.Factory)}.g.cs";
            var sourceTxt = FactoryContentGenerator.GenerateClass(group, factoryProductsGroups);

            context.AddSource(hintName, SourceText.From(sourceTxt, Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
