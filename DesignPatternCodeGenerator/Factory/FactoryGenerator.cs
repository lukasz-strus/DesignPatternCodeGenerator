using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
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
            var constructors = GetConstructorDeclarations(context.Compilation, context.CancellationToken).Result;
            var groups = constructors.GroupBy(x => x.Identifier.Text);

            foreach (var group in groups)
            {
                var accessibility = group.First().FirstAncestorOrSelf<TypeDeclarationSyntax>().Modifiers.First().Text;
                var ns = group.First().FirstAncestorOrSelf<NamespaceDeclarationSyntax>()?.Name?.ToString() ?? group.First().FirstAncestorOrSelf<FileScopedNamespaceDeclarationSyntax>().Name.ToString();
                var usings = group.First().FirstAncestorOrSelf<CompilationUnitSyntax>().DescendantNodesAndSelf().OfType<UsingDirectiveSyntax>().Select(x => x.Name.ToString());
                var text1 = $@"
{string.Join("\n", usings.Select(x => $"using {x};"))}
namespace {ns} {{
{accessibility} interface I{group.Key}Factory
{{
	{string.Join("\n", group.Select(GenerateCreateMethodDeclaration).Select(x => x + ";"))}
}}
}}";
                context.AddSource(
                    $"I{group.Key}Factory.g.cs",
                    SourceText.From(text1, Encoding.UTF8)
                    );
                var text = $@"
{string.Join("\n", usings.Select(x => $"using {x};"))}
namespace {ns} {{
internal class {group.Key}Factory : I{group.Key}Factory
{{
	{GenerateFieldsAndConstructor(group)}
	{string.Join("\n", group.Select(GenerateCreateMethod))}
}}
}}";
                context.AddSource(
    $"{group.Key}Factory.g.cs",
    SourceText.From(text, Encoding.UTF8)
    );
            }
        }

        private string GenerateFieldsAndConstructor(IEnumerable<ConstructorDeclarationSyntax> group)
        {
            var paramterTypes = group.SelectMany(x => x.ParameterList.Parameters).Where(IsDependency).Select(x => x.Type).Distinct();
            return $"{string.Join(";\n", paramterTypes.Select(x => $"private readonly {x} _{x};"))}\n"
                +
                $@"public {group.First().Identifier}Factory({string.Join(", ", paramterTypes.Select(x => $"{x} {x.ToString().Replace("<", "_").Replace(">", "_")}"))})
{{
	{string.Join(";\n", paramterTypes.Select(x => $"_{x.ToString().Replace("<", "_").Replace(">", "_")} = {x.ToString().Replace("<", "_").Replace(">", "_")};"))}
}}";
        }

        private string GenerateCreateMethod(ConstructorDeclarationSyntax arg)
        {
            return GenerateCreateMethodDeclaration(arg) +
                $@"=>new {arg.Identifier}({string.Join(
                    ", ",
                    arg.ParameterList.Parameters.Select(
                        x =>
                        {
                            if (IsDependency(x))
                                return $"_{x.Type.ToString().Replace("<", "_").Replace(">", "_")}";
                            return x.Type.ToString().Replace("<", "_").Replace(">", "_");
                        }))});";
        }

        private string GenerateCreateMethodDeclaration(ConstructorDeclarationSyntax syntax)
        {
            //todo: interface names
            return $"public I{syntax.Identifier.Text} Create({string.Join(", ", syntax.ParameterList.Parameters.Where(IsNotDependency).Select(CreateParameter))})";
        }

        private bool IsDependency(ParameterSyntax syntax)
        {
            var b = syntax.AttributeLists.SelectMany(x => x.Attributes.Select(y => y.Name.GetText())).ToArray();
            return !syntax.AttributeLists.Any(x => x.Attributes.Any(y => y.Name.GetText().ToString().Contains("Parameter")));
        }
        
        private bool IsNotDependency(ParameterSyntax syntax) => !IsDependency(syntax);

        private string CreateParameter(ParameterSyntax syntax)
        {
            return $"{syntax.Type} {syntax.Type.ToString().Replace("<", "_").Replace(">", "_")}";
        }

        private async Task<IEnumerable<ConstructorDeclarationSyntax>> GetConstructorDeclarations(
            Compilation compilation,
            CancellationToken token)
        {
            return (await Task.WhenAll(compilation.SyntaxTrees.Select(x => GetConstructorDeclarationSyntax(x, compilation, token))))
                .SelectMany(x => x);
        }

        private async Task<IEnumerable<ConstructorDeclarationSyntax>> GetConstructorDeclarationSyntax(
            SyntaxTree tree,
            Compilation compilation,
            CancellationToken token)
        {
            var semanticModel = compilation.GetSemanticModel(tree);
            var constructors = (await tree.GetRootAsync(token)).DescendantNodes()
                .OfType<ConstructorDeclarationSyntax>()
                .Where(x => x.AttributeLists.Any());

            return constructors.Where(x => x.AttributeLists.Any(y => y.Attributes.Any(z => semanticModel.GetTypeInfo(z).Type.Name == typeof(FactoryAttribute).Name)));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
