using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DataAccess
{
    public class Upgrade
    {
        public static bool UpgradeDatabase(string strSqlitePath, string strSqlDiffPath, string strDb1, string strDb2, ref string strErrMsg)
        {
            bool blnRetVal = true;
            
            try
            {
                //right now, this transforms both the data and the schema.  but we really only want the schema from db1 and the data from db2
                string strSqlScriptFile = string.Format("update{0}.sql", System.DateTime.Now.ToString("yyyyMMddhhmmss"));
                string strSqlDiffArgs = string.Format("{0} {1} --transaction > {2}", strDb1, strDb2, strSqlScriptFile);
                string strSqliteArgs = string.Format("{0} < {1}", strDb1, strSqlScriptFile);

                //get script to transform db1 into db2
                if (RunProcess(strSqlDiffPath, strSqlDiffArgs, ref strErrMsg))
                {
                    //run script on db1
                    if (RunProcess(strSqlitePath, strSqliteArgs, ref strErrMsg))
                    {
                        //succeeded
                    }
                    else
                    {
                        //failed to run the upgrade script
                        blnRetVal = false;
                    }
                }
                else
                {
                    //failed to generate the script
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

        public static bool RunProcess(string strProcess, string strArguments, ref string strErrMsg)
        {
            bool blnRetVal = true;
            try
            {
                Process process = new Process();
                // Configure the process using the StartInfo properties.
                process.StartInfo.FileName = strProcess;
                process.StartInfo.Arguments = strArguments;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                process.WaitForExit();// Waits here for the process to exit.
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
