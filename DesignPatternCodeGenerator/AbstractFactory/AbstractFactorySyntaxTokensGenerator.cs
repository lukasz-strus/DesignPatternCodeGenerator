using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Base.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace DesignPatternCodeGenerator.AbstractFactory
{
    internal static class AbstractFactorySyntaxTokensGenerator
    {
        internal static SyntaxTokens AbstractFactoryGenerateSyntaxTokens(
            IGrouping<string, InterfaceDeclarationSyntax> interfaceGroup,
            IGrouping<string, ClassDeclarationSyntax> classGroup,
            GeneratorAttributeType generatorType,
            SyntaxTokensConfigurations configurations)
        {
            SyntaxTokens syntaxTokens = SyntaxTokensGenerator.GenerateSyntaxTokens(interfaceGroup, generatorType, configurations);

            syntaxTokens.ClassName = SetClassName(classGroup);
            syntaxTokens.InterfaceName = SetInterfaceName(interfaceGroup);

            return syntaxTokens;
        }

        private static string SetClassName(IGrouping<string, ClassDeclarationSyntax> group) 
            => $"{GetFactoryName(group)}Factory";

        private static string SetInterfaceName(IGrouping<string, InterfaceDeclarationSyntax> group) 
            => $"I{GetFactoryName(group)}Factory";

        private static string GetFactoryName(IGrouping<string, TypeDeclarationSyntax> group)
        {
            var attributeLists = group.First().AttributeLists;

            var attributes = attributeLists.First().Attributes;

            var arguments = attributes.First().ArgumentList.Arguments;

            var expression = arguments.First().Expression;

            return expression.GetFirstToken().ValueText;
        }


    }
}
