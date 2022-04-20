using System;
using System.IO;
using System.Reflection;

namespace Prueba.Common
{
    public class Log : ILog
    {
        private readonly string pathE;

        public Log()
        {
            try
            {
                pathE = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't initialize Logger: " + ex.Message);
            }
        }

        public void Error(string message)
        {
            WriteMessage("ERROR", message);
        }

        public void Message(string message)
        {
            WriteMessage("INFO", message);
        }

        public void Success(string message)
        {
            WriteMessage("SUCCESS", message);
        }

        private void WriteMessage(string type, string logMessage)
        {
            using StreamWriter txtWriter = File.AppendText(pathE + "\\" + "log.txt");

            try
            {
                txtWriter.Write($"\r\n{type} Log: ");
                txtWriter.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                txtWriter.WriteLine($"Context: {logMessage}");
                txtWriter.WriteLine("************************************************************************");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't write log: " + ex.Message);
            }
        }
    }
}
