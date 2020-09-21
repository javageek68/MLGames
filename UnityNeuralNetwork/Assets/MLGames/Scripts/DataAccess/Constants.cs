using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class Constants
    {
        public class Salt
        {
            public static string Common = "You can`t handle the truth!";
        }
        /// <summary>
        /// These values must match the values in the Admin.UserTypes table
        /// </summary>
        public class UserTypes
        {
            public static string Test = "Test";
            public static string Production = "Production";
        }
        public class DbServer
        {
            public static string Test = "Test";
            public static string Live = "Live";
            public static string Local = "Local";
        }

        public class ProcessLevel
        {
            public static int NotProcessed = 0;
            public static int Pass1 = 1;
            public static int Pass2 = 2;
        }

        public class Settings
        {
            public static string AvailableFreeSpace = "";
            public static string DriveFormat = "";
            public static string DriveType = "";
            public static string Name = "";
            public static string TotalFreeSpace = "";
            public static string TotalSize = "";
            public static string VolumeLabel = "";
        }

        /// <summary>
        /// These values must match the connection string names in the web.config file
        /// </summary>
        public class ConnectionStrings
        {
            public static string Admin = "AdminCnxn";
            public static string Test = "TestDb";
            public static string Production = "ProductionDb";
            public static string LocalAdminCnxn = "LocalAdminCnxn";
            public static string LocalTestDb = "LocalTestDb";
            public static string LocalProductionDb = "LocalProductionDb";
            public static string TestAdminCnxn = "TestAdminCnxn";
            public static string TestTestDb = "TestTestDb";
            public static string TestProductionDb = "TestProductionDb";

        }

        public class Databases
        {
            public static string Admin = "vscadmin";
            public static string Test = "vsctest";
            public static string Production = "vsc";
        }

        /// <summary>
        /// 
        /// </summary>
        public class TableCompareResults
        {
            public static string RowDoesntExist = "RowDoesntExist";
            public static string RowNewer = "RowNewer";
            public static string RowsMatch = "RowsMatch";
            public static string RowsDoNotMatch = "RowsDoNotMatch";

            public static string BothEmpty = "Both tables are empty";
            public static string NewRows = "New rows in staging";
            public static string DeleteRows = "Delete rows in staging";
            public static string TablesMatch = "Tables match";
            public static string UpdatesDetected = "Updates Detected";
        }

        /// <summary>
        /// 
        /// </summary>
        public class DataFields
        {
            public static string GroupId = "GroupId";
            public static string GroupName = "GroupName";
            public static string UserId = "UserId";
            public static string UserName = "UserName";
            public static string BundleId = "BundleId";
            public static string BundleName = "BundleName";

        }
    }
}