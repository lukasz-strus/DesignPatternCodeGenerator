using DesignPatternCodeGenerator.Base;
using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using DesignPatternCodeGenerator.Base.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatternCodeGenerator.Factory
{
    /*
     * todo 1: Parametr [Factory] nakładany na interface
     * todo 2: Parametr [Paramter} nakładany na prop interace
     * todo 3: Generowanie enum klas
     * todo 4: Metoda Create tworzy odpowiedni typ w zależności od enum klas
     */

    [Generator]
    public class FactoryGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {

//#if DEBUG
//            Debugger.Launch();
//#endif

            var sourceContext = new DeclarationsSyntaxGenerator(context, GeneratorType.Factory);

            var groups = sourceContext.InterfaceGroups;

            foreach (var group in groups)
            {
                var syntaxTokensGenerator = new SyntaxTokensGenerator(group, GeneratorType.Factory);

                var syntaxTokens = syntaxTokensGenerator.GenerateSyntaxTokens();

                var codeGenerator = new BaseCodeGenerator(syntaxTokens);

                var interfaceContent = GenerateInterface(codeGenerator, group);
                context.AddSource($"{syntaxTokens.InterfaceName}.g.cs", SourceText.From(interfaceContent, Encoding.UTF8));

                var classContent = GenerateClass(codeGenerator, group);
                context.AddSource($"{syntaxTokens.ClassName}.g.cs", SourceText.From(classContent, Encoding.UTF8));
            }
        }

        #region InterfaceGenerator
        private string GenerateInterface(BaseCodeGenerator codeGenerator, IGrouping<string, InterfaceDeclarationSyntax> group)
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
        #endregion

        #region ClassGenerator
        private string GenerateClass(BaseCodeGenerator codeGenerator, IGrouping<string, InterfaceDeclarationSyntax> group)
        {
            return codeGenerator.GenerateUsingsAndNamespace() + 
$@"
{{
    {codeGenerator.GenerateDeclaration(CodeType.Class)}
    {{
	    {GenerateFieldsAndConstructor(group)}

	    {string.Join("\n", group.Select(GenerateCreateMethod))}
    }}
}}";
        }

        private string GenerateFieldsAndConstructor(IEnumerable<InterfaceDeclarationSyntax> group)
        {
            var properties = group
                .SelectMany(x => x.Members)
                .OfType<PropertyDeclarationSyntax>()
                .Where(IsDependency)
                .Distinct();

            return
                $"{string.Join("\n\t\t", properties.Select(x => $"private readonly {x.Type} _{(x.Identifier.Text).ToLower()};"))}\n"
                + "\n" +
$"\t\t" + $@"public {(group.First().Identifier.Text).Substring(1)}Factory({string.Join(", ", properties.Select(x => $"{x.Type} {x.Identifier.Text.Replace("<", "_").Replace(">", "_")}"))})
        {{
	        {string.Join(";\n\t\t\t", properties.Select(x => $"_{x.Identifier.Text.Replace("<", "_").Replace(">", "_").ToLower()} = {x.Identifier.Text.Replace("<", "_").Replace(">", "_")};"))}
        }}";
        }



        #endregion

        #region CreateMethodGenerator

        private string GenerateCreateMethod(InterfaceDeclarationSyntax syntax)
        {
            var properties = syntax.Members.OfType<PropertyDeclarationSyntax>();

            return GenerateCreateMethodDeclaration(syntax) +
                $@" => new {(syntax.Identifier.Text).Substring(1)}({string.Join(
                    ", ",
                    properties.Select(
                        x =>
                        {
                            if (IsDependency(x))
                            {
                                return $"_{x.Identifier.Text.Replace("<", "_").Replace(">", "_").ToLower()}";
                            }

                            return x.Identifier.Text.Replace("<", "_").Replace(">", "_");

                        }))});";
        }
        private string GenerateCreateMethodDeclaration(InterfaceDeclarationSyntax syntax)
        {
            var properties = syntax.Members.OfType<PropertyDeclarationSyntax>();

            return $"public {syntax.Identifier.Text} Create({string.Join(", ", properties.Where(IsNotDependency).Select(CreateParameter))})";
        }

        private string CreateParameter(PropertyDeclarationSyntax syntax)
        {
            return $"{syntax.Type} {syntax.Identifier.Text.ToString().Replace("<", "_").Replace(">", "_")}";
        }
        #endregion

        #region IsParameter
        private bool IsDependency(MemberDeclarationSyntax syntax)
        {
            return !syntax.AttributeLists.Any(x => x.Attributes.Any(y => y.Name.GetText().ToString().Contains("Property")));

        }

        private bool IsNotDependency(MemberDeclarationSyntax syntax) => !IsDependency(syntax);
        #endregion


        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
