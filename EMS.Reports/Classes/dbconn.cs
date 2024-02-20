using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Text.RegularExpressions;

namespace EMS.Reports.Classes
{
    public class dbconn
    {
        OdbcConnection gs_ConnDB;
        OdbcCommand cmdQuery;
        OdbcDataReader objreader;
        DataSet objdataset=new DataSet();

        #region ODBC Connection

        // DataBase Connection Open 
       
        public OdbcConnection openConn()
        {

            if (gs_ConnDB == null)
            {
                gs_ConnDB = new OdbcConnection(ConfigurationManager.ConnectionStrings["AuthConn"].ToString());
            }
            if (gs_ConnDB.State != ConnectionState.Open)
            {
                gs_ConnDB.Open();
            }
            return gs_ConnDB;

        }
        //Database Connection Close
        public void closeConn()
        {
            if (gs_ConnDB.State != ConnectionState.Closed)
            {
                gs_ConnDB.Close();

            }
        }

        // Execute a Query on Database

        public int ExecuteNonQuerySQL(string msSQL, OdbcConnection openconnect)
        {
            int mnResult = 0;
            cmdQuery.Connection = openconnect;
            cmdQuery.CommandText = msSQL;
            cmdQuery.CommandType = CommandType.Text;
            mnResult = cmdQuery.ExecuteNonQuery();
            return mnResult;
        }
       
        //Read a data from Database

        public OdbcDataReader GetDataReader(string query)
        {
            OdbcCommand cmd = new OdbcCommand(query, openConn());
            OdbcDataReader rdr;
            rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return rdr;
        }

        //  Get Dataset from the data of Database

        public DataSet GetDataSet(string msSQL, string tblname)
        {
            OdbcDataAdapter objadapter = new OdbcDataAdapter(msSQL, openConn());
            objadapter.Fill(objdataset, tblname);
            return objdataset;
        }

        // Get Datatable from Database

        public DataTable GetDAtaTable(string msSQL, OdbcConnection openconnect)
        {
            OdbcDataAdapter objDataAdapter = new OdbcDataAdapter(msSQL, openconnect);
            DataTable objDatatable = new DataTable();
            objDataAdapter.Fill(objDatatable);
            return objDatatable;

        }

        // Get DataTable via SP

        public DataTable GetDataTableSP(string storedProcedureName, OdbcParameter arrParam, OdbcConnection openconnect)
        {

            cmdQuery.CommandType = CommandType.StoredProcedure;
            cmdQuery.CommandText = storedProcedureName;
            if (arrParam != null)
            {
                cmdQuery.Parameters.Add(new OdbcParameter("employee_gid", arrParam.Value));

            }
            OdbcDataAdapter objdataAdapter = new OdbcDataAdapter(cmdQuery);
            DataTable objdatatable = new DataTable();
            objdataAdapter.Fill(objdatatable);
            return objdatatable;
        }

        // Get Scalar Value

        public string GetExecuteScalar(string query)
        {
            string val;
            OdbcConnection ObjODBCConnection = openConn();
            try
            {
                OdbcCommand cmd = new OdbcCommand(query, ObjODBCConnection);
                val = cmd.ExecuteScalar().ToString();

            }
            catch
            {
                val = "";
            }
            ObjODBCConnection.Close();
            return val;

        }

        // Split By Expression

        public string[] split(string input, string pattern)
        {
            string[] elements = Regex.Split(input, pattern);
            return elements;
        }

        public string ConvertToAscii(string str)
        {
            int iIndex;
            int lenOfUserString;
            string newUserPass = string.Empty;
            string tmp;
            lenOfUserString = str.Length;
            for (iIndex = 0; iIndex < lenOfUserString; iIndex++)
            {
                tmp = str.Substring(iIndex, 1);
                tmp = (((int)Convert.ToChar(tmp)) - lenOfUserString).ToString();
                newUserPass = newUserPass + (tmp.Length < 3 ? "0" : "") + tmp;
            }
            return newUserPass;
        }


        #endregion

    }
}