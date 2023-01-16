using System.Diagnostics.CodeAnalysis;

namespace Wordler.Core
{
    //internal
    public class WordPad
    {
        public Stack<LetterSpace> Pool { get; set; }

        public WordPad(Stack<LetterSpace> pool)
        {
            Pool = pool;
        }

        public Word Word { get; set; } = new();

        public bool IsCompleted => Pool.Count == 0;

        public WordPad Clone() => new(new(Pool))
        {
            Word = new() { Letters = new(Word.Letters) }
        };
    }

    //internal
    // is this type useless?
    public class Word
    {
        const int WordSize = 5;

        public List<char?> Letters { get; set; } = new(Enumerable.Range(0, WordSize).Select(i => default(char?)));
    }

    public class LetterSpace
    {
        public char? Value { get; set; }
        public int? At { get; set; }
        public List<int> NotAt { get; set; } = new(); // >1 had to come from multiple guesses
    }

    public class Mixer
    {
        public static IEnumerable<IEnumerable<char?>> Mix(IEnumerable<LetterSpace> pool)
        {
            var letterQ = new Stack<LetterSpace>(pool);
            var initPad = new WordPad(letterQ);
            var pads = PlaceAll(new[] { initPad });
            return pads.Select(wp => wp.Word.Letters).Distinct(new EnumerableValuesComparer<char?>());
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
            for (int i = 0; i < wordPad.Word.Letters.Count; i++)
            {
                var letters = wordPad.Word.Letters;
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
                    branch.Word.Letters[i] = toPlace.Value;
                    yield return branch;
                }
            }
        }
    }

    public class EnumerableValuesComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        public bool Equals(IEnumerable<T>? x, IEnumerable<T>? y) => (x is null || y is null) ? false : Enumerable.SequenceEqual(x, y);

        public int GetHashCode([DisallowNull] IEnumerable<T> obj) =>
            obj.Select(i => i?.GetHashCode() ?? 0).Sum();
    }
}
