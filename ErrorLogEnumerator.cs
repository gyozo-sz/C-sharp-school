using System.Collections;
using System.Text.RegularExpressions;

namespace MyApp
{
    class ErrorLogEnumerator : IEnumerator<string>
    {
        private string _currentErrorLog = "";
        private readonly Regex _errorRegex = new("error", RegexOptions.IgnoreCase);
        private readonly LogFileEnumerator _logEnumerator;
        private int _errorCount;
        private int _logCount;

        public ErrorLogEnumerator(string filePath)
        {
            _logEnumerator = new(filePath);
        }

        public string Current => _currentErrorLog;
        public int ErrorCount => _errorCount;
        public int LogCount => _logCount;

        object IEnumerator.Current { get => Current; }

        public bool MoveNext()
        {
            if (_logEnumerator.IsFinished)
            {
                return false;
            }

            do
            {
                _logCount++;
                if (_logEnumerator.MoveNext())
                {
                    _currentErrorLog = _logEnumerator.Current;
                }
                else
                {
                    return false;
                }
            } while (!_errorRegex.Match(_currentErrorLog).Success);
            _errorCount++;
            return true;
        }

        public void Reset()
        {
            _logEnumerator.Reset();
            _currentErrorLog = "";
        }

        public void Dispose()
        {
            _logEnumerator.Dispose();
        }
    }

    class ErrorLogEnumerable : IEnumerable
    {
        private readonly ErrorLogEnumerator _logEnumerator;
        public ErrorLogEnumerable(string filePath)
        {
            _logEnumerator = new(filePath);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ErrorLogEnumerator GetEnumerator()
        {
            return _logEnumerator;
        }

        public double ErrorRatio()
        {
            if (_logEnumerator.ErrorCount == 0)
            {
                throw new DivideByZeroException();
            }
            return Convert.ToDouble(_logEnumerator.LogCount) / Convert.ToDouble(_logEnumerator.ErrorCount);
        }
    }
}
