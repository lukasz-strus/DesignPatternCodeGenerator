using DesignPatternCodeGenerator.Base.Enums;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Base.Generators
{
    internal static class BaseNamesGenerator
    {
        internal static string GetAccesibility(IGrouping<string, MemberDeclarationSyntax> group)
            => group.First().FirstAncestorOrSelf<MemberDeclarationSyntax>().Modifiers.First().Text;

        internal static string GetAccesibility(MemberDeclarationSyntax memberDeclarationSyntax)
            => memberDeclarationSyntax.FirstAncestorOrSelf<MemberDeclarationSyntax>().Modifiers.First().Text;

        internal static string GetNamespace(IGrouping<string, TypeDeclarationSyntax> group)
            => group.First().FirstAncestorOrSelf<NamespaceDeclarationSyntax>()?.Name?.ToString() ??
               group.First().FirstAncestorOrSelf<FileScopedNamespaceDeclarationSyntax>().Name.ToString();

        internal static string GetNamespace(IGrouping<string, MethodDeclarationSyntax> group)
            => group.First().FirstAncestorOrSelf<NamespaceDeclarationSyntax>()?.Name?.ToString() ??
               group.First().FirstAncestorOrSelf<FileScopedNamespaceDeclarationSyntax>().Name.ToString();

        internal static IEnumerable<string> GetUsings(IGrouping<string, TypeDeclarationSyntax> group)
            => group.First()
                 .FirstAncestorOrSelf<CompilationUnitSyntax>()
                 .DescendantNodesAndSelf()
                 .OfType<UsingDirectiveSyntax>()
                 .Select(x => x.Name.ToString());

        internal static IEnumerable<string> GetUsings(IGrouping<string, MethodDeclarationSyntax> group)
            => group.First()
                 .FirstAncestorOrSelf<CompilationUnitSyntax>()
                 .DescendantNodesAndSelf()
                 .OfType<UsingDirectiveSyntax>()
                 .Select(x => x.Name.ToString());

        internal static string GetClassName(
            IGrouping<string, TypeDeclarationSyntax> group,
            GeneratorAttributeType generatorType,
            bool isDesignPatternPostfix = false,
            bool isMainAttributeOnInterface = false)
        {
            string className = isDesignPatternPostfix ? group.Key + generatorType.ToString() : group.Key;

            if (isMainAttributeOnInterface)
                className = className.Substring(1);

            return className;
        }

        internal static string GetClassName(IGrouping<string, ClassDeclarationSyntax> group) 
            => group.Key;

        internal static string GetClassName(IGrouping<string, MethodDeclarationSyntax> group)
             => group.Key;

        internal static string GetClassName(IGrouping<string, InterfaceDeclarationSyntax> group)
             => group.Key.Substring(1);

        internal static string GetClassName(IGrouping<string, ClassDeclarationSyntax> group, GeneratorAttributeType generatorType)
            => group.Key + generatorType.ToString();

        internal static string GetClassName(IGrouping<string, InterfaceDeclarationSyntax> group, GeneratorAttributeType generatorType)
            => group.Key.Substring(1) + generatorType.ToString();

        internal static string GetInterfaceName(IGrouping<string, TypeDeclarationSyntax> group, GeneratorAttributeType generatorType)
        { 
            var baseInterfaceName = group.Key + generatorType.ToString();

            return baseInterfaceName.StartsWith("I") ? baseInterfaceName : baseInterfaceName.Insert(0, "I");
        }

        internal static string GetInterfaceName(IGrouping<string, TypeDeclarationSyntax> group)
            => group.Key.StartsWith("I") ? group.Key : group.Key.Insert(0, "I");

    }

}
