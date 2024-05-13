using System.Collections;
using System.Text.RegularExpressions;

namespace MyApp
{
    internal class Program
    {
        static void Main()
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
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("Finished parsing the log file.");
            }
        }
    }
}