
using QuChallenge.Domain;

namespace QuChallenge.Tests
{
    [TestFixture]
    public class WordFinderTests
    {
        [Test]
        public void Find_WordsInMatrix_ReturnsCorrectWords()
        {
            // Arrange
            var matrix = new List<string>
        {
            "chillax",
            "deftone",
            "aeolide",
            "weather",
            "ziocold",
            "windxyx",
            "qpwderz"
        };

            var wordStream = new List<string>
        {
            "chill", "cold", "wind", "rain", "storm"
        };

            var expected = new List<string> { "chill", "cold", "wind" };

            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(expected.Count));
            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void Find_WordAppearsMultipleTimesInMatrix_ReturnsWordOnce()
        {
            // Arrange
            var matrix = new List<string>
        {
            "ccoldc",
            "ocoldo",
            "lcoldl",
            "dcoldd",
            "icesaa",
            "snowbb",
            "meltcc",
            "warmdd",
            "hoteee",
        };

            var wordStream = new List<string>
        {
            "ice", "snow", "melt", "warm", "aa", "bb", "cc", "dd", "hot", "ee",
            "ice", "snow", "melt", "warm", "aa", "bb", "cc", "dd", "hot", "ee",
            "cold"
        };

            // cold is repeated in matrix but not in word stream
            var expected = new List<string> { "ice", "snow", "melt", "warm", "aa", "bb", "cc", "dd", "hot", "ee" };

            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(expected.Count));
            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void Find_WordAppearsVertically_ReturnsWord()
        {
            // Arrange
            var matrix = new List<string>
        {
            "abcdef",
            "ghijkl",
            "mnocde",
            "pqrcde",
            "stucde",
            "vwxcde"
        };

            var wordStream = new List<string>
        {
            "cold", "flee", "gmpsv", "mnopqr"
        };

            var expected = new List<string> { "flee", "gmpsv" };

            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(expected.Count));
            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void Find_WordsNotInMatrix_ReturnsEmpty()
        {
            // Arrange
            var matrix = new List<string>
        {
            "abcdef",
            "ghijkl",
            "mnopqr",
            "stuvwx",
            "yzabcd",
            "efghij"
        };

            var wordStream = new List<string>
        {
            "rain", "snow", "storm"
        };

            var expected = new List<string>();

            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(expected.Count));
        }

        [Test]
        public void Find_Top10MostFrequentWords_ReturnsCorrectly()
        {
            // Arrange
            var matrix = new List<string>
        {
            "abcdefghijklmnop",
            "qrstuvwxyzabcdef",
            "ghijklmnopqrstuv",
            "mnopqrstuvwxabcd",
            "fghijklmnopqrstu",
            "wxyzabcdefghijkl",
            "mnopqrstuvwxyzab",
        };

            var wordStream = new List<string>
        {
            "abc", "def", "ghi", "jkl", "mno", "pqr", "stu", "vwx", "yz",
            "abc", "def", "ghi", "jkl", "mno", "pqr", "stu", "vwx", "yz",
            "abc", "def", "ghi", "jkl", "mno", "pqr", "stu", "vwx", "yz",
            "aqgmf", "aqgmf", "fwm", "pfv", "aaa", "bbb", "ccc", "ddd", "eee"
        };

            var expected = new List<string>
        {
            "abc", "def", "ghi", "jkl", "mno", "pqr", "stu", "vwx", "yz", "aqgmf"
        };

            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(10));
            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void Find_EmptyWordStream_ReturnsEmpty()
        {
            // Arrange
            var matrix = new List<string>
        {
            "abcdef",
            "ghijkl",
            "mnopqr",
            "stuvwx",
            "yzabcd",
            "efghij"
        };

            var wordStream = new List<string>(); // Empty word stream

            var expected = new List<string>();

            var wordFinder = new WordFinder(matrix);

            // Act
            var result = wordFinder.Find(wordStream).ToList();

            // Assert
            Assert.That(result.Count, Is.EqualTo(expected.Count));
        }
    }
}