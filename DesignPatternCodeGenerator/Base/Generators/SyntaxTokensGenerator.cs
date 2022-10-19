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
        private readonly IGrouping<string, TypeDeclarationSyntax> _group;
        private readonly GeneratorAttributeType _generatorType;

        internal SyntaxTokensGenerator(
            IGrouping<string, TypeDeclarationSyntax> group,
            GeneratorAttributeType generatorType)
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

        private string SetAccesibility() =>
            _group.First().FirstAncestorOrSelf<TypeDeclarationSyntax>().Modifiers.First().Text;

        private string SetNamespace() =>
            _group.First().FirstAncestorOrSelf<NamespaceDeclarationSyntax>()?.Name?.ToString() ??
            _group.First().FirstAncestorOrSelf<FileScopedNamespaceDeclarationSyntax>().Name.ToString();

        private IEnumerable<string> SetUsings() =>
            _group.First()
                 .FirstAncestorOrSelf<CompilationUnitSyntax>()
                 .DescendantNodesAndSelf()
                 .OfType<UsingDirectiveSyntax>()
                 .Select(x => x.Name.ToString());

        private string SetClassName() => (_group.Key + _generatorType.ToString()).Substring(1);

        private string SetInterfaceName() => _group.Key + _generatorType.ToString();


    }

}
