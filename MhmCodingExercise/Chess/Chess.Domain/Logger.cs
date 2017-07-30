using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ChessDomain
{

    public enum LogLevel
    {
        info, 
        debug,
        warning,
        error
    }
    /// <summary>
    /// Very basic logging class to handle logging messages. 
    /// </summary>
    public sealed class Logger
    {
        //verify that the application has access to log to this location
        static string path = @"C:\_log\log.txt";

        /// <summary>
        /// Method to allow users to log messages to the log
        /// </summary>
        /// <param name="message">the message of the log entry</param>
        /// <param name="level">the severity of the message, from debug to error</param>
        public static void LogMessage(string message, LogLevel level)
        {
            using (StreamWriter w = File.AppendText(path))
            {
                Log(message, w, level);

            }

            using (StreamReader r = File.OpenText(path))
            {
                DumpLog(r);
            }

        }


        public static void Log(string logMessage, TextWriter w, LogLevel level)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
            w.WriteLine(level.ToString());
        }


        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }

        }
    }
}
