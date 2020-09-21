using dbUtilities;
using System;
using System.Data;
using General;

namespace DataAccess
{
    public class DbOps
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strUserType"></param>
        /// <returns></returns>
        private static string GetCnxnString()
        {
            string strRetVal = string.Empty;
            string strCnxnKey = Constants.ConnectionStrings.Admin;

            try
            {
                Logger.Log(Logger.Level.High, "Entered DbOps.GetCnxnStringFromUserType()");

                string strAppDataPath = General.Constants.Paths.ApplicationData;
                string strAppDataDb = string.Format(@"{0}\dbESDI.db", strAppDataPath);

                strCnxnKey = Constants.ConnectionStrings.LocalAdminCnxn;
                Logger.Log(Logger.Level.High, string.Format("Using connection string key {0} ", strCnxnKey));
                //get the connection string from web.config for this connection string name
                strRetVal = ConfigurationManager.ConnectionStrings[strCnxnKey].ConnectionString;

                if (strRetVal.Trim().Length == 0)
                {
                    Logger.Log(Logger.Level.Exception, string.Format("Error the connection string is empty!!!"));
                }
                else
                {
                    strRetVal = string.Format(strRetVal, strAppDataDb);
                }
                Logger.Log(Logger.Level.High, "Leaving DbOps.GetCnxnStringFromUserType()");
            }
            catch (Exception ex)
            {
                string strErrMsg = ex.ToString();
                Logger.Log(Logger.Level.Exception, string.Format("Error getting the connection string {0} ", strErrMsg));
            }

            return strRetVal;
        }

   

        /// <summary>
        /// Upgrades from one version of the database to the next
        /// </summary>
        /// <returns></returns>
        public static bool InitDatabase(ref string strErrMsg)
        {
            bool blnRetVal = true;
            
            try
            {
                string strAppDataPath = General.Constants.Paths.ApplicationData;
                string strAppDataDb = string.Format(@"{0}\dbESDI.db", strAppDataPath);

                //make sure the app data path exists
                if (!System.IO.Directory.Exists(strAppDataPath)) System.IO.Directory.CreateDirectory(strAppDataPath);
                //if the database isnt in the app data path, then copy it from the assembly path
                if (!System.IO.File.Exists(strAppDataDb))
                {
                    //get the location of the db bundle in the assembly
                    string strAssemblyPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    string strAsseblyDb = string.Format(@"{0}\Data\dbESDI.db", strAssemblyPath);

                    //copy the file
                    System.IO.File.Copy(strAsseblyDb, strAppDataDb);
                }

            }
            catch (Exception ex)
            {
                strErrMsg = ex.ToString();
                blnRetVal = false;
            }

            return blnRetVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strDbBackupFileName"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool BackupDatabase(string strDbBackupFileName, ref string strErrMsg)
        {
            //Data Source=D:\Code\DotNet\InterfaceStudy\ESDI\ESDI\bin\Debug\Data\dbESDI.db;Version=3;
            bool blnRetVal = true;
            try
            {
                
      

                //get the connection string
                string strCnxn = GetCnxnString();
                //get the Data Source setting from the connection string
                string[] strCnxnParts = strCnxn.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                //get the path from the data source
                string[] strPathParts = strCnxnParts[0].Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                //now we have the path
                string strOldFilePath = strPathParts[1];
                //copy the old file to the backup file
                System.IO.File.Copy(strOldFilePath, strDbBackupFileName, true);
            }
            catch (Exception ex)
            {
                blnRetVal = false;
                strErrMsg = ex.ToString();
            }
            return blnRetVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtScreens"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool GetAllScreens(ref DataTable dtScreens, ref string strErrMsg)
        {         
            string strSQL = "SELECT *"
                          + "  FROM vwScreens "
                          + " ORDER BY ScreenNumber, SortOrder";
        
            return GetDataFromQuery(strSQL, ref dtScreens, ref strErrMsg);
        }

        /// <summary>
        /// Get the last screen by sort order
        /// </summary>
        /// <param name="row"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool GetLatestScreenBySortOrder(ref DataRow row, ref string strErrMsg)
        {
            bool blnRetVal = true;
            DataTable dtScreens = null;
            try
            {
                if (GetAllScreens(ref dtScreens, ref strErrMsg))
                {
                    if (dtScreens.Rows.Count>0)
                    {
                        //get the last row
                        row = dtScreens.Rows[dtScreens.Rows.Count - 1];
                    }
                }
                else
                {
                    blnRetVal = false;
                }
            }
            catch (Exception ex)
            {
                blnRetVal = false;
                strErrMsg = ex.ToString();
            }
            return blnRetVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intScreenId"></param>
        /// <param name="dtFields"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool GetFields(int intScreenId, ref DataTable dtFields, ref string strErrMsg)
        {
            string strSQL = string.Format("SELECT *"
                                        + "  FROM vwFields "
                                        + " WHERE ScreenId = {0}"
                                        + " ORDER BY id", intScreenId);

            return GetDataFromQuery(strSQL, ref dtFields, ref strErrMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intListId"></param>
        /// <param name="dtAnswerList"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool GetAnswerListDetails(int intListId, ref DataTable dtAnswerList, ref string strErrMsg)
        {
            string strSQL = string.Format( "SELECT *"
                                         + "  FROM AnswerListDetails "
                                         + " WHERE ListId = {0}"
                                         + " ORDER BY id", intListId);

            return GetDataFromQuery(strSQL, ref dtAnswerList, ref strErrMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intListId"></param>
        /// <param name="dtAnswerList"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool GetAnswerListById(int intListId, ref DataTable dtAnswerList, ref string strErrMsg)
        {
            string strSQL = string.Format("SELECT *"
                                         + "  FROM AnswerList "
                                         + " WHERE ListId = {0}"
                                         + " ORDER BY id", intListId);

            return GetDataFromQuery(strSQL, ref dtAnswerList, ref strErrMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtAnswerList"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool GetAllAnswerLists(ref DataTable dtAnswerList, ref string strErrMsg)
        {
            string strSQL = string.Format("SELECT *"
                                         + "  FROM AnswerList "
                                         + " ORDER BY Name");

            return GetDataFromQuery(strSQL, ref dtAnswerList, ref strErrMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtAnserTypes"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool GetAllAnswerTypes(ref DataTable dtAnserTypes, ref string strErrMsg)
        {
            string strSQL = string.Format("SELECT *"
                                         + "  FROM AnswerTypes"
                                         + " ORDER BY id");

            return GetDataFromQuery(strSQL, ref dtAnserTypes, ref strErrMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtControlTypes"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool GetAllControlTypes(ref DataTable dtControlTypes, ref string strErrMsg)
        {
            string strSQL = string.Format("SELECT *"
                                         + "  FROM ControlTypes"
                                         + " ORDER BY id");

            return GetDataFromQuery(strSQL, ref dtControlTypes, ref strErrMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtLayoutPatterns"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool GetAllLayoutPatterns(ref DataTable dtLayoutPatterns, ref string strErrMsg)
        {
            string strSQL = string.Format("SELECT *"
                                         + "  FROM LayoutPatterns"
                                         + " ORDER BY id");

            return GetDataFromQuery(strSQL, ref dtLayoutPatterns, ref strErrMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtTopics"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool GetAllTopics(ref DataTable dtTopics, ref string strErrMsg)
        {
            string strSQL = string.Format("SELECT *"
                                         + "  FROM Topics"
                                         + " ORDER BY Name");

            return GetDataFromQuery(strSQL, ref dtTopics, ref strErrMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strQuery"></param>
        /// <param name="dtDataTable"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool GetDataFromQuery(string strQuery, ref DataTable dtDataTable, ref string strErrMsg)
        {
            bool blnRetVal = true;
            string strCnxn = string.Empty;
            clsDbUtil objDbUtil = null;
            DataSet dsDataset = null;

            try
            {
                strCnxn = GetCnxnString();
                objDbUtil = new clsDbUtil(strCnxn);
                if (objDbUtil.open())
                {
                    dsDataset = objDbUtil.getDataSet(strQuery);

                    if (dsDataset != null)
                    {
                        dtDataTable = dsDataset.Tables[0];
                    }
                    else
                    {
                        blnRetVal = false;
                        strErrMsg = objDbUtil.ErrorMessage;
                    }
                }
                else
                {
                    blnRetVal = false;
                    strErrMsg = objDbUtil.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                blnRetVal = false;
                strErrMsg = ex.ToString();
            }
            finally
            {
                if (objDbUtil != null) objDbUtil.close();
                objDbUtil = null;
            }
            return blnRetVal;
        }

        public static bool UpdateScreen(int id, int intColumns, int intRows, int intLayoutPattern, ref string strErrMsg)
        {
            string strSQL = string.Empty;

            if (id > -1)
            {
                strSQL = string.Format("UPDATE Screens"
                                     + "   SET Columns = '{0}',"
                                     + "       Rows = {1},"
                                     + "       LayoutPattern = {2}"
                                     + " WHERE id = {3}", intColumns, intRows, intLayoutPattern, id);
            }
            else
            {
                //strSQL = string.Format("INSERT INTO AnswerListDetails (Value, ListId) VALUES ('{0}', {1})", intRows, intColumns);
            }


            return Update(strSQL, ref strErrMsg);
        }

    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="intScreenId"></param>
        /// <param name="strName"></param>
        /// <param name="strLabel"></param>
        /// <param name="intAnswerTypeId"></param>
        /// <param name="intAnswerListId"></param>
        /// <param name="strAnswerText"></param>
        /// <param name="intPosCol"></param>
        /// <param name="intPosRow"></param>
        /// <param name="intColSpan"></param>
        /// <param name="intRowSpan"></param>
        /// <param name="intColumns"></param>
        /// <param name="intRows"></param>
        /// <param name="intLayoutPattern"></param>
        /// <param name="intControlTypeId"></param>
        /// <param name="intWidth"></param>
        /// <param name="intHeight"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool UpdateField(int id, int intScreenId, string strName, string strLabel, int intAnswerTypeId, int intAnswerListId, string strAnswerText, int intPosCol, int intPosRow, int intColSpan, int intRowSpan, int intColumns, int intRows, int intLayoutPattern, int intControlTypeId, int intWidth, int intHeight, ref string strErrMsg)
        {
            string strSQL = string.Empty;

            if (id > -1)
            {
                strSQL = string.Format("UPDATE Fields"
                     + "   SET ScreenId = '{0}',"
                     + "       Name = '{1}',"
                     + "       Label = '{2}',"
                     + "       AnswerTypeId = {3},"
                     + "       AnswerListId = {4},"
                     + "       AnswerText = '{5}',"
                     + "       PosCol = {6},"
                     + "       PosRow = {7},"
                     + "       ColSpan = {8},"
                     + "       RowSpan = {9},"
                     + "       Columns = {10},"
                     + "       Rows = {11},"
                     + "       LayoutPattern = {12}"
                     + "       ControlTypeId = {13}"
                     + "       Width = {14}"
                     + "       Height = {15}"
                     + " WHERE id = '{16}'", intScreenId, strName, strLabel, intAnswerTypeId, intAnswerListId, strAnswerText, intPosCol, intPosRow, intColSpan, intRowSpan, intColumns, intRows, intLayoutPattern, intControlTypeId, intWidth, intHeight, id);
            }
            else
            {
                strSQL = string.Format("INSERT INTO Fields (ScreenId, Name, Label, AnswerTypeId, AnswerListId, AnswerText, PosCol, PosRow, ColSpan, RowSpan, Columns, Rows, LayoutPattern, ControlTypeId, Width, Height) VALUES ({0}, '{1}', '{2}', {3}, {4}, '{5}', {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15} )",  intScreenId, strName, strLabel, intAnswerTypeId, intAnswerListId, strAnswerText, intPosCol, intPosRow, intColSpan, intRowSpan, intColumns, intRows, intLayoutPattern, intControlTypeId, intWidth, intHeight);
            }


            return Update(strSQL, ref strErrMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="strName"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool UpdateAnswerList(int id, string strName, ref string strErrMsg)
        {
            string strSQL = string.Empty;

            if (id > -1)
            {
                strSQL = string.Format("UPDATE AnswerList"
                                     + "   SET Name = '{0}'"
                                     + " WHERE id = {1}",  strName, id);
            }
            else
            {
                strSQL = string.Format("INSERT INTO AnswerList (Name) VALUES ('{0}')",  strName);
            }


            return Update(strSQL, ref strErrMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool DeleteAnswerList(int id, ref string strErrMsg)
        {
            string strSQL = string.Empty;

            strSQL = string.Format("DELETE FROM AnswerList"
                                    + " WHERE id = {0}", id);

            return Update(strSQL, ref strErrMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="intListId"></param>
        /// <param name="strValue"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool UpdateAnswerListDetails(int id, int intListId, string strValue, ref string strErrMsg)
        {
            string strSQL = string.Empty;

            if (id > -1)
            {
                strSQL = string.Format("UPDATE AnswerListDetails"
                                     + "   SET Value = '{0}',"
                                     + "       ListId = {1}"
                                     + " WHERE id = {2}", strValue, intListId, id);
            }
            else
            {
                strSQL = string.Format("INSERT INTO AnswerListDetails (Value, ListId) VALUES ('{0}', {1})", strValue, intListId);
            }


            return Update(strSQL, ref strErrMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool DeleteAnswerListDetails(int id, ref string strErrMsg)
        {
            string strSQL = string.Empty;

            strSQL = string.Format("DELETE FROM AnswerListDetails"
                                    + " WHERE id = {0}", id);

            return Update(strSQL, ref strErrMsg);
        }

        public static bool UpdateTopics(int id, string strName, ref string strErrMsg)
        {
            string strSQL = string.Empty;

            if (id > -1)
            {
                strSQL = string.Format("UPDATE Topics"
                                     + "   SET Name = '{0}'"
                                     + " WHERE id = {1}", strName, id);
            }
            else
            {
                strSQL = string.Format("INSERT INTO Topics (Name) VALUES ('{0}')", strName);
            }


            return Update(strSQL, ref strErrMsg);
        }

        public static bool DeleteTopic(int id, ref string strErrMsg)
        {
            string strSQL = string.Empty;

            strSQL = string.Format("DELETE FROM Topics"
                                    + " WHERE id = {0}", id);

            return Update(strSQL, ref strErrMsg);
        }

        public static bool Delete(int id, string strTable, ref string strErrMsg)
        {
            string strSQL = string.Empty;

            strSQL = string.Format("DELETE FROM {0}"
                                    + " WHERE id = {1}", strTable, id);

            return Update(strSQL, ref strErrMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool Update(string strSQL, ref string strErrMsg)
        {
            bool blnRetVal = true;
            string strCnxn = string.Empty;
            clsDbUtil objDbUtil = null;
            IDataReader dReader = null;

            try
            {
                strCnxn = GetCnxnString();
                objDbUtil = new clsDbUtil(strCnxn);
                if (objDbUtil.open())
                {
                    //update

                    dReader = objDbUtil.getDataReader(strSQL);

                    if (dReader != null)
                    {
                    }
                    else
                    {
                        //we got an error on update
                        blnRetVal = false;
                        strErrMsg = objDbUtil.ErrorMessage;
                    }
                }
                else
                {
                    blnRetVal = false;
                    strErrMsg = objDbUtil.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                blnRetVal = false;
                strErrMsg = ex.ToString();
            }
            finally
            {
                if (objDbUtil != null) objDbUtil.close();
                objDbUtil = null;
            }
            return blnRetVal;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSettingValue"></param>
        /// <param name="strSettingName"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool GetSetting(ref string strSettingValue, string strSettingName, ref string strErrMsg)
        {
            bool blnRetVal = true;
            string strCnxn = string.Empty;
            string strSQL = string.Empty;
            clsDbUtil objDbUtil = null;
            DataSet ds = null;
            DataTable dtSensors = null;

            try
            {
                strCnxn = GetCnxnString();
                objDbUtil = new clsDbUtil(strCnxn);
                if (objDbUtil.open())
                {
                    strSQL = "SELECT SettingValue"
                           + "  FROM Settings "
                           + " WHERE SettingName = @SettingName ";

                    objDbUtil.Command.Parameters.Add(new SQLiteParameter("@SettingName", strSettingName));

                    ds = objDbUtil.getDataSet(strSQL);

                    if (ds != null)
                    {
                        dtSensors = ds.Tables[0];
                        foreach (DataRow dr in dtSensors.Rows)
                        {
                            strSettingValue = dr["SettingValue"].ToString();
                        }

                    }
                    else
                    {
                        blnRetVal = false;
                        strErrMsg = objDbUtil.ErrorMessage;
                    }
                }
                else
                {
                    blnRetVal = false;
                    strErrMsg = objDbUtil.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                blnRetVal = false;
                strErrMsg = ex.ToString();
            }
            finally
            {
                if (objDbUtil != null) objDbUtil.close();
                objDbUtil = null;
            }
            return blnRetVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSettingName"></param>
        /// <param name="strSettingValue"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool PutSetting(string strSettingName, string strSettingValue, ref string strErrMsg)
        {
            bool blnRetVal = true;
            string strCnxn = string.Empty;
            string strSQL = string.Empty;
            clsDbUtil objDbUtil = null;
            IDataReader dReader = null;

            try
            {
                strCnxn = GetCnxnString();
                objDbUtil = new clsDbUtil(strCnxn);
                if (objDbUtil.open())
                {
                    //update
                    //strSQL = "UPDATE Sensors"
                    //       + "   SET SensorName = @SensorName"
                    //       + " WHERE SensorId = @SensorId";

                    strSQL = string.Format("UPDATE Settings"
                                + "   SET SettingValue = '{0}'"
                                + " WHERE SettingName = '{1}'", strSettingValue, strSettingName);

                    objDbUtil.Command.Parameters.Add(new SQLiteParameter("@SettingName", strSettingName));

                    objDbUtil.Command.Parameters.Add(new SQLiteParameter("@SettingValue", strSettingValue));


                    dReader = objDbUtil.getDataReader(strSQL);

                    if (dReader != null)
                    {
                    }
                    else
                    {
                        //we got an error on update
                        blnRetVal = false;
                        strErrMsg = objDbUtil.ErrorMessage;
                    }
                }
                else
                {
                    blnRetVal = false;
                    strErrMsg = objDbUtil.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                blnRetVal = false;
                strErrMsg = ex.ToString();
            }
            finally
            {
                if (objDbUtil != null) objDbUtil.close();
                objDbUtil = null;
            }
            return blnRetVal;
        }

        public static bool UpdateTable(string strTable, ref DataRow row, ref string strErrMsg)
        {
            bool blnRetVal = true;
            string strSQL = string.Empty;
            int intId = int.Parse( row["id"].ToString());

            if (BuildUpdate(intId, strTable, row, ref strSQL, ref strErrMsg))
            {
                if (Update(strSQL, ref strErrMsg))
                {
                    if (intId<1)
                    {
                        //this was an insert
                        //get the latest row for this table to get the id
                        if (GetLatestRow(strTable, ref row, ref strErrMsg))
                        {
                            //Success
                        }
                        else
                        {
                            //failed to get latest row
                            blnRetVal = false;
                        }
                    }
                }
                else
                {
                    //failed to update the table
                    blnRetVal = false;
                }
            }
            else
            {
                //failed to build the update
                blnRetVal = false;
            }

            return blnRetVal;
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="intId"></param>
        /// <param name="strTable"></param>
        /// <param name="row"></param>
        /// <param name="strSQL"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool BuildUpdate(int intId, string strTable, DataRow row, ref string strSQL, ref string strErrMsg)
        {
            bool blnRetVal = true;
            DataColumnCollection lstColumns = null;
            string strInsertValues = string.Empty;
            string strInsertColumns = string.Empty;
            string strUpdateList = string.Empty;

            //get the column list
            if (GetColumns(strTable, ref lstColumns, ref strErrMsg))
            {
                //loop through all columns in this table
                foreach (DataColumn col in lstColumns)
                {
                    //get the column name
                    string strColName = col.ColumnName;
                    //if this is the id column, then skip it.  we never update the id
                    if (strColName.Trim().ToLower() == "id") continue;
                    //if the table column is not in the update list, then skip it
                    if (!row.Table.Columns.Contains(strColName)) continue;
                    //get the type
                    string strType = col.DataType.ToString().Trim().ToLower();
                    //if the type is a string, then the value needs to be in quotes
                    string strQuote = string.Empty;
                    if (strType.Contains("string")) strQuote = "'";
                    string strValue = row[strColName].ToString();
                    if (strValue == string.Empty) strValue = GetDefaultValue(strType);
                    //format the value
                    string strFormattedValue = string.Format("{0}{1}{0}, ",strQuote, strValue);
                    //add it to the insert lists
                    strInsertValues += strFormattedValue;
                    strInsertColumns += strColName + ", ";
                    //create the update pair
                    string strUpdatePair = string.Format("{0} = {1}", strColName, strFormattedValue);
                    //add it to the update list
                    strUpdateList += strUpdatePair;
                }
                //trim the last comma
                strInsertValues = strInsertValues.Substring(0, strInsertValues.Length - 2);
                strInsertColumns = strInsertColumns.Substring(0, strInsertColumns.Length - 2);
                strUpdateList = strUpdateList.Substring(0, strUpdateList.Length - 2);

                //build the update or insert
                if (intId > 0)
                {
                    strSQL = string.Format("UPDATE      {0}"
                                        + "    SET      {1}"
                                        + "  WHERE id = {2}", strTable, strUpdateList, intId);
                }
                else
                {
                    strSQL = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", strTable, strInsertColumns, strInsertValues);
                }
                
            }
            else
            {
                blnRetVal = false;
            }

            return blnRetVal;
        }

        private static string GetDefaultValue(string strType)
        {
            string strValue = string.Empty;
            if (strType.Contains("string")) strValue = string.Empty;
            else if (strType.Contains("int")) strValue = "0";
            else if (strType.Contains("float")) strValue = "0.0";


            return strValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTable"></param>
        /// <param name="lstColumns"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public static bool GetColumns(string strTable, ref DataColumnCollection lstColumns,  ref string strErrMsg)
        {
            bool blnRetVal = true;

            DataTable dtTable = null;
            string strSQL = string.Format("SELECT *"
                                       + "  FROM {0}"
                                       + " LIMIT 1", strTable);

            if (GetDataFromQuery(strSQL, ref dtTable, ref strErrMsg))
            {
                lstColumns = dtTable.Columns;
            }
            else
            {
                blnRetVal = false;
            }

            return blnRetVal;
        }

        public static bool GetEmptyRow(string strTable, ref DataRow row, ref string strErrMsg)
        {
            bool blnRetVal = true;

            DataTable dtTable = null;
            string strSQL = string.Format("SELECT *"
                                       + "  FROM {0}"
                                       + " LIMIT 1", strTable);

            if (GetDataFromQuery(strSQL, ref dtTable, ref strErrMsg))
            {
                row = dtTable.NewRow();
            }
            else
            {
                blnRetVal = false;
            }

            return blnRetVal;
        }

        public static bool GetLatestRow(string strTable, ref DataRow row, ref string strErrMsg)
        {
            bool blnRetVal = true;

            try
            {
                DataTable dtTable = null;
                string strSQL = string.Format("SELECT *"
                                            + "  FROM {0} "
                                            + " ORDER BY id DESC LIMIT 1", strTable);

                if (GetDataFromQuery(strSQL, ref dtTable, ref strErrMsg))
                {
                    if (dtTable.Rows.Count>0)
                    {
                        row = dtTable.Rows[0];
                    }
                    else
                    {
                        blnRetVal = false;
                        strErrMsg =  string.Format("There are no rows in table {0} ", strTable);
                    }
                }
                else
                {
                    blnRetVal = false;
                }
            }
            catch (Exception ex)
            {
                strErrMsg = ex.ToString();
                blnRetVal = false;
            }
            return blnRetVal;
        }
    }
}
