namespace Wordler.Core.UnitTests
{
    public class MixerTests
    {

        IEnumerable<IEnumerable<char?>> Act(params LetterSpace[] pool) => Wordler.Core.Mixer.Mix(pool.ToList());

        private string LettersToWord(IEnumerable<char?> letters) => new(letters.Select(l => l ?? '_').ToArray());

        [Fact]
        public void NoCluesGivesEmptyMix()
        {
            var clues = Array.Empty<LetterSpace>();
            var actual = Act(clues);
            LettersToWord(actual.Single()).Should().Be("_____");
        }

        [Fact]
        public void OneGreen()
        {
            var actual = Act('A'.At(0));
            var words = actual.Select(x => LettersToWord(x)).ToArray();
            words.Single().Should().Be("A____");
        }

        [Fact]
        public void TwoGreen()
        {
            var actual = Act('E'.At(2), 'E'.At(3));
            var words = actual.Select(x => LettersToWord(x)).ToArray();
            words.Single().Should().Be("__EE_");
        }

        [Fact]
        public void OneYellow()
        {
            var actual = Act('A'.NotAt(1));
            actual.Select(x => LettersToWord(x)).Should().Equal(
               "A____",
               "__A__",
               "___A_",
               "____A");
        }

        [Fact]
        public void OneYellowTwoNots()
        {
            var actual = Act('T'.NotAt(3, 4));
            var words = actual.Select(x => LettersToWord(x)).ToArray();
            words.Should().Equal(
                "T____",
                "_T___",
                "__T__");
        }

        [Fact]
        public void GreensAndYellow()
        {
            var actual = Act('T'.At(0), 'I'.At(3), 'A'.NotAt(1,2));
            var words = actual.Select(x => LettersToWord(x)).ToArray();
            words.Single().Should().Be("T__IA");
        }
    }
}