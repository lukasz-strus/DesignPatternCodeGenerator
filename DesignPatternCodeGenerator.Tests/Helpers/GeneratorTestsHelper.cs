using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DesignPatternCodeGenerator.Tests.Helpers;

internal static class GeneratorTestsHelper
{
    internal static Compilation CreateCompilation(string sourceCode)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);
        var references = AppDomain.CurrentDomain.GetAssemblies()
                                  .Where(assembly => !assembly.IsDynamic)
                                  .Select(assembly => MetadataReference
                                                      .CreateFromFile(assembly.Location))
                                  .Cast<MetadataReference>();

        var compilation = CSharpCompilation.Create("SourceGeneratorTests",
                      new[] { syntaxTree },
                      references,
                      new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));


        return compilation;
    }

    internal static IEnumerable<IGrouping<string, ClassDeclarationSyntax>> GetClassGroups(string context)
        => CSharpSyntaxTree
                .ParseText(context)
                .GetRoot()
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .ToList()
                .GroupBy(x => x.Identifier.Text);

    internal static IGrouping<string, ClassDeclarationSyntax> GetClassGroup(string context)
        => CSharpSyntaxTree
                .ParseText(context)
                .GetRoot()
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .ToList()
                .GroupBy(x => x.Identifier.Text)
                .First();
    

    internal static IEnumerable<IGrouping<string, InterfaceDeclarationSyntax>> GetInterfaceGroups(string context)
        => CSharpSyntaxTree
                .ParseText(context)
                .GetRoot()
                .DescendantNodes()
                .OfType<InterfaceDeclarationSyntax>()
                .ToList()
                .GroupBy(x => x.Identifier.Text);

    internal static IGrouping<string, InterfaceDeclarationSyntax> GetInterfaceGroup(string context)
        => CSharpSyntaxTree
                .ParseText(context)
                .GetRoot()
                .DescendantNodes()
                .OfType<InterfaceDeclarationSyntax>()
                .ToList()
                .GroupBy(x => x.Identifier.Text)
                .First();
}
