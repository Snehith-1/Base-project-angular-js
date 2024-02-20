using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

/// <summary>
/// (It's used for Business Unit Master ) Bureau API DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>
/// 
namespace ems.master.DataAccess
{
    public class DaBusinessUnit
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGetAPICode;
        int mnResult, GetApiMasterGID;
        string lsmaster_value;

        public void DaGetBusinessUnit(MdlBusinessUnit objMdlBusinessUnit)
        {
            try
            {
                msSQL = " SELECT businessunit_gid,api_code,businessunit_name,lms_code,bureau_code,status_log, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by " +
                    " from ocs_mst_tbusinessunit a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid order by businessunit_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbusinessunit = new List<businessunit_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbusinessunit.Add(new businessunit_list
                        {
                            businessunit_gid = (dr_datarow["businessunit_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            businessunit_name = (dr_datarow["businessunit_name"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objMdlBusinessUnit.businessunit_list = getbusinessunit;
                }
                dt_datatable.Dispose();
                objMdlBusinessUnit.status = true;
            }
            catch
            {
                objMdlBusinessUnit.status = false;
            }
        }

        public void DaCreateBusinessUnit(businessunit values, string employee_gid)
        {
            msSQL = "select businessunit_gid from ocs_mst_tbusinessunit where businessunit_name = '" + values.businessunit_name.Replace("'", "\\'") + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                //if (lsdocumentgid != values.businessunit_gid)
                //{
                    values.message = " This Business Unit Already Exists";
                    values.status = false;
                    return;
                //}
            }

            msGetGid = objcmnfunctions.GetMasterGID("MSBU");
            msGetAPICode = objcmnfunctions.GetApiMasterGID("BUSU");
            msSQL = " insert into ocs_mst_tbusinessunit(" +
                    " businessunit_gid," +
                    " api_code," +
                    " lms_code," +
                    " bureau_code," +
                    " businessunit_name," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetAPICode + "',";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.lms_code.Replace("'", "") + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += "'',";
            }
            else
            {
                msSQL += "'" + values.bureau_code.Replace("'", "") + "',";
            }

            msSQL += "'" + values.businessunit_name.Replace("'", "") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
            }
            else
            {
                values.status = false;
            }
        }

        public void DaEditBusinessUnit(string businessunit_gid, businessunit values)
        {
            try
            {
                msSQL = " select businessunit_gid,businessunit_name,lms_code,bureau_code,status_log from ocs_mst_tbusinessunit where businessunit_gid='" + businessunit_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                   values.businessunit_name = objODBCDatareader["businessunit_name"].ToString();
                    values.businessunit_gid = objODBCDatareader["businessunit_gid"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.status_log = objODBCDatareader["status_log"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateBusinessUnit(string employee_gid, businessunit values)
        {
            msSQL = "select businessunit_gid from ocs_mst_tbusinessunit where businessunit_name = '" + values.businessunit_name.Replace("'", "\\'") + "'";
            string lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                if (lsdocumentgid != values.businessunit_gid)
                {
                    values.message = " This Business Unit Already Exists";
                    values.status = false;
                    return;
                }
            }
            msSQL = "select updated_by, updated_date,businessunit_name from ocs_mst_tbusinessunit where businessunit_gid = '" + values.businessunit_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("SBLG");
                    msSQL = " insert into ocs_trn_tauditbusinessunitlog(" +
                              " auditbusinessunitlog_gid," +
                              " businessunit_gid," +
                              " businessunit_name, " +
                               " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.businessunit_gid + "'," +
                              "'" + objODBCDatareader["businessunit_name"].ToString() + "'," +
                               "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update ocs_mst_tbusinessunit set ";
            if (values.lms_code == "" || values.lms_code == null)
            {
                msSQL += " lms_code='',";
            }
            else
            {
                msSQL += " lms_code='" + values.lms_code + "',";
            }
            if (values.bureau_code == "" || values.bureau_code == null)
            {
                msSQL += " bureau_code='',";
            }
            else
            {
                msSQL += " bureau_code='" + values.bureau_code + "',";
            }

            msSQL += " businessunit_name='" + values.businessunit_name + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where businessunit_gid='" + values.businessunit_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
            }
            else
            {
                values.status = false;
            }
        }

        public void DaDeleteBusinessUnit(string businessunit_gid,string employee_gid, businessunit values)
        {
            msSQL = " select businessunit_name from ocs_mst_tbusinessunit where businessunit_gid='" + businessunit_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                lsmaster_value = objODBCDatareader["businessunit_name"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " select application_gid from ocs_mst_tapplication where businessunit_gid='" + businessunit_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.message = "Can't able to delete Strategic Business Unit, Because it is tagged to Application Creation";
                values.status = false;
                return;
            }
            else
            {
                objODBCDatareader.Close();
                msSQL = " delete from ocs_mst_tbusinessunit where businessunit_gid='" + businessunit_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;

                    msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                    msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                             "master_gid, " +
                             "master_name, " +
                             "master_value, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'Strategic Business Unit'," +
                             "'" + lsmaster_value + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    values.status = false;
                }
            }
        }
        //----------Get Business unit - order by ASC-----------//
        public void DaGetBusinessUnitASC(MdlBusinessUnit objMdlBusinessUnit)
        {
            try
            {
                msSQL = " SELECT businessunit_gid,businessunit_name FROM ocs_mst_tbusinessunit order by businessunit_gid asc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getbusinessunit = new List<businessunit_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getbusinessunit.Add(new businessunit_list
                        {
                            businessunit_gid = (dr_datarow["businessunit_gid"].ToString()),
                            businessunit_name = (dr_datarow["businessunit_name"].ToString()),
                        });
                    }
                    objMdlBusinessUnit.businessunit_list = getbusinessunit;
                }
                dt_datatable.Dispose();
                objMdlBusinessUnit.status = true;
            }
            catch
            {
                objMdlBusinessUnit.status = false;
            }
        }
        public void DaBusinessUnitStatusUpdate(string employee_gid, businessunit values)
        {

            msSQL = " update ocs_mst_tbusinessunit set status_log='" + values.status_log + "'," +
                " remarks='" + values.remarks.Replace("'", " ") + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where businessunit_gid='" + values.businessunit_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SBLG");
                msSQL = " insert into ocs_trn_tbusinessunitstatuslog(" +
                          " businessunitstatuslog_gid," +
                          " businessunit_gid," +
                          " status_log, " +
                          " remarks, " +
                          " created_by, " +
                          " created_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.businessunit_gid + "'," +
                          "'" + values.status_log + "'," +
                          "'" + values.remarks.Replace("'", " ") + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
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
        public void DaGetActiveLog(string businessunit_gid, MdlBusinessUnit objgetsegment)
        {
            try
            {
                msSQL = " SELECT d.businessunit_name,a.status_log,a.remarks, " +
                    " date_format(a.created_date,'%d-%m-%Y || %h:%i %p') as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by" +
                    " FROM ocs_trn_tbusinessunitstatuslog a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    "  left join ocs_mst_tbusinessunit d on a.businessunit_gid=d.businessunit_gid where a.businessunit_gid='" + businessunit_gid + "' order by a.businessunitstatuslog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<businessunit_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new businessunit_list
                        {
                            businessunit_name = (dr_datarow["businessunit_name"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objgetsegment.businessunit_list = getSegment;
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