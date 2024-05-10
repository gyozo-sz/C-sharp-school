using System;
using System.Collections;
using System.Runtime.CompilerServices;
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

        public LogFileEnumerator(string logFileName) {
            _logFileName = logFileName;
            _lineReader = File.ReadLines(logFileName).GetEnumerator();
            _currentBuffer = "";
        }

        private Regex newLogEntryRegex = new("(([0-9]{2}:){2}[0-9]{2}.?) [A-Z]:");
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

    class ErrorLogEnumerator : IEnumerator<string>
    {
        private string _currentErrorLog = "";
        private readonly Regex _errorRegex = new("error", RegexOptions.IgnoreCase);
        private readonly LogFileEnumerator _logEnumerator;

        public ErrorLogEnumerator(string filePath) {
            _logEnumerator = new(filePath);
        }

        public string Current => _currentErrorLog;

        object IEnumerator.Current { get => Current; }

        public bool MoveNext()
        {
            if (_logEnumerator.IsFinished)
            {
                return false;
            }

            do
            {
                if(_logEnumerator.MoveNext())
                {
                    _currentErrorLog = _logEnumerator.Current;
                }
                else 
                {
                    return false;
                }
            } while (!_errorRegex.Match(_currentErrorLog).Success);
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
        private string _logFilePath;
        public ErrorLogEnumerable(string filePath) => _logFilePath = filePath;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ErrorLogEnumerator GetEnumerator()
        {
            return new ErrorLogEnumerator(_logFilePath);
        }
    }

    class LogEnumerable : IEnumerable
    {
        private LogFileEnumerator _logFileEnumerator;
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

    internal class Program
    {
        static void Main(string[] args)
        {
            const string logFilePath = @"../../../yupdate.txt";
            const string outputFilePath = @"../../../errors.log";

            ErrorLogEnumerable errorEnumerator = new(logFilePath);
            using StreamWriter outputFile = new(outputFilePath);
            foreach (string error in errorEnumerator)
            {
                outputFile.Write(error);
                if (error.Contains("CRITICAL ERROR"))
                {
                    Console.Write(error);
                }
            }


        }
    }
}