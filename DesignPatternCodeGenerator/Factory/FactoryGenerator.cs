using DesignPatternCodeGenerator.Base;
using Microsoft.CodeAnalysis;
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
                var codeGenerator = new ContentCodeGenerator(group);

                var interfaceContent = GenerateInterface(codeGenerator, group);
                context.AddSource($"I{group.Key}Factory.g.cs", SourceText.From(interfaceContent, Encoding.UTF8));

                var classContent = GenerateClass(codeGenerator, group);
                context.AddSource($"{group.Key}Factory.g.cs", SourceText.From(classContent, Encoding.UTF8));
            }
        }

        private string GenerateInterface(ContentCodeGenerator codeGenerator, IGrouping<string, ConstructorDeclarationSyntax> group)
        {
            return codeGenerator.GetUsingsAndNamespace() + 
$@"
{{
    {codeGenerator.Accessibility} interface I{group.Key}Factory
    {{
	    {string.Join("\n", group.Select(GenerateCreateMethodDeclaration).Select(x => x + ";"))}
    }}
}}";
        }

        private string GenerateCreateMethodDeclaration(ConstructorDeclarationSyntax syntax)
        {
            return $"public I{syntax.Identifier.Text} Create({string.Join(", ", syntax.ParameterList.Parameters.Where(IsNotDependency).Select(CreateParameter))})";
        }

        private string GenerateClass(ContentCodeGenerator codeGenerator, IGrouping<string, ConstructorDeclarationSyntax> group)
        {
            return codeGenerator.GetUsingsAndNamespace() + 
$@"
{{
    {codeGenerator.Accessibility} class {group.Key}Factory : I{group.Key}Factory
    {{
	    {GenerateFieldsAndConstructor(group)}

	    {string.Join("\n", group.Select(GenerateCreateMethod))}
    }}
}}";
        }

        private string GenerateFieldsAndConstructor(IEnumerable<ConstructorDeclarationSyntax> group)
        {
            var paramterTypes = group.SelectMany(x => x.ParameterList.Parameters)
                .Where(IsDependency)
                .Select(x => x.Type)
                .Distinct();

            return $"{string.Join(";\n", paramterTypes.Select(x => $"private readonly {x} _{x};"))}\n" +
$"\t\t" + $@"public {group.First().Identifier}Factory({string.Join(", ", paramterTypes.Select(x => $"{x} {x.ToString().Replace("<", "_").Replace(">", "_")}"))})
        {{
	        {string.Join(";\n", paramterTypes.Select(x => $"_{x.ToString().Replace("<", "_").Replace(">", "_")} = {x.ToString().Replace("<", "_").Replace(">", "_")};"))}
        }}";
        }

        private string GenerateCreateMethod(ConstructorDeclarationSyntax arg)
        {
            return GenerateCreateMethodDeclaration(arg) +
                $@"=> new {arg.Identifier}({string.Join(
                    ", ",
                    arg.ParameterList.Parameters.Select(
                        x =>
                        {
                            if (IsDependency(x))
                                return $"_{x.Type.ToString().Replace("<", "_").Replace(">", "_")}";
                            return x.Type.ToString().Replace("<", "_").Replace(">", "_");
                        }))});";
        }



        private bool IsDependency(ParameterSyntax syntax)
        {
            return !syntax.AttributeLists.Any(x => x.Attributes.Any(y => y.Name.GetText().ToString().Contains("Parameter")));
        }

        private bool IsNotDependency(ParameterSyntax syntax) => !IsDependency(syntax);

        private string CreateParameter(ParameterSyntax syntax)
        {
            return $"{syntax.Type} {syntax.Type.ToString().Replace("<", "_").Replace(">", "_")}";
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
