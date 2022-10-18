using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternCodeGenerator.Base.Models
{
    internal class SyntaxTokens
    {
        internal string Accessibility { get; set; }
        internal string Namespace { get; set; }
        internal IEnumerable<string> Usings { get; set; }
        internal string ClassName { get; set; }
        internal string InterfaceName { get; set; }

    }
}
