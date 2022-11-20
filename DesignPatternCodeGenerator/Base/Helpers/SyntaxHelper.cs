using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternCodeGenerator.Base.Helpers
{
    public static class SyntaxHelper
    {
        internal static string GetAtributeValueText(IGrouping<string, TypeDeclarationSyntax> group)
                => group
                .Select(x => x.AttributeLists.First().Attributes.First().ArgumentList.Arguments.First().Expression.GetFirstToken().ValueText)
                .First();
    }
}
