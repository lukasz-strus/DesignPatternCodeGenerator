using DesignPatternCodeGenerator.Base.Generators;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace DesignPatternCodeGenerator.Singleton.Components
{
    internal static class SingletonComponentsGenerator
    {
        internal static string GenerateDeclaration(IGrouping<string, ClassDeclarationSyntax> group)
            => BaseNamesGenerator.GetAccesibility(group) + " partial class " + BaseNamesGenerator.GetClassName(group);

        internal static string GenerateInstanceField(IGrouping<string, ClassDeclarationSyntax> group)
            => $"private static {BaseNamesGenerator.GetClassName(group)} _instance = null;";

        internal static string GenerateObjectToLock(IGrouping<string, ClassDeclarationSyntax> group)
            => $"private static object obj = new object();";

        internal static string GenerateConstructor(IGrouping<string, ClassDeclarationSyntax> group)
            => $"private {BaseNamesGenerator.GetClassName(group)}() {{ }}";

        internal static string GenerateGetInstanceMethod(IGrouping<string, ClassDeclarationSyntax> group)
            => $@"
        {BaseNamesGenerator.GetAccesibility(group)} static {BaseNamesGenerator.GetClassName(group)} GetInstance()
        {{
            lock(obj)
            {{
                if (_instance == null)
                {{
                    _instance = new {BaseNamesGenerator.GetClassName(group)}();
                }}
            }}

        return _instance;
        }}
";
    }
}
