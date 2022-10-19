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

                var interfaceContent = FactoryContentGenerator.GenerateInterface(codeGenerator, group);
                context.AddSource($"{syntaxTokens.InterfaceName}.g.cs", SourceText.From(interfaceContent, Encoding.UTF8));

                var classContent = FactoryContentGenerator.GenerateClass(codeGenerator, group);
                context.AddSource($"{syntaxTokens.ClassName}.g.cs", SourceText.From(classContent, Encoding.UTF8));
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
