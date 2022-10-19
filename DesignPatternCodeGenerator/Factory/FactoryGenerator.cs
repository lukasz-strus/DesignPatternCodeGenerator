using DesignPatternCodeGenerator.Attributes;
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

            var factoryAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.Factory);
            var factoryChildAttribute = AttributeTypeGenerator.SetGeneratorAttributeType(GeneratorAttributeType.FactoryChild);

            var interfaceGroups = DeclarationsSyntaxGenerator.GetInterfaceGroups(context, factoryAttribute);
            var classGroups = DeclarationsSyntaxGenerator.GetClassGroups(context, factoryChildAttribute);


            foreach (var group in interfaceGroups)
            {
                var syntaxTokensGenerator = new SyntaxTokensGenerator(group, GeneratorAttributeType.Factory);

                var syntaxTokens = syntaxTokensGenerator.GenerateSyntaxTokens();

                var codeGenerator = new BaseCodeGenerator(syntaxTokens);

                var factoryChildGroups = FactoryChildGenerator.FilterFactoryChild(classGroups, group);

                var enumChildContent = FactoryChildEnumGenerator.GenerateEnum(codeGenerator, factoryChildGroups);
                context.AddSource($"{syntaxTokens.ClassName}Type.g.cs", SourceText.From(enumChildContent, Encoding.UTF8));

                var interfaceContent = FactoryContentGenerator.GenerateInterface(codeGenerator, group);
                context.AddSource($"{syntaxTokens.InterfaceName}.g.cs", SourceText.From(interfaceContent, Encoding.UTF8));

                var classContent = FactoryContentGenerator.GenerateClass(codeGenerator, group, factoryChildGroups);
                context.AddSource($"{syntaxTokens.ClassName}.g.cs", SourceText.From(classContent, Encoding.UTF8));
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
