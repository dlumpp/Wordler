using System.Diagnostics.CodeAnalysis;

namespace Wordler.Core
{
    internal class WordPad
    {
        public Stack<LetterSpace> Pool { get; set; }

        public WordPad(Stack<LetterSpace> pool)
        {
            Pool = pool;
        }

        public List<char?> Word { get; set; } = CreateBlankWord();

        public bool IsCompleted => Pool.Count == 0;

        public WordPad Clone() => new(new(Pool))
        {
            Word = Word.Select(i => i).ToList()
        };

        private static List<char?> CreateBlankWord() => new(Enumerable.Range(0, Mixer.WordSize).Select(i => default(char?)));
    }

    public class LetterSpace
    {
        public char? Value { get; set; }
        public int? At { get; set; }
        public List<int> NotAt { get; set; } = new(); // >1 had to come from multiple guesses
    }

    public static class Mixer
    {
        public const int WordSize = 5;

        public static IEnumerable<IEnumerable<char?>> Mix(IEnumerable<LetterSpace> pool)
        {
            var letterQ = new Stack<LetterSpace>(pool);
            var initPad = new WordPad(letterQ);
            var pads = PlaceAll(new[] { initPad });
            return pads.Select(wp => wp.Word).Distinct(new EnumerableValuesComparer<char?>());
        }

        private static IEnumerable<WordPad> PlaceAll(IEnumerable<WordPad> wordPads)
        {
            foreach (var word in wordPads)
            {
                if (word.IsCompleted)
                {
                    yield return word;
                }
                else
                {
                    foreach (var next in PlaceAll(PlaceNext(word)))
                    {
                        yield return next;
                    }
                }
            }
        }

        private static IEnumerable<WordPad> PlaceNext(WordPad wordPad)
        {
            var toPlace = wordPad.Pool.Pop();
            for (int i = 0; i < wordPad.Word.Count; i++)
            {
                var letters = wordPad.Word;
                if (letters[i].HasValue)
                    continue;

                if (toPlace.At == i)
                {
                    letters[i] = toPlace.Value;
                    yield return wordPad;
                    break;
                }
                if (!toPlace.At.HasValue && !toPlace.NotAt.Contains(i))
                {
                    var branch = wordPad.Clone();
                    branch.Word[i] = toPlace.Value;
                    yield return branch;
                }
            }
        }
    }

    internal class EnumerableValuesComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        public bool Equals(IEnumerable<T>? x, IEnumerable<T>? y) => (x is null || y is null) ? false : Enumerable.SequenceEqual(x, y);

        public int GetHashCode([DisallowNull] IEnumerable<T> obj) =>
            obj.Select(i => i?.GetHashCode() ?? 0).Sum();
    }
}
