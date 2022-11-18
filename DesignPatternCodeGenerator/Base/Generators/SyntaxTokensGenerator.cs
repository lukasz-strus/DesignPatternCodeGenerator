using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Base.Generators
{
    internal static class SyntaxTokensGenerator
    {
        internal static SyntaxTokens GenerateSyntaxTokens(
            IGrouping<string, TypeDeclarationSyntax> group,
            GeneratorAttributeType generatorType,
            SyntaxTokensConfigurations configurations) => 
            new SyntaxTokens()
            {
                Accessibility = SetAccesibility(group),
                Namespace = SetNamespace(group),
                Usings = SetUsings(group),
                ClassName = SetClassName(group, generatorType, configurations),
                InterfaceName = SetInterfaceName(group, generatorType, configurations),
                AdditionalClassToken = SetAdditionalClassToken(configurations)
            };

        private static string SetAccesibility(IGrouping<string, TypeDeclarationSyntax> group)
            => group.First().FirstAncestorOrSelf<TypeDeclarationSyntax>().Modifiers.First().Text;

        private static string SetNamespace(IGrouping<string, TypeDeclarationSyntax> group)
            => group.First().FirstAncestorOrSelf<NamespaceDeclarationSyntax>()?.Name?.ToString() ??
               group.First().FirstAncestorOrSelf<FileScopedNamespaceDeclarationSyntax>().Name.ToString();

        private static IEnumerable<string> SetUsings(IGrouping<string, TypeDeclarationSyntax> group)
            => group.First()
                 .FirstAncestorOrSelf<CompilationUnitSyntax>()
                 .DescendantNodesAndSelf()
                 .OfType<UsingDirectiveSyntax>()
                 .Select(x => x.Name.ToString());

        private static string SetClassName(
            IGrouping<string, TypeDeclarationSyntax> group,
            GeneratorAttributeType generatorType,
            SyntaxTokensConfigurations configurations)
        {
            string className = configurations.IsDesignPatternPostfix 
                ? group.Key + generatorType.ToString() 
                : group.Key;

            if (configurations.IsMainAttributeOnInterface)
                className = className.Substring(1);

            return className;
        }

        private static string SetInterfaceName(
            IGrouping<string, TypeDeclarationSyntax> group,
            GeneratorAttributeType generatorType,
            SyntaxTokensConfigurations configurations)
                => configurations.IsDesignPatternPostfix
                ? group.Key + generatorType.ToString()
                : group.Key;

        private static string SetAdditionalClassToken(SyntaxTokensConfigurations configurations)
                => configurations.IsPartialClass ? " partial" : "";


    }

}
