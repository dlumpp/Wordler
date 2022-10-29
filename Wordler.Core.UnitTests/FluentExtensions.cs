namespace Wordler.Core.UnitTests;

public static class FluentExtensions
{
    public static LetterSpace NotAt(this char c, params int[] indexes) => 
        new() { NotAt = indexes.ToList(), Value = c };

    public static LetterSpace At(this char c, int index) =>
    new() { At = index, Value = c };
}
