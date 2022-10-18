﻿using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternCodeGenerator.Base.Generators
{
    internal class BaseCodeGenerator
    {
        private readonly SyntaxTokens _syntaxTokens;

        internal BaseCodeGenerator(SyntaxTokens syntaxTokens)
        {
            _syntaxTokens = syntaxTokens;
        }

        internal string GenerateUsingsAndNamespace()
        {
            string usingsAndNamespace =
$@"// <auto-generated/>
{string.Join("\n", _syntaxTokens.Usings.Select(x => $"using {x};"))}

namespace {_syntaxTokens.Namespace}";

            return usingsAndNamespace;
        }

        internal string GenerateDeclaration(CodeType codeType)
        {
            string declaration = codeType == CodeType.Interface
                ? $@"{_syntaxTokens.Accessibility} interface {_syntaxTokens.InterfaceName}"
                : $@"{_syntaxTokens.Accessibility} class {_syntaxTokens.ClassName}: {_syntaxTokens.InterfaceName}";

            return declaration;
        }
    }
}
