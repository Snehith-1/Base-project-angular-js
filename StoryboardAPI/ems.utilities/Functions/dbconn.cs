using System;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ems.utilities.Functions
{
    public class dbconn
    {
        private string lsConnectionString = string.Empty;

        // Get Connection String 

        public string GetConnectionString()
        {
            try
            {
                if (HttpContext.Current.Request.Headers["Authorization"] == null || HttpContext.Current.Request.Headers["Authorization"] == "null")
                {
                    lsConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ToString();
                }
                else
                {
                    if (ConfigurationManager.AppSettings["DevDBConnection"].ToString() == "No")
                    {
                        lsConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ToString();
                    }
                    else
                    {
                        using (OdbcConnection conn = new OdbcConnection(ConfigurationManager.ConnectionStrings["AuthConn"].ToString()))
                        {
                            try
                            {
                                using (OdbcCommand cmd = new OdbcCommand())
                                {
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = " CALL storyboarddb.adm_mst_spgetconnectionstring('" + HttpContext.Current.Request.Headers["Authorization"].ToString() + "')";
                                    cmd.Connection = conn;  
                                    conn.Open();
                                    lsConnectionString = cmd.ExecuteScalar().ToString();
                                    conn.Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                conn.Close();
                                lsConnectionString = "error";
                            }

                        }
                    }
                }
            }
            catch
            {
                lsConnectionString = "error";
            }
            return lsConnectionString;
        }

        // Open Connection 

        public OdbcConnection OpenConn()
        {
            try
            {
                OdbcConnection gs_ConnDB;
                gs_ConnDB = new OdbcConnection(GetConnectionString());
                if (gs_ConnDB.State != ConnectionState.Open)
                {
                    gs_ConnDB.Open();
                }
                return gs_ConnDB;
            }
            catch (Exception e)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "UnAuthorized" };
                throw new HttpResponseException(msg);
            }

        }

        // Close Connection



        public void CloseConn()
        {
            if (OpenConn().State != ConnectionState.Closed)
            {
                OpenConn().Dispose();
                OpenConn().Close();
            }
        }

        // Execute a Query

        public int ExecuteNonQuerySQL(string query, string user_gid = null, string module_reference = null, string module_name = "Log")
        {
            int mnResult = 0;
            OdbcConnection ObjODBCConnection = OpenConn();
            try
            {
                OdbcCommand cmd = new OdbcCommand(query, ObjODBCConnection);
                mnResult = cmd.ExecuteNonQuery();
                mnResult = 1;
            }
            catch (Exception e)
            {
                LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + e.Message.ToString() + "*****Query****" + query + "*******Apiref********" + module_reference, module_name);

            }
            ObjODBCConnection.Close();
            return mnResult;
        }

        // Get Scalar Value

        public string GetExecuteScalar(string query, string user_gid = null, string module_reference = null, string module_name = "Log")
        {
            string val;
            OdbcConnection ObjODBCConnection = OpenConn();
            try
            {
                OdbcCommand cmd = new OdbcCommand(query, ObjODBCConnection);
                val = cmd.ExecuteScalar().ToString();

            }
            catch (Exception e)
            {
                LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + e.Message.ToString() + "*****Query****" + query + "*******Apiref********" + module_reference, module_name);

                val = "";
            }
            ObjODBCConnection.Close();
            return val;

        }

        // Get Data Reader

        public OdbcDataReader GetDataReader(string query, string user_gid = null, string module_reference = null, string module_name = "Log")
        {
            try
            {
                OdbcCommand cmd = new OdbcCommand(query, OpenConn());
                OdbcDataReader rdr;
                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return rdr;
            }
            catch (Exception e)
            {
                LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + e.Message.ToString() + "*****Query****" + query + "*******Apiref********" + module_reference, module_name);

                return null;
            }

        }

        // Get Data Table

        public DataTable GetDataTable(string query, string user_gid = null, string module_reference = null, string module_name = "Log")
        {
            try
            {
                OdbcConnection ObjODBCConnection = OpenConn();
                DataTable dt = new DataTable();
                OdbcDataAdapter da = new OdbcDataAdapter(query, ObjODBCConnection);
                da.Fill(dt);
                ObjODBCConnection.Close();
                return dt;
            }
            catch (Exception e)
            {

                LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + e.Message.ToString() + "*****Query****" + query + "*******Apiref********" + module_reference, module_name);

                return null;
            }

        }

        // Get Data Set

        public DataSet GetDataSet(string query, string table, string user_gid = null, string module_reference = null, string module_name = "Log")
        {
            try
            {
                OdbcConnection ObjODBCConnection = OpenConn();
                DataSet ds = new DataSet();
                OdbcDataAdapter da = new OdbcDataAdapter(query, ObjODBCConnection);
                da.Fill(ds, table);
                ObjODBCConnection.Close();
                return ds;
            }
            catch (Exception e)
            {
                LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + e.Message.ToString() + "*****Query****" + query + "*******Apiref********" + module_reference, module_name);

                return null;
            }

        }
        public void LogForAudit(string strVal, string module_name)
        {

            try
            {
                string lspath = ConfigurationManager.AppSettings["file_path"].ToString() + "/erpdocument/ExceptionLOG/" + module_name + "/" + DateTime.Now.Year + @"\" + DateTime.Now.Month;
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);

                lspath = lspath + @"\" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(lspath, true);
                sw.WriteLine(strVal);
                sw.Close();
            }
            catch (Exception ex)
            {
            }
        }
    }
}