using System.Collections;
using System.Text.RegularExpressions;

namespace MyApp
{
    class LogFileEnumerator : IEnumerator<string>
    {
        private readonly string _logFileName;
        private string _currentLogEntry;
        private string _currentBuffer;
        private IEnumerator<string> _lineReader;
        private bool _isFinished = false;

        private readonly Regex newLogEntryRegex = new("(([0-9]{2}:){2}[0-9]{2}.?) [A-Z]:");

        public LogFileEnumerator(string logFileName)
        {
            _logFileName = logFileName;
            _lineReader = File.ReadLines(logFileName).GetEnumerator();
            _currentBuffer = "";
            _currentLogEntry = "";
        }

        public bool MoveNext()
        {
            if (_isFinished)
            {
                return false;
            }

            _currentLogEntry = "";
            do
            {
                _currentLogEntry += _currentBuffer + "\n";
                if (_lineReader.MoveNext())
                {
                    _currentBuffer = _lineReader.Current;
                }
                else
                {
                    _currentLogEntry = _currentBuffer;
                    _isFinished = true;
                    return true;
                }
            } while (!newLogEntryRegex.Match(_currentBuffer).Success);
            return true;
        }

        public string Current => _currentLogEntry;

        public bool IsFinished => _isFinished;

        object IEnumerator.Current { get => Current; }

        public void Reset()
        {
            _lineReader = File.ReadLines(_logFileName).GetEnumerator();
            _currentBuffer = "";
            _currentLogEntry = "";
        }

        public void Dispose()
        {
            _lineReader.Dispose();
        }
    }

    class LogEnumerable : IEnumerable
    {
        private readonly LogFileEnumerator _logFileEnumerator;
        public LogEnumerable(string filePath)
        {
            _logFileEnumerator = new(filePath);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public LogFileEnumerator GetEnumerator()
        {
            return _logFileEnumerator;
        }
    }
}
