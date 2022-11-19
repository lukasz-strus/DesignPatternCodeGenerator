using DesignPatternCodeGenerator.Attributes;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DesignPatternCodeGenerator.AbstractFactory
{
    [Generator]
    public class AbstractFactoryGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {

            var factoryAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.AbstractFactory);
            var factoryChildAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.AbstractFactoryChild);

            var interfaceGroups = DeclarationsSyntaxGenerator.GetInterfaceGroups(
                context.Compilation,
                context.CancellationToken,
                factoryAttribute);

            var classGroups = DeclarationsSyntaxGenerator.GetClassGroups(
                context.Compilation,
                context.CancellationToken,
                factoryChildAttribute);

            foreach (var interfaceGroup in interfaceGroups)
            {
                foreach (var classGroup in classGroups)
                {
#if DEBUG
                    if (!Debugger.IsAttached)
                    {
                        Debugger.Launch();
                    }
#endif


                    //var syntaxTokens = AbstractFactorySyntaxTokensGenerator.AbstractFactoryGenerateSyntaxTokens(
                    //    interfaceGroup,
                    //    classGroup,
                    //    GeneratorAttributeType.Factory,
                    //    configuration);

                    //syntaxTokensList.Add(syntaxTokens);

                }
            }

            Console.WriteLine();
        }

        private bool IsAbstractFactoryClass(
            IGrouping<string, InterfaceDeclarationSyntax> interfaceGroup,
            IGrouping<string, ClassDeclarationSyntax> classGroup)
        {
            bool ret = false;

            return ret;
        }



        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }
}
