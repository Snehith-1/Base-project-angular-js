using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.ecms.Models;
using ems.utilities.Functions;
namespace ems.ecms.DataAccess
{
    /// <summary>
    /// covenanttype Controller Class containing API methods for accessing the  DataAccess class DaCovenanttype 
    /// To create covenant type, update covenat type, delete covenant type, summary of covenant type
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class DaCovenanttype
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunction = new cmnfunctions();
        OdbcDataReader objOdbcDataReader;
        DataTable dt_datatable;
    
        string mssql, msGetGid;
        int mnresult;
        public void DaPostCreateCovenanttype(mdlcreateconvenantType values,string employee_gid)
        {
           
            msGetGid = objcmnfunction.GetMasterGID("CNTE");
            mssql  = " insert into ocs_mst_tcovenanttype(" +
                 " covenanttype_gid," +
                 " covenanttype_code," +
                 " covenanttype_name," +
                 " criticallity," +
                 " comments," +
                 " created_by," +
                 " created_date)" +
                 " values(" +
                 "'" + msGetGid + "'," +
                 "'" + values.covenanttype_code .Replace("'", "").ToUpper () + "'," +
                 "'" + values.covenanttype_name .Replace("'", "") + "'," +
                 "'" + values.criticallity + "'," +
                 "'" + values.comments.Replace("'", "") + "'," +
                 "'" + employee_gid + "'," +
                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnresult = objdbconn.ExecuteNonQuerySQL(mssql);
            if (mnresult != 0)
            {
                values.message = "success";
                values.status = true;
            }
            else
            {
                values.message = "failure";
                values.status = true;
            }
        }
        public void DaGetCovenanttype(MdlCovenanttype objcovenanttype)
        {
            try
            {
                mssql = " select covenanttype_gid,covenanttype_code,"+
                        " covenanttype_name,criticallity,comments" +
                        " from ocs_mst_tcovenanttype order by created_date desc";

                dt_datatable = objdbconn.GetDataTable(mssql);
                var get_employee = new List<covenanttype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_employee.Add(new covenanttype_list
                        {
                            covenanttype_gid = (dr_datarow["covenanttype_gid"].ToString()),
                            covenanttype_code = (dr_datarow["covenanttype_code"].ToString()),
                            covenanttype_name = (dr_datarow["covenanttype_name"].ToString()),
                            criticallity = (dr_datarow["criticallity"].ToString()),
                            comments = (dr_datarow["comments"].ToString()),
                        });
                    }
                    objcovenanttype.covenanttype_list = get_employee;
                }
                dt_datatable.Dispose();
                objcovenanttype.status = true;


            }
            catch
            {
                objcovenanttype.status = false;
            }
            
           
        }
        public void DaGetEditCovenanttype(string covenanttype_gid,covenantypeedit values)
        {
            try
            {
                mssql = " select covenanttype_code,covenanttype_name,criticallity,comments"+
                        " from ocs_mst_tcovenanttype where covenanttype_gid='" + covenanttype_gid + "' ";

                objOdbcDataReader = objdbconn.GetDataReader(mssql);
                if (objOdbcDataReader.HasRows)
                {
                    values.covenantTypecodeedit = objOdbcDataReader["covenanttype_code"].ToString();
                    values.covenantTypenameedit = objOdbcDataReader["covenanttype_name"].ToString();
                    values.criticallity = objOdbcDataReader["criticallity"].ToString();
                    values.comments = objOdbcDataReader["comments"].ToString();
                    values.covenanttype_gid = covenanttype_gid;
                }
                objOdbcDataReader.Close();
                values.status = true;
                values.message = "success";
            }
            catch
            {
                values.status = false;
                values.message = "failure";

            }
            
        }

        public void DaPostUpdateCovenanttype(string employee_gid,covenantypeedit values)
        {
           
            mssql = " update ocs_mst_tcovenanttype set " +
                 " covenanttype_code='" + values.covenantTypecodeedit + "'," +
                 " covenanttype_name='" + values.covenantTypenameedit + "'," +
                 " criticallity='" + values.criticallity + "'," +
                 " comments='" + values.comments.Replace("'", "") + "'," +
                  " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where covenanttype_gid='" + values.covenanttype_gid +"' ";
            mnresult = objdbconn .ExecuteNonQuerySQL(mssql);

            mssql = " update ocs_trn_tdeferral set " +
                   " covenanttype_name='" + values.covenantTypenameedit + "'," +
                   " criticallity='" + values.criticallity + "'" +
                   " where covenanttype_gid='" + values.covenanttype_gid + "' ";
            mnresult = objdbconn .ExecuteNonQuerySQL(mssql);

            mssql = " update ocs_trn_tdeferral2loan set " +
                    " covenanttype_name='" + values.covenantTypenameedit + "'," +
                    " criticallity='" + values.criticallity + "'" +
                    " where covenanttype_gid='" + values.covenanttype_gid + "' ";
            mnresult = objdbconn.ExecuteNonQuerySQL(mssql);

            if(mnresult ==1)
            {
                values.status = true;
                values.message = "success";
            }
           else
            {
                values.status = false;
                values.message = "failure";
            }
        }

        public void DaPostDeleteCovenanttype(string covenanttype_gid, covenantypeedit values)
        {
            
            mssql = " delete from ocs_mst_tcovenanttype where covenanttype_gid='" + covenanttype_gid + "'";
            mnresult = objdbconn.ExecuteNonQuerySQL(mssql);
          
            if (mnresult != 0)
            {
                values.status = true;
                values.message = "success";
            }
            else
            {
                values.status = true;
                values.message = "failure";
            }
        }

    }
}