﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace DesignPatternCodeGenerator.Factory
{
    internal static class FactoryContentComponentsGenerator
    {
        internal static string GenerateCreateMethodDeclaration(InterfaceDeclarationSyntax interfaceSyntax)
        {
            var properties = interfaceSyntax.Members.OfType<PropertyDeclarationSyntax>();

            var interfaceName = interfaceSyntax.Identifier.Text;
            var factoryType = $"{interfaceName.Substring(1)}FactoryType type";
            var parameters = $"{string.Join(", ", properties.Where(IsNotDependency).Select(CreateParameter))}";

            return parameters == ""
                ? $"public {interfaceName} Create({factoryType})"
                : $"public {interfaceName} Create({factoryType},{parameters})";
        }

        private static string CreateParameter(PropertyDeclarationSyntax propertySyntax)
            => $"{propertySyntax.Type} {propertySyntax.Identifier.Text.ToLower()}";

        internal static bool IsDependency(MemberDeclarationSyntax memberSyntax)
            => !memberSyntax.AttributeLists.Any(x => x.Attributes.Any(y => y.Name.GetText().ToString().Contains("Parameter")));

        internal static bool IsNotDependency(MemberDeclarationSyntax memberSyntax)
            => !IsDependency(memberSyntax);
    }
}
