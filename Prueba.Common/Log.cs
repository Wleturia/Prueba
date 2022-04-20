using System;
using System.IO;
using System.Reflection;

namespace Prueba.Common
{
    public class Log : ILog
    {
        private readonly string pathE;
        public DateTime dateTime;

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

        public DateTime Error(string message)
        {
            return WriteMessage("ERROR", message);
        }

        public DateTime Message(string message)
        {
            return WriteMessage("INFO", message);
        }

        public DateTime Success(string message)
        {
            return WriteMessage("SUCCESS", message);
        }

        private DateTime WriteMessage(string type, string logMessage)
        {
            using StreamWriter txtWriter = File.AppendText(pathE + "\\" + "log.txt");
            dateTime = DateTime.Now;

            try
            {
                txtWriter.Write($"\r\n{type} Log: ");
                txtWriter.WriteLine($"{dateTime.ToLongTimeString()} {dateTime.ToLongDateString()}");
                txtWriter.WriteLine($"Context: {logMessage}");
                txtWriter.WriteLine("************************************************************************");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Couldn't write log: " + ex.Message);
            }
            return dateTime;
        }
    }
}
