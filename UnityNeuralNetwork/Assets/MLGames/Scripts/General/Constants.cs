using System;

namespace General
{
    public class Constants
    {
        
        public class AppSettings
        {
            public static string TraceLevel = "TraceLevel";
            public static string ServerMode = "ServerMode";
            public static string LogFolder = "LogFolder";
        }

        public class Paths
        {
            public static string PageThumbnails = @"Images\PageThumbnails";
            public static string ApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\{0}\\Database\\";
            public static string Baackups = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\{0}\\Backups\\";
            public static string Logs = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\{0}\\Logs\\";
            public static string Recordings = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\{0}\\Recordings\\";
            public static string Output = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\{0}";
        }
    }
}
