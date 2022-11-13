namespace DesignPatternCodeGenerator.Tests.Helpers;

internal static class StringHelper
{
    internal static string RemoveWhitespace(this string input)
        => new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
}
