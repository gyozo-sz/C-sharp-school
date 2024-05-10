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
        private int _errorCount;
        private int _lineCount;

        public ErrorLogEnumerator(string filePath) {
            _logEnumerator = new(filePath);
        }

        public string Current => _currentErrorLog;
        public int ErrorCount => _errorCount;
        public int LineCount => _lineCount;

        object IEnumerator.Current { get => Current; }

        public bool MoveNext()
        {
            if (_logEnumerator.IsFinished)
            {
                return false;
            }

            do
            {
                _lineCount++;
                if(_logEnumerator.MoveNext())
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
        private string _logFilePath;
        private ErrorLogEnumerator _logEnumerator;
        public ErrorLogEnumerable(string filePath) {
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
            return Convert.ToDouble(_logEnumerator.LineCount) / Convert.ToDouble(_logEnumerator.ErrorCount);
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

    class CriticalErrorException : Exception
    {
        public CriticalErrorException(string errorMessage) : base(errorMessage) { }
    }

    class ErrorCounter
    {
        private ErrorLogEnumerable _errorLogEnumerator;
        private string _outputFilePath;

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
                } catch (CriticalErrorException criticalError)
                {
                    Console.Write(criticalError.Message);
                }
                    
            }

            return _errorLogEnumerator.ErrorRatio();
        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            const string logFilePath = @"../../../yupdate.txt";
            const string outputFilePath = @"../../../errors.log";

            try
            {
                ErrorCounter errorCounter = new(logFilePath, outputFilePath);
                double errorRatio = errorCounter.EnumerateErrors();
                Console.WriteLine($"Error ratio in the log file: {errorRatio}");
            } 
            catch (IOException inputOutputException)
            {
                Console.WriteLine("File operation error: {0}", inputOutputException.Message);
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine($"There were no errors in the log file.");
            }
            finally
            {
                Console.WriteLine("Finished parsing the log file.");
            }

        }
    }
}