using DesignPatternCodeGenerator.Base.Enums;
using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternCodeGenerator.Singleton
{
    public class SingletonContentGenerator
    {
        internal static string GenerateClass
            (BaseCodeGenerator codeGenerator,
            IGrouping<string, ClassDeclarationSyntax> group)
            => codeGenerator.GenerateUsingsAndNamespace() +
$@"
{{
    {codeGenerator.GenerateDeclaration(CodeType.Class)}
    {{
	    {GenerateInstanceField(group)}
        {GenerateObjectToLock(group)}

        {GenerateConstructor(group)}

        {GenerateGetInstanceMethod(group)}
    }}
}}";

        private static string GenerateInstanceField(IGrouping<string, ClassDeclarationSyntax> group)
            => $"private static {group.Key} _instance = null;";

        private static string GenerateObjectToLock(IGrouping<string, ClassDeclarationSyntax> group)
            => $"private static object obj = new object();";

        private static string GenerateConstructor(IGrouping<string, ClassDeclarationSyntax> group)
            => $"private {group.Key}() {{ }}";

        private static string GenerateGetInstanceMethod(IGrouping<string, ClassDeclarationSyntax> group)
            => $@"
        public static {group.Key} GetInstance()
        {{
            lock(obj)
            {{
                if (_instance == null)
                {{
                    _instance = new {group.Key}();
                }}
            }}

        return _instance;
        }}
";

    }
}
