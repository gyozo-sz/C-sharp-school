using MyApp;

namespace MyApp
{
    class CriticalErrorException : Exception
    {
        public CriticalErrorException(string errorMessage) : base(errorMessage) { }
    }

    class ErrorCounter
    {
        private readonly ErrorLogEnumerable _errorLogEnumerator;
        private readonly string _outputFilePath;

        public ErrorCounter(string logFilePath, string outputFilePath)
        {
            _errorLogEnumerator = new(logFilePath);
            _outputFilePath = outputFilePath;
        }

        public double EnumerateErrors()
        {
            using StreamWriter outputFile = new(_outputFilePath);
            foreach (string error in _errorLogEnumerator)
            {
                try
                {
                    outputFile.Write(error);
                    if (error.Contains("CRITICAL ERROR"))
                    {
                        throw new CriticalErrorException(error);
                    }
                }
                catch (CriticalErrorException criticalError)
                {
                    Console.Write(criticalError.Message);
                }
            }

            return _errorLogEnumerator.ErrorRatio();
        }
    }
}
