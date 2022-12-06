using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Factory.Compontents
{
    internal static class FactoryClassComponentsGenerator
    {
        internal static string GenerateDeclaration(IGrouping<string, InterfaceDeclarationSyntax> group)
            => $"{BaseNamesGenerator.GetAccesibility(group)} class {BaseNamesGenerator.GetClassName(group, GeneratorAttributeType.Factory)}" +
            $": {BaseNamesGenerator.GetInterfaceName(group, GeneratorAttributeType.Factory)}";

        internal static string GenerateFields(IEnumerable<InterfaceDeclarationSyntax> group)
        {
            var properties = GetProperties(group);

            return $"{string.Join("\n\t\t", properties.Select(p => $"private readonly {p.Type} _{p.Identifier.Text.ToLower()};"))}\n";
        }

        internal static string GenerateConstructor(IEnumerable<InterfaceDeclarationSyntax> group)
        {
            var properties = GetProperties(group);

            return $@"{GenerateConstructorDeclaration(group, properties)}
        {{
	        {GenerateConstructorImplementation(properties)}
        }}";
        }

        internal static string GenerateCreateMethodClass(
            IGrouping<string, InterfaceDeclarationSyntax> group,
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> factoryChildGroups)
            => $"{string.Join("\n", group.Select(g => GenerateCreateMethod(g, factoryChildGroups)))}";

        private static string GenerateCreateMethod(
            InterfaceDeclarationSyntax interfaceSyntax,
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> factoryChildGroups)
        {
            var properties = interfaceSyntax.Members.OfType<PropertyDeclarationSyntax>();
            var enums = factoryChildGroups.Select(x => x.Key);

            return FactoryComponentsGenerator.GenerateCreateMethodDeclaration(interfaceSyntax)
                + GenerateCreateMethodImplementation(enums, interfaceSyntax);
        }

        private static string GenerateCreateMethodImplementation(IEnumerable<string> enums, InterfaceDeclarationSyntax interfaceSyntax)
            => $@"
        {{
            switch (type)
            {{
                {GenerateCases(enums, interfaceSyntax)}
                default :
                    throw new Exception($""Type {{type}} is not handled"");
            }}    
        }}";

        private static string GenerateCases(IEnumerable<string> enums, InterfaceDeclarationSyntax interfaceSyntax)
            => $"{string.Join("\n\t\t\t\t", enums.Select(e => $"case {interfaceSyntax.Identifier.Text.Substring(1)}FactoryType.{e} :\n\t\t\t\t\treturn new {e}({GenerateConstructorParameters(interfaceSyntax)});"))}";

        private static string GenerateConstructorParameters(InterfaceDeclarationSyntax interfaceSyntax)
        {
            var properties = interfaceSyntax.Members.OfType<PropertyDeclarationSyntax>();

            return string.Join(", ", properties.Select(p => GenerateParams(p)));
        }

        private static string GenerateParams(PropertyDeclarationSyntax property)
            => FactoryComponentsGenerator.IsDependency(property) ? $"_{property.Identifier.Text.ToLower()}" : property.Identifier.Text.ToLower();

        private static string GenerateConstructorDeclaration(
            IEnumerable<InterfaceDeclarationSyntax> group,
            IEnumerable<PropertyDeclarationSyntax> properties)
            => $"public {group.First().Identifier.Text.Substring(1)}" +
            $"Factory({string.Join(", ", properties.Select(p => $"{p.Type} {p.Identifier.Text.ToLower()}"))})";

        private static string GenerateConstructorImplementation(IEnumerable<PropertyDeclarationSyntax> properties)
            => $"{string.Join("\n\t\t\t", properties.Select(p => $"_{p.Identifier.Text.ToLower()} = {p.Identifier.Text.ToLower()};"))}";

        private static IEnumerable<PropertyDeclarationSyntax> GetProperties(IEnumerable<InterfaceDeclarationSyntax> group)
            => group.SelectMany(g => g.Members)
            .OfType<PropertyDeclarationSyntax>()
            .Where(FactoryComponentsGenerator.IsDependency)
            .Distinct();

    }
}
