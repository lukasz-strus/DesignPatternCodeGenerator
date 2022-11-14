using System.Collections.Generic;

namespace DesignPatternCodeGenerator.Base.Models
{
    internal class SyntaxTokens
    {
        internal string Accessibility { get; set; }
        internal string Namespace { get; set; }
        internal IEnumerable<string> Usings { get; set; }
        internal string ClassName { get; set; }
        internal string InterfaceName { get; set; }
        internal string AdditionalClassToken { get; set; }

    }
}
