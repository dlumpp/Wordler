namespace Wordler.Core.UnitTests
{
    public class MixerTests
    {

        IEnumerable<IEnumerable<char?>> Act(IEnumerable<LetterSpace> pool) => Wordler.Core.Mixer.Mix(pool.ToList());

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
            var clues = new[] { new LetterSpace { Value = 'A', At = 0 } };
            var actual = Act(clues);
            var words = actual.Select(x => LettersToWord(x)).ToArray();
            words.Single().Should().Be("A____");
        }

        [Fact]
        public void TwoGreen()
        {
            var clues = new LetterSpace[] { 
                new()  { Value = 'E', At = 2 } ,
                new()  { Value = 'E', At = 3 } ,
            };
            var actual = Act(clues);
            var words = actual.Select(x => LettersToWord(x)).ToArray();
            words.Single().Should().Be("__EE_");
        }

        [Fact]
        public void OneYellow()
        {
            var a = new LetterSpace { Value = 'A' };
            a.NotAt.Add(1);
            var actual = Act(new[] { a });
            actual.Select(x => LettersToWord(x)).Should().Equal(
               "A____",
               "__A__",
               "___A_",
               "____A");
        }

        [Fact]
        public void OneYellowTwoNots()
        {
            var t = new LetterSpace { Value = 'T' };
            t.NotAt.Add(3);
            t.NotAt.Add(4);
            var actual = Act(new[] { t });
            var words = actual.Select(x => LettersToWord(x)).ToArray();
            words.Should().Equal(
                "T____",
                "_T___",
                "__T__");
        }

        [Fact]
        public void GreensAndYellow()
        {
            var t = new LetterSpace { Value = 'T', At = 0 };
            var i = new LetterSpace { Value = 'I', At = 3 };
            var a = new LetterSpace { Value = 'A', NotAt = new() { 1, 2 } };
            var actual = Act(new[] { t, i, a });
            var words = actual.Select(x => LettersToWord(x)).ToArray();
            words.Single().Should().Be("T__IA");
        }
    }
}