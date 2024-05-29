using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheArtist
{
    internal class Log
    {
        private static readonly object lockObj = new object();
        private const string LogFileName = "../../../logs.txt";

        public static void Error(string message, string nameFile)
        {
            string formattedMessage = $"ERROR: [message: {message}] - [FielName: {nameFile}] - [Date: {DateTime.Now:yyyy-MM-dd} Time: {DateTime.Now:HH:mm}]";

            try
            {
                string routeFile = Path.Combine(Environment.CurrentDirectory, LogFileName);
                using (StreamWriter writer = new StreamWriter(routeFile, true))
                {
                    writer.WriteLine(formattedMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }
    }
}
