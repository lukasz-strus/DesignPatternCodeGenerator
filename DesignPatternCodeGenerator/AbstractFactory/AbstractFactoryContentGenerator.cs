using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Factory;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternCodeGenerator.AbstractFactory
{
    public class AbstractFactoryContentGenerator
    {
        internal static string GenerateMainInterface(
        BaseCodeGenerator codeGenerator,
        IEnumerable<IGrouping<string, InterfaceDeclarationSyntax>> groups)
        => codeGenerator.GenerateUsingsAndNamespace() +
$@"
{{
    {codeGenerator.GenerateDeclaration(CodeType.Interface)}
    {{
	    {GenerateCreateMethodInterface(groups)}
    }}
}}";

        private static string GenerateCreateMethodInterface(IEnumerable<IGrouping<string, InterfaceDeclarationSyntax>> groups)
        {
            string ret = "";

            groups.ToList().ForEach(x => ret += $"{string.Join("\n", x.Select(GenerateCreateMethodDeclaration).Select(y => y + ";"))}\n\t\t");

            return ret;
        }

        private static string GenerateCreateMethodDeclaration(InterfaceDeclarationSyntax interfaceSyntax)
            => $"{interfaceSyntax.Identifier.Text} Create{(interfaceSyntax.Identifier.Text).Substring(1)}()";
        

    }
}
