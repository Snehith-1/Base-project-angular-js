using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.ecms.Models;
using ems.utilities.Functions;
using System.Data;
using System.Data.Odbc;

namespace ems.ecms.DataAccess
{
    /// <summary>
    /// security Controller Class containing API methods for accessing the  DataAccess class DaSecurityType
    ///   Security Type - create, update, edit, delete, Active log, status update, view
    /// </summary>
    /// <remarks>Written by Sundar Rajan </remarks>
    public class DaSecurityType
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunction = new cmnfunctions();
        OdbcDataReader objOdbcDataReader, objODBCDatareader;
        string mssql, msGetGid, msGetGIDREF, msGetAPICode, msSQL;
        DataTable dt_datatable;
        int mnresult;
        string lsmaster_value, lsdocumentgid;

        public void DaPostcreateSecurityType(securitytype values, string user_gid)
        {
            msSQL = "select security_type from ocs_trn_tsecuritytype where security_type = '" + values.security_type.Replace("'", "\\'") + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Security Type Already Exist";
            }
            else
            {
                msGetGid = objcmnfunction.GetMasterGID("SECU");
                // msGetGIDREF = objcmnfunction.GetMasterGID("SEC0");
                msGetAPICode = objcmnfunction.GetApiMasterGID("SECT");
                mssql = " insert into ocs_trn_tsecuritytype(" +
                        " securitytype_gid," +
                        " api_code," +
                        " lms_code," +
                        " bureau_code," +
                        " security_type," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                       "'" + msGetAPICode + "',";
                if (values.lms_code == "" || values.lms_code == null)
                {
                    mssql += "'',";
                }
                else
                {
                    mssql += "'" + values.lms_code.Replace("'", "") + "',";
                }
                if (values.bureau_code == "" || values.bureau_code == null)
                {
                    mssql += "'',";
                }
                else
                {
                    mssql += "'" + values.bureau_code.Replace("'", "") + "',";
                }

                mssql += "'" + values.security_type.Replace("'", "") + "'," +
                        "'" + user_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnresult = objdbconn.ExecuteNonQuerySQL(mssql);

                if (mnresult != 0)
                {
                    values.status = true;
                }
                else
                {
                    values.status = false;
                }
            }
        }
        public void DaGetSecuritytype(MdlSecurity objSecuritytype)
        {
            try
            {
                mssql = " select securitytype_gid,api_code,security_type,lms_code,bureau_code,status_log, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by " +
                    " from ocs_trn_tsecuritytype a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid" +
                    " order by securitytype_gid desc ";

                dt_datatable = objdbconn.GetDataTable(mssql);
                var getSecurity = new List<securitytype>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSecurity.Add(new securitytype
                        {
                            securitytype_gid = (dr_datarow["securitytype_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            security_type = (dr_datarow["security_type"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objSecuritytype.securitytype_list = getSecurity;
                }
                dt_datatable.Dispose();
            }
            catch
            {

            }
           
        }

        public bool DaGetEditSecuritytype(string securitytype_gid, securitytype values)
        {
            mssql = " select securitytype_gid,security_type,lms_code,bureau_code,status_log  " +
                    " from ocs_trn_tsecuritytype where securitytype_gid='" + securitytype_gid + "' ";
           
            objOdbcDataReader = objdbconn .GetDataReader(mssql);
            if (objOdbcDataReader.HasRows)
            {                
                values.security_type = objOdbcDataReader["security_type"].ToString();
                values.lms_code = objOdbcDataReader["lms_code"].ToString();
                values.bureau_code = objOdbcDataReader["bureau_code"].ToString();
                values.status_log = objOdbcDataReader["status_log"].ToString();
            }
            objOdbcDataReader.Close();
           
            return true;
        }

        public void DaPostUpdateSecurityType(string user_gid, securitytype values)
        {
            mssql = "select updated_by, updated_date,security_type from ocs_trn_tsecuritytype where securitytype_gid = '" + values.securitytype_gid + "'";
            objOdbcDataReader = objdbconn.GetDataReader(mssql);

            if (objOdbcDataReader.HasRows == true)
            {
                string lsUpdatedBy = objOdbcDataReader["updated_by"].ToString();
                string lsUpdatedDate = objOdbcDataReader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunction.GetMasterGID("SLOG");
                    mssql = " insert into ocs_trn_tauditsecuritytypelog(" +
                              " auditsecuritytypelog_gid," +
                              " securitytype_gid," +
                              " security_type, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.securitytype_gid + "'," +
                              "'" + objOdbcDataReader["security_type"].ToString() + "'," +
                              "'" + user_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnresult = objdbconn.ExecuteNonQuerySQL(mssql);
                }
            }
            objOdbcDataReader.Close();
            mssql = " update ocs_trn_tsecuritytype set ";
            msSQL = "select securitytype_gid from ocs_mst_tdesignation where security_type = '" + values.security_type.Replace("'", "\\'") + "'";
            lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                if (lsdocumentgid != values.securitytype_gid)
                {
                    values.message = "Security Type Already Exist";
                    values.status = false;
                    return;
                }
            }
            if (values.lms_code == "" || values.lms_code == null)
            {
                mssql += " lms_code='',";
            }
            else
            {
                mssql += " lms_code='" + values.lms_code + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                mssql += " bureau_code='',";
            }
            else
            {
                mssql += " bureau_code='" + values.bureau_code + "',";
            }

            mssql += " security_type='" + values.security_type + "'," +                  
                    " updated_by='" + user_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where securitytype_gid='" + values.securitytype_gid + "' ";
            mnresult = objdbconn .ExecuteNonQuerySQL(mssql);

        
            if (mnresult != 0)
            {
                values.status = true;
            }
            else
            {
                values.status = false;
            }
       
        }

        public void DaPostDeleteSecuritytype(string securitytype_gid,string employee_gid, securitytype values)
        {

            mssql = " select security_type from ocs_trn_tsecuritytype where securitytype_gid='" + securitytype_gid + "'";
            lsmaster_value = objdbconn.GetExecuteScalar(mssql);
            mssql = " delete from ocs_trn_tsecuritytype where securitytype_gid='" + securitytype_gid + "'";
            mnresult = objdbconn .ExecuteNonQuerySQL(mssql);
          
            if (mnresult != 0)
            {
                values.status = true;
                values.message = "Security Type Deleted Successfully..!";
                msGetGid = objcmnfunction.GetMasterGID("MSTD");
                mssql = " insert into ocs_mst_tmasterdeletelog(" +
                         "master_gid, " +
                         "master_name, " +
                         "master_value, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'Security Type'," +
                         "'" + lsmaster_value + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnresult = objdbconn.ExecuteNonQuerySQL(mssql);
            }
            else
            {
                values.status = false;
                values.message = "Error While Deleting";
            }
        }
        public void DasecurityTypeStatusUpdate(string employee_gid, securitytype values)
        {

            mssql = " update ocs_trn_tsecuritytype set status_log='" + values.status_log + "'," +
                " remarks='" + values.remarks.Replace("'", " ") + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where securitytype_gid='" + values.securitytype_gid + "' ";
            mnresult = objdbconn.ExecuteNonQuerySQL(mssql);

            if (mnresult != 0)
            {
                msGetGid = objcmnfunction.GetMasterGID("SLOG");
                mssql = " insert into ocs_trn_tsecuritytypestatuslog(" +
                          " securitytypestatuslog_gid," +
                          " securitytype_gid," +
                          " status_log, " +
                          " remarks, " +
                          " created_by, " +
                          " created_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.securitytype_gid + "'," +
                          "'" + values.status_log + "'," +
                          "'" + values.remarks.Replace("'", " ") + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnresult = objdbconn.ExecuteNonQuerySQL(mssql);
                values.message = "Status Updated Successfully";
                values.status = true;
            }
            else
            {
                values.message = "Error Occured while updating Status";
                values.status = false;
            }
        }
        //Get Active Status Log
        public void DaGetActiveLog(string securitytype_gid, MdlSecurity objgetsegment)
        {
            try
            {
                mssql = " SELECT d.security_type,a.status_log,a.remarks, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by" +
                    " FROM ocs_trn_tsecuritytypestatuslog a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    "  left join  ocs_trn_tsecuritytype d on a.securitytype_gid=d.securitytype_gid where a.securitytype_gid='" + securitytype_gid + "' order by a.securitytypestatuslog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(mssql);
                var getSegment = new List<securitytype>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new securitytype
                        {
                            security_type = (dr_datarow["security_type"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objgetsegment.securitytype_list = getSegment;
                }
                dt_datatable.Dispose();
                objgetsegment.status = true;

            }
            catch
            {
                objgetsegment.status = false;
            }
        }
    }
}