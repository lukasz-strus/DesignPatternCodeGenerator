using DesignPatternCodeGenerator.Base;
using DesignPatternCodeGenerator.Base.Enums;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatternCodeGenerator.Factory
{
    [Generator]
    public class FactoryGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var sourceContext = new SourceContext(context, GeneratorType.Factory);

            var groups = sourceContext.Constructors.GroupBy(x => x.Identifier.Text);

            foreach (var group in groups)
            {
                var codeGenerator = new ContentCodeGenerator(group, GeneratorType.Factory);

                var interfaceContent = GenerateInterface(codeGenerator, group);
                context.AddSource($"{codeGenerator.InterfaceName}.g.cs", SourceText.From(interfaceContent, Encoding.UTF8));

                var classContent = GenerateClass(codeGenerator, group);
                context.AddSource($"{codeGenerator.ClassName}.g.cs", SourceText.From(classContent, Encoding.UTF8));
            }
        }

        #region InterfaceGenerator

        private string GenerateInterface(ContentCodeGenerator codeGenerator, IGrouping<string, ConstructorDeclarationSyntax> group)
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

        private string GenerateClass(ContentCodeGenerator codeGenerator, IGrouping<string, ConstructorDeclarationSyntax> group)
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

        private string GenerateFieldsAndConstructor(IEnumerable<ConstructorDeclarationSyntax> group)
        {
            var parameteres = group
                .SelectMany(x => x.ParameterList.Parameters)
                .Where(IsDependency)
                .Distinct();

            return 
                $"{string.Join("\n\t\t", parameteres.Select(x => $"private readonly {x.Type} _{x.Identifier.Text};"))}\n" 
                + "\n" +
$"\t\t" + $@"public {group.First().Identifier}Factory({string.Join(", ", parameteres.Select(x => $"{x.Type} {x.Identifier.Text.Replace("<", "_").Replace(">", "_")}"))})
        {{
	        {string.Join(";\n\t\t\t", parameteres.Select(x => $"_{x.Identifier.Text.Replace("<", "_").Replace(">", "_")} = {x.Identifier.Text.Replace("<", "_").Replace(">", "_")};"))}
        }}";
        }

        private string GenerateCreateMethod(ConstructorDeclarationSyntax arg)
        {
            return GenerateCreateMethodDeclaration(arg) +
                $@" => new {arg.Identifier}({string.Join(
                    ", ",
                    arg.ParameterList.Parameters.Select(
                        x =>
                        {
                            if (IsDependency(x))
                            {
                                return $"_{x.Identifier.Text.Replace("<", "_").Replace(">", "_")}";
                            }

                            return x.Identifier.Text.Replace("<", "_").Replace(">", "_");

                        }))});";
        }

        #endregion

        private string GenerateCreateMethodDeclaration(ConstructorDeclarationSyntax syntax)
        {
            return $"public I{syntax.Identifier.Text} Create({string.Join(", ", syntax.ParameterList.Parameters.Where(IsNotDependency).Select(CreateParameter))})";
        }
        private string CreateParameter(ParameterSyntax syntax)
        {
            return $"{syntax.Type} {syntax.Identifier.Text.ToString().Replace("<", "_").Replace(">", "_")}";
        }


        private bool IsDependency(ParameterSyntax syntax)
        {
            return !syntax.AttributeLists.Any(x => x.Attributes.Any(y => y.Name.GetText().ToString().Contains("Parameter")));
        }

        private bool IsNotDependency(ParameterSyntax syntax) => !IsDependency(syntax);

        

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
