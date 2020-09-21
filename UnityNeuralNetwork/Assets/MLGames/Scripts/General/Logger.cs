using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace General
{
    /// <summary>
    /// 
    /// </summary>
    public class Logger
    {
        private static Logger.Level logLevel = Level.Exception;
        private static bool blnReadConfig = false;
        private static string strLogFolder = @"D:\Logs";

        /// <summary>
        /// 
        /// </summary>
        public enum Level
        {
            Exception,
            Low,
            Medium,
            High
        }
        private static string strLogFileName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMsg"></param>
        public static void Log(Logger.Level msgLevel, string strMsg)
        {
            try
            {
                //this message is a non-issue
                if (strMsg.Contains("System.Threading.ThreadAbortException: Thread was being aborted")) return;
                //read the configuration settings the first time through
                if (blnReadConfig == false)
                {
                    ReadConfig();
                    //make sure we have a log file started
                    if (strLogFileName.Length == 0) CreateLogFile();
                    //log a message that indicates the logging level
                    string strStartMsg = string.Format("Logging started at level {0} {1} ", (int)logLevel, logLevel.ToString());
                    WriteToLogFile(strStartMsg);
                }
                    
                //only log the message if it has a greater priority than the current log level
                if (msgLevel <= logLevel) WriteToLogFile(strMsg);
            }
            catch (Exception ex)
            {
                string strErrMsg = ex.ToString();
                //Doh!!!
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMsg"></param>
        private static void WriteToLogFile(string strMsg)
        {
            try
            {
                string strLine = string.Format("{0}\t{1}\r\n", System.DateTime.Now.ToString("yyyy:MM:dd hh:mm:ss"), strMsg);
                System.IO.File.AppendAllText(strLogFileName, strLine);
            }
            catch (Exception ex)
            {
                string strErrMsg = ex.ToString();
                //Doh!!!
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void CreateLogFile()
        {
            //make sure the folder exists
            if (!System.IO.Directory.Exists(strLogFolder)) System.IO.Directory.CreateDirectory(strLogFolder);
            //get the name of the log file
            string strFileName = string.Format("{0}.log", System.DateTime.Now.ToString("yyyyMMddhhmmss"));
            //append the file name to the path
            strLogFileName = string.Format("{0}\\{1}", strLogFolder, strFileName);
        }

        /// <summary>
        /// 
        /// </summary>
        private static void ReadConfig()
        {
            logLevel = Configuration.GetLogLevel(Level.Exception);
            strLogFolder = Constants.Paths.Logs;
            blnReadConfig = true;
        }
    }
}
