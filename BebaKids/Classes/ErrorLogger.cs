using System;
using System.IO;

namespace BebaKids.Classes
{
    public class ErrorLogger
    {
        private string logFilePath = @"C:\bkapps\error.log";


        public ErrorLogger()
        {
            this.logFilePath = logFilePath;
        }

        public void LogException(Exception ex)
        {
            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.WriteLine($"[{DateTime.Now}] Exception Details:");
                writer.WriteLine($"Message: {ex.Message}");
                writer.WriteLine($"Stack Trace: {ex.StackTrace}");
                writer.WriteLine();
            }
        }

        public void LogStringException(String ex)
        {
            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.WriteLine($"[{DateTime.Now}] Exception Details:");
                writer.WriteLine($"Message: {ex}");
                writer.WriteLine();
            }
        }
    }
}
