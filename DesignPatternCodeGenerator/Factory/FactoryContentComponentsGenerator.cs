using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Factory
{
    internal static class FactoryContentComponentsGenerator
    {
        internal static string GenerateCreateMethodClass(
            IGrouping<string, TypeDeclarationSyntax> group,
            IEnumerable<IGrouping<string, TypeDeclarationSyntax>> factoryChildGroups)
            => $"{string.Join("\n", group.Select(g => GenerateCreateMethod(g, factoryChildGroups)))}";

        internal static string GenerateCreateMethodInterface(IGrouping<string, TypeDeclarationSyntax> group)
            => $"{string.Join("\n", group.Select(GenerateCreateMethodDeclaration).Select(x => x + ";"))}";

        internal static string GenerateFields(IEnumerable<TypeDeclarationSyntax> group)
        {
            var properties = GetProperties(group);

            return $"{string.Join("\n\t\t", properties.Select(p => $"private readonly {p.Type} _{(p.Identifier.Text).ToLower()};"))}\n";
        }

        internal static string GenerateConstructor(IEnumerable<TypeDeclarationSyntax> group)
        {
            var properties = GetProperties(group);

            return $@"{GenerateConstructorDeclaration(group, properties)}
        {{
	        {GenerateConstructorImplementation(properties)}
        }}";
        }

        private static string GenerateCreateMethod(
            TypeDeclarationSyntax interfaceSyntax,
            IEnumerable<IGrouping<string, TypeDeclarationSyntax>> factoryChildGroups)
        {
            var properties = interfaceSyntax.Members.OfType<PropertyDeclarationSyntax>();
            var enums = factoryChildGroups.Select(x => x.Key);

            return GenerateCreateMethodDeclaration(interfaceSyntax) 
                + GenerateCreateMethodImplementation(enums, interfaceSyntax);
        }

        private static string GenerateCreateMethodDeclaration(TypeDeclarationSyntax interfaceSyntax)
        {
            var properties = interfaceSyntax.Members.OfType<PropertyDeclarationSyntax>();

            var factoryType = GenerateFactoryType(interfaceSyntax);
            var parameters = GenerateMethodParameters(properties);

            return parameters != ""
                ? $"public {interfaceSyntax.Identifier.Text} Create({factoryType},{parameters})"
                : $"public {interfaceSyntax.Identifier.Text} Create({factoryType})";
        }

        private static string GenerateFactoryType(TypeDeclarationSyntax interfaceSyntax)
            => $"{interfaceSyntax.Identifier.Text.Substring(1)}FactoryType type";

        private static string GenerateMethodParameters(IEnumerable<PropertyDeclarationSyntax> properties)
            => $"{string.Join(", ", properties.Where(IsNotDependency).Select(CreateParameter))}";

        private static string CreateParameter(PropertyDeclarationSyntax propertySyntax)
            => $"{propertySyntax.Type} {propertySyntax.Identifier.Text.ToString().Replace("<", "_").Replace(">", "_").ToLower()}";

        private static string GenerateCreateMethodImplementation(
                    IEnumerable<string> enums,
            TypeDeclarationSyntax interfaceSyntax)
            => $@"
        {{
            switch (type)
            {{
                {GenerateCases(enums, interfaceSyntax)}
                default :
                    throw new Exception($""Type {{type}} is not handled"");
            }}    
        }}";

        private static string GenerateCases(
            IEnumerable<string> enums,
            TypeDeclarationSyntax interfaceSyntax)
            => $"{string.Join("\n\t\t\t\t", enums.Select(e => $"case {interfaceSyntax.Identifier.Text.Substring(1)}FactoryType.{e} :\n\t\t\t\t\treturn new {e}({GenerateConstructorParameters(interfaceSyntax)});"))}";

        private static string GenerateConstructorParameters(TypeDeclarationSyntax interfaceSyntax)
        {
            var properties = interfaceSyntax.Members.OfType<PropertyDeclarationSyntax>();

            return string.Join(", ", properties.Select(p => GenerateParams(p)));
        }

        private static string GenerateParams(PropertyDeclarationSyntax property)
            => IsDependency(property)
                ? $"_{property.Identifier.Text.Replace("<", "_").Replace(">", "_").ToLower()}"
                : property.Identifier.Text.Replace("<", "_").Replace(">", "_").ToLower();

        private static IEnumerable<PropertyDeclarationSyntax> GetProperties(IEnumerable<TypeDeclarationSyntax> group)
            => group.SelectMany(g => g.Members).OfType<PropertyDeclarationSyntax>().Where(IsDependency).Distinct();

        private static string GenerateConstructorDeclaration(
            IEnumerable<TypeDeclarationSyntax> group,
            IEnumerable<PropertyDeclarationSyntax> properties)
            => $"public {(group.First().Identifier.Text).Substring(1)}Factory({string.Join(", ", properties.Select(p => $"{p.Type} {p.Identifier.Text.Replace("<", "_").Replace(">", "_").ToLower()}"))})";

        private static string GenerateConstructorImplementation(
            IEnumerable<PropertyDeclarationSyntax> properties)
            => $"{string.Join("\n\t\t\t", properties.Select(p => $"_{p.Identifier.Text.Replace("<", "_").Replace(">", "_").ToLower()} = {p.Identifier.Text.Replace("<", "_").Replace(">", "_").ToLower()};"))}";

        private static bool IsDependency(MemberDeclarationSyntax memberSyntax)
            => !memberSyntax.AttributeLists.Any(x => x.Attributes.Any(y => y.Name.GetText().ToString().Contains("Parameter")));

        private static bool IsNotDependency(MemberDeclarationSyntax memberSyntax)
            => !IsDependency(memberSyntax);
    }
}
