namespace DesignPatternCodeGenerator.Base.Models
{
    internal class SyntaxTokensConfigurations
    {
        internal bool IsPartialClass { get; set; } = false;
        internal bool IsDesignPatternPostfix { get; set; } = false;

        internal bool IsMainAttributeOnInterface { get; set; } = false;
    }
}
