using Microsoft.Extensions.Logging;
using QuChallenge.Domain;

namespace QuChallenge.Console
{
    internal class Runner : IRunner
    {
        private readonly Settings _settings;
        private readonly ILogger<Runner> _logger;

        public Runner(Microsoft.Extensions.Options.IOptions<Settings> settings, ILogger<Runner> logger)
        {
            _settings = settings.Value;
            _logger = logger;
        }

        public int Run()
        {
            try
            {
                _logger.LogDebug("Started running");
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

                WordFinder wordFinder;
                if (_settings.NumberOfThreads > 0)
                {
                    wordFinder = new WordFinder(matrix, _settings.NumberOfThreads);
                } 
                else
                {
                    wordFinder = new WordFinder(matrix);
                }

                var result = wordFinder.Find(wordStream).ToList();

                _logger.LogInformation($"Words found: {string.Join(", ", result)}");
                _logger.LogDebug("Finished running");
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while trying to find words {ex.Message}");
                return 1;
            }
        }
    }
}
