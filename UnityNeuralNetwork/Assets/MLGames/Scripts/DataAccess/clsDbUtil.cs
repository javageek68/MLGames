using Mono.Data.Sqlite;
using System;
using System.Data;


namespace dbUtilities
{
    public class clsDbUtil
    {

        private SqliteConnection DbCnxn = null;

        private IDataReader DbReader = null;
        private IDataAdapter DbAdapter = null;
        private IDbTransaction DbTrans = null;


        private string strCnxnString = "";
        public string CnxnString
        {
            get { return strCnxnString; }
            set { strCnxnString = value; }
        }

        protected string strErrMsg = "";
	    public string ErrorMessage {
		    get {
			    //Swap error message into a temp variable so that we can clear the
			    //main one as soon as this property is read.
			    string strTempMsg = strErrMsg;
			    strErrMsg = "";
			    return strTempMsg;
		    }
	    }



        private IDbCommand DbCmnd = null;
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public IDbCommand Command
        {
        get {
	        //If the user requests an instance, then make sure it is
	        //instantiated before we send it.
	        if (DbCmnd == null) {
                DbCmnd = DbCnxn.CreateCommand();
		        DbCmnd.Connection = this.DbCnxn;
	        }

	        return DbCmnd;
        }
        set { DbCmnd = value; }
        }

        public clsDbUtil(string strConnectionString)
        {
            strCnxnString = strConnectionString;
        }


        /// <summary>
        /// 
        /// </summary>
	    public bool beginTransaction()
	    {
            bool blnRetVal = true;
		    bool blnOpen = true;

		    //Make sure we have an open connection to the database.
		    if ((this.DbCnxn == null)) {
			    blnOpen = false;
		    } else {
			    if (this.DbCnxn.State != ConnectionState.Open) {
				    blnOpen = false;
			    }
		    }

		    if (blnOpen == false) {
			    blnOpen = this.open();
		    }

            // If we're successfully connected to the database, execute the query.
            if (blnOpen == true)
            {
                try
                {
                    this.DbTrans = this.DbCnxn.BeginTransaction();
                }
                catch (Exception myException)
                {
                    blnRetVal = false;
                    strErrMsg = "Error opening transaction : " + myException.ToString();
                }
            }
            else
            {
                blnRetVal = false;
                strErrMsg = "A transaction cannot be started with no open connection.";
            }
            return blnRetVal;
	    }


        /// <summary>
        /// 
        /// </summary>
	    public bool commitTransaction()
	    {
            bool blnRetVal = true;
		    if ((this.DbTrans != null)) {
			    try {
				    this.DbTrans.Commit();
			    } catch (Exception ex) {
                    blnRetVal = false;
				    strErrMsg = "Error committing transcation : " + ex.ToString();
			    }
		    }
            return blnRetVal;
	    }

	    /// <summary>
	    /// 
	    /// </summary>
	    public bool rollbackTransaction()
	    {
            bool blnRetVal = true;
		    if ((this.DbTrans != null)) {
			    try {
				    this.DbTrans.Rollback();
			    } catch (Exception ex) {
                    blnRetVal = false;
				    strErrMsg = "Error rolling back transcation : " + ex.ToString();
			    }
		    }
            return blnRetVal;
	    }

	
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        
        public bool open()
        {
            bool functionReturnValue = false;

            functionReturnValue = false;

            // Do we have a connection string to make a connection with?

            if (!string.IsNullOrEmpty(this.strCnxnString)) {
	            // Do we already have an open connection?  If so, don't bother.
	            if ((this.DbCnxn == null)) {
                    this.DbCnxn = new SqliteConnection(this.strCnxnString);

		            if (this.DbCnxn.State == ConnectionState.Broken) {
			            // The connection has been broken.  Close it before re-opening it.
			            this.close();
		            }

		            if (this.DbCnxn.State == ConnectionState.Closed) {
			            try {
                            strErrMsg = "Opening a connection to " + this.strCnxnString; 
				            this.DbCnxn.Open();
			            } catch (Exception ex) {
				            strErrMsg = "Got this exception when trying to open to " + this.strCnxnString + ": " + ex.ToString();
			            }

		            }
		            // If Me.oleDb_con.State = ConnectionState.Broken Or Me.oleDb_con.State = ConnectionState.Closed


	            }
	            // If IsNothing(Me.oleDb_con)

            }
            // If Me.str_connection_string <> ""


            // Are we successfully connected?
            if ((this.DbCnxn != null)) {
	            if (this.DbCnxn.State == ConnectionState.Open) {
		            functionReturnValue = true;
		            //sql command should be initialized at the sql open
		            if ((this.DbCmnd == null)) {
			            DbCmnd = this.DbCnxn.CreateCommand();
		            }
		            DbCmnd.Connection = this.DbCnxn;
	            }
            }
            return functionReturnValue;
       }


        public void close()
        {
	        // If the OleDbConnection object exists, close it & destroy it.  Also,
	        // close the dbReader.
	        if ((this.DbReader != null)) {
		        // Charles 5/4/2005 - Mike pointed out that his trace log shows that I'm sometimes
		        // closing a data reader that's already closed.  Now I check the IsClosed() method.
		        if (this.DbReader.IsClosed == false) {
			        this.DbReader.Close();
		        }
		        // If Me.oleDb_read.IsClosed() = False

		        this.DbReader = null;
	        }
	        // If Not IsNothing(Me.oleDb_read)

	        if ((this.DbCmnd != null)) {
		        this.DbCmnd = null;
	        }

	        if ((this.DbCnxn != null)) {

		        try {
			        if (this.DbCnxn.State == ConnectionState.Open) {
                        strErrMsg = "Closing the connection to " + this.strCnxnString;
				        this.DbCnxn.Close();
			        }

		        } catch (Exception ex) {
			        strErrMsg = "Got this exception when trying to close connection to " + this.strCnxnString + ": " + ex.ToString();
		        }

		        this.DbCnxn = null;
	        }

        }


        // <summary>
        // 
        // </summary>
        // <param name = "strSQL" ></ param >
        // < returns ></ returns >
        public IDataReader getDataReader(string strSQL)
        {
            IDataReader dbDataReader = null;

            bool blnOpen = true;

            //Make sure we have an open connection to the database.
            if ((this.DbCnxn == null))
            {
                blnOpen = false;
            }
            else {
                if (this.DbCnxn.State != ConnectionState.Open)
                {
                    blnOpen = false;
                }
            }

            if (blnOpen == false)
            {
                blnOpen = this.open();
            }

            // If we're successfully connected to the database, execute the query.
            if (blnOpen == true)
            {

                try
                {
                    this.Command.CommandType = CommandType.Text;

                    //MXB 9-26-2005 Added for transaction support
                    //If the user has opened a transaction, then connect it to the command
                    if ((this.DbTrans != null))
                    {
                        this.Command.Transaction = this.DbTrans;
                    }

                    //Ram 09/16/2010 for parameterizing the query
                    if (!string.IsNullOrEmpty(strSQL))
                    {
                        this.Command.CommandText = strSQL;
                    }


                    //MXB 6-28-2005 Added for transaction support
                    //If the user has opened a transaction, then connect it to the command
                    if ((this.DbTrans != null))
                    {
                        this.Command.Transaction = this.DbTrans;
                        //ram 11-03-2010 added to handle difference between updates and retrieving SQL queries
                        dbDataReader = this.Command.ExecuteReader(CommandBehavior.Default);
                    }
                    else {
                        if ((this.Command.Connection == null))
                        {
                            this.Command.Connection = this.DbCnxn;
                        }
                        dbDataReader = this.Command.ExecuteReader();
                    }
                }
                catch (Exception ex)
                {
                    dbDataReader = null;
                    strErrMsg = ex.ToString() + " : " + strSQL;
                }
            }
            return dbDataReader;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public System.Data.DataSet getDataSet(string strSQL)
	    {
		    System.Data.DataSet dsReturnValue = null;

		    dsReturnValue = new System.Data.DataSet();

		    bool blnOpen = true;

		    //Make sure we have an open connection to the database.
		    if ((this.DbCnxn == null)) {
			    blnOpen = false;
		    } else {
			    if (this.DbCnxn.State != ConnectionState.Open) {
				    blnOpen = false;
			    }
		    }

		    if (blnOpen == false) {
			    blnOpen = this.open();
		    }

		    // If we're successfully connected to the database, execute the query.
		    if (blnOpen == true) {
			    try {
                    this.Command.CommandType = CommandType.Text;

                    //MXB 9-26-2005 Added for transaction support
                    //If the user has opened a transaction, then connect it to the command
                    if (this.DbTrans != null) this.Command.Transaction = this.DbTrans;
                    //add the query to the command
                    this.Command.CommandText = strSQL;
                    //add the connection to the command
                    this.Command.Connection = this.DbCnxn;
                    //create the adapter user the command
                    this.DbAdapter = new SqliteDataAdapter((SqliteCommand)this.Command);
                    //use the adapter to fill the dataset
                    this.DbAdapter.Fill(dsReturnValue);

				    // Add the logic to insert the commands in to the SQL tables
				    //write_trace_logs_for_SQL(ref strErrMsg);
			    } catch (Exception ex) {
				    strErrMsg = ex.ToString() + " : " + strSQL;
				    dsReturnValue = null;
			    }
		    }
		    return dsReturnValue;
	    }
    }
}
