using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace DesignPatternCodeGenerator.Singleton
{
    internal static class SingletonContentComponentsGenerator
    {
        internal static string GenerateInstanceField(IGrouping<string, ClassDeclarationSyntax> group)
            => $"private static {group.Key} _instance = null;";

        internal static string GenerateObjectToLock(IGrouping<string, ClassDeclarationSyntax> group)
            => $"private static object obj = new object();";

        internal static string GenerateConstructor(IGrouping<string, ClassDeclarationSyntax> group)
            => $"private {group.Key}() {{ }}";

        internal static string GenerateGetInstanceMethod(IGrouping<string, ClassDeclarationSyntax> group)
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
