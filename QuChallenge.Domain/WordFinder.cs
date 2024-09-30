using System.Collections.Concurrent;
using System.Text;

namespace QuChallenge.Domain
{
    public class WordFinder
    {
        private readonly IEnumerable<string> _matrix;
        private readonly IEnumerable<string> _twistedMatrix;
        private readonly int _numberOfThreads;
        private readonly int _rows;
        private readonly int _cols;

        public WordFinder(IEnumerable<string> matrix)
        {
            _matrix = matrix;
            _rows = _matrix.Count();
            _cols = _matrix.FirstOrDefault()?.Length ?? 0;
            _twistedMatrix = TwistMatrix(matrix);
            // The quantity of threads could be changed to optimize performance depending on the number of cores
            // Here I'm assuming the scenario of an 8 - core CPU with two threads per core, so the CPU boasts 16 threads for task execution
            _numberOfThreads = 16;
        }

        public WordFinder(IEnumerable<string> matrix, int numberOfThreads): this(matrix)
        {
            _numberOfThreads = numberOfThreads;
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            // filter words longer than 64, since matrix cannot be larger than 64x64
            var filtered = wordstream.Where(word => word.Length <= 64).ToList();

            var foundWords = new ConcurrentDictionary<string, int>();

            var count = filtered.Count();
            if (count == 0) return [];

            // Divide and conquer
            // CPU-intensive task, so using threads 
            var chunkSize = Convert.ToInt32(Math.Max(Math.Ceiling((decimal)(count / _numberOfThreads)), 1)); // Make sure that chunk size is at least 1
            var chunks = filtered.Chunk(chunkSize);
            Parallel.ForEach(chunks, (chunk) =>
            {
                FindInChunk(foundWords, chunk);
            });

            // Return top 10 most frequent words
            return foundWords
                    .OrderByDescending(kvp => kvp.Value)
                    .Take(10)
                    .Select(kvp => kvp.Key);
        }

        private void FindInChunk(ConcurrentDictionary<string, int> foundWords, IEnumerable<string> chunk)
        {
            foreach (var word in chunk)
            {
                if (foundWords.ContainsKey(word))
                {
                    foundWords[word]++;
                    continue;
                }
                // Horizontal Search
                foreach (var horizontalString in _matrix)
                {
                    if (horizontalString.Contains(word))
                    {
                        foundWords.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1);
                        break;
                    }
                }

                if (foundWords.ContainsKey(word))
                {
                    continue;
                }

                // Vertical Search
                foreach (var verticalString in _twistedMatrix)
                {
                    if (verticalString.Contains(word))
                    {
                        foundWords.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1);
                        break;
                    }
                }
            }
        }

        private IEnumerable<string> TwistMatrix(IEnumerable<string> matrix)
        {
            var twisted = new List<string>();
            StringBuilder stringBuilder = new StringBuilder(_rows);
            for (var i = 0; i < _cols; i++)
            {
                foreach (var word in matrix)
                {
                    stringBuilder.Append(word[i]);
                }
                twisted.Add(stringBuilder.ToString());
            }
            return twisted;
        }
    }

}