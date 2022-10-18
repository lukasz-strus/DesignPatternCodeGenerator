using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Base.Generators
{
    internal class SyntaxTokensGenerator
    {
        private readonly IGrouping<string, ConstructorDeclarationSyntax> _group;
        private readonly GeneratorType _generatorType;

        internal SyntaxTokensGenerator(
            IGrouping<string, ConstructorDeclarationSyntax> group,
            GeneratorType generatorType)
        {
            _group = group;
            _generatorType = generatorType;
        }

        internal SyntaxTokens GenerateSyntaxTokens() => new SyntaxTokens()
        {
            Accessibility = SetAccesibility(),
            Namespace = SetNamespace(),
            Usings = SetUsings(),
            ClassName = SetClassName(),
            InterfaceName = SetInterfaceName()
        };

        internal string SetAccesibility() =>
            _group.First().FirstAncestorOrSelf<TypeDeclarationSyntax>().Modifiers.First().Text;

        internal string SetNamespace() =>
            _group.First().FirstAncestorOrSelf<NamespaceDeclarationSyntax>()?.Name?.ToString() ??
            _group.First().FirstAncestorOrSelf<FileScopedNamespaceDeclarationSyntax>().Name.ToString();

        internal IEnumerable<string> SetUsings() =>
            _group.First()
                 .FirstAncestorOrSelf<CompilationUnitSyntax>()
                 .DescendantNodesAndSelf()
                 .OfType<UsingDirectiveSyntax>()
                 .Select(x => x.Name.ToString());

        internal string SetClassName() => _group.Key + _generatorType.ToString();

        internal string SetInterfaceName() => "I" + _group.Key + _generatorType.ToString();


    }

}
