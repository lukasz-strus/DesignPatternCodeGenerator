using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternCodeGenerator.Factory
{
    internal static class FactoryContentGenerator
    {
        internal static string GenerateInterface(BaseCodeGenerator codeGenerator, IGrouping<string, InterfaceDeclarationSyntax> group)
        {
            return codeGenerator.GenerateUsingsAndNamespace() +
$@"
{{
    {codeGenerator.GenerateDeclaration(CodeType.Interface)}
    {{
	    {string.Join("\n", group.Select(GenerateCreateMethodDeclaration).Select(x => x + ";"))}
    }}
}}";
        }
        internal static string GenerateClass
            (BaseCodeGenerator codeGenerator, 
            IGrouping<string, InterfaceDeclarationSyntax> group,
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> factoryChildGroups)
        {
            return codeGenerator.GenerateUsingsAndNamespace() +
$@"
{{
    {codeGenerator.GenerateDeclaration(CodeType.Class)}
    {{
	    {GenerateFieldsAndConstructor(group)}

	    {string.Join("\n", group.Select(g => GenerateCreateMethod(g, factoryChildGroups)))}
    }}
}}";
        }

        private static string GenerateFieldsAndConstructor(IEnumerable<InterfaceDeclarationSyntax> group)
        {
            var properties = group
                .SelectMany(g => g.Members)
                .OfType<PropertyDeclarationSyntax>()
                .Where(IsDependency)
                .Distinct();

            return
                $"{string.Join("\n\t\t", properties.Select(p => $"private readonly {p.Type} _{(p.Identifier.Text).ToLower()};"))}\n"
                + "\n" +
$"\t\t" + $@"public {(group.First().Identifier.Text).Substring(1)}Factory({string.Join(", ", properties.Select(p => $"{p.Type} {p.Identifier.Text.Replace("<", "_").Replace(">", "_")}"))})
        {{
	        {string.Join(";\n\t\t\t", properties.Select(p => $"_{p.Identifier.Text.Replace("<", "_").Replace(">", "_").ToLower()} = {p.Identifier.Text.Replace("<", "_").Replace(">", "_")};"))}
        }}";
        }

        private static string GenerateCreateMethod(
            InterfaceDeclarationSyntax interfaceSyntax,
            IEnumerable<IGrouping<string, ClassDeclarationSyntax>> factoryChildGroups)
        {
            var properties = interfaceSyntax.Members.OfType<PropertyDeclarationSyntax>();
            var enums = factoryChildGroups.Select(x => x.Key);

            return GenerateCreateMethodDeclaration(interfaceSyntax) +
$@"
        {{
            switch (type)
            {{
                {string.Join(";\n\t\t\t", enums.Select(e=>$"case {interfaceSyntax.Identifier.Text.Substring(1)}FactoryType.{e}:\n\t\t\t\t\treturn new {e}({GenerateConstructorParameters(interfaceSyntax)});"))}
                default:
                    throw new Exception($""Shape type {{type}} is not handled"");
            }}    
        }}
";
        }

        private static string GenerateConstructorParameters(InterfaceDeclarationSyntax interfaceSyntax)
        {
            var properties = interfaceSyntax.Members.OfType<PropertyDeclarationSyntax>();

            return string.Join(", ",properties.Select(
                        p =>
                        {
                            if (IsDependency(p))
                            {
                                return $"_{p.Identifier.Text.Replace("<", "_").Replace(">", "_").ToLower()}";
                            }

                            return p.Identifier.Text.Replace("<", "_").Replace(">", "_");

                        }));
        }

        private static string GenerateCreateMethodDeclaration(InterfaceDeclarationSyntax interfaceSyntax)
        {
            var properties = interfaceSyntax.Members.OfType<PropertyDeclarationSyntax>();

            var factoryType = $"{interfaceSyntax.Identifier.Text.Substring(1)}FactoryType type";
            var parameters = $", {string.Join(", ", properties.Where(IsNotDependency).Select(CreateParameter))}";

            if(parameters !="")
                return $"public {interfaceSyntax.Identifier.Text} Create({factoryType}{parameters})";
            else
                return $"public {interfaceSyntax.Identifier.Text} Create({factoryType})";
        }

        private static string CreateParameter(PropertyDeclarationSyntax propertySyntax)
        {
            return $"{propertySyntax.Type} {propertySyntax.Identifier.Text.ToString().Replace("<", "_").Replace(">", "_")}";
        }
        private static bool IsDependency(MemberDeclarationSyntax memberSyntax)
        {
            return !memberSyntax.AttributeLists.Any(x => x.Attributes.Any(y => y.Name.GetText().ToString().Contains("Parameter")));

        }
        private static bool IsNotDependency(MemberDeclarationSyntax memberSyntax) => !IsDependency(memberSyntax);
    }
}
