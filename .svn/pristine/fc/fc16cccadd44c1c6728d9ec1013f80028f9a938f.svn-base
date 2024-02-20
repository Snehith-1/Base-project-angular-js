using ems.businessteam.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;


namespace ems.businessteam.DataAccess
{
    public class DaMstStartupRequire
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, lsaudittype_value, lslms_code, lsbureau_code, lsaudittype_code;
        int mnResult;
        public void DaGetStartupRequire(MdlMstStartupRequire values)
        {
            try
            {
                msSQL = " SELECT a.startuprequire_gid,a.startuprequire_name, a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM ldm_mst_tstartuprequire a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.startuprequire_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getstartuprequire_list = new List<startuprequire_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getstartuprequire_list.Add(new startuprequire_list
                        {
                            startuprequire_gid = (dr_datarow["startuprequire_gid"].ToString()),
                            startuprequire_name = (dr_datarow["startuprequire_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.startuprequire_list = getstartuprequire_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaCreateStartupRequire(MdlMstStartupRequire values, string employee_gid)
        {
            msSQL = "select startuprequire_name from ldm_mst_tstartuprequire where startuprequire_name = '" + values.startuprequire_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Require Already Exist";
            }
            else
            {

                if (values.lms_code == null || values.lms_code == "")
                {
                    lslms_code = "";
                }
                else
                {
                    lslms_code = values.lms_code.Replace("'", "");
                }
                if (values.bureau_code == null || values.bureau_code == "")
                {
                    lsbureau_code = "";
                }
                else
                {
                    lsbureau_code = values.bureau_code.Replace("'", "");
                }



                msGetGid = objcmnfunctions.GetMasterGID("STAR");

                msSQL = " insert into ldm_mst_tstartuprequire(" +
                        " startuprequire_gid," +
                        " startuprequire_name," +
                        " lms_code," +
                        " bureau_code," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.startuprequire_name.Replace("'", "") + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Startup Require Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }
            }
        }

        public void DaEditStartupRequire(string startuprequire_gid, MdlMstStartupRequire values)
        {
            try
            {
                msSQL = " SELECT startuprequire_gid,startuprequire_name,lms_code, bureau_code, status as Status FROM ldm_mst_tstartuprequire where startuprequire_gid='" + startuprequire_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.startuprequire_gid = objODBCDatareader["startuprequire_gid"].ToString();
                    values.startuprequire_name = objODBCDatareader["startuprequire_name"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.Status = objODBCDatareader["Status"].ToString();
                }
                objODBCDatareader.Close();
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateStartupRequire(string employee_gid, MdlMstStartupRequire values)
        {


            msSQL = " update ldm_mst_tstartuprequire set " +
                 " startuprequire_name='" + values.startuprequire_name.Replace("'", "") + "'," +
                 " lms_code='" + values.lms_code + "'," +
                 " bureau_code='" + values.bureau_code + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where startuprequire_gid='" + values.startuprequire_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("STAL");

                msSQL = " insert into ldm_mst_tstartuprequirelog (" +
                       " startuprequirelog_gid, " +
                       " startuprequire_gid, " +
                       " startuprequire_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.startuprequire_gid + "'," +
                       " '" + values.startuprequire_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Startup Require Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaInactiveStartupRequire(MdlMstStartupRequire values, string employee_gid)
        {
            msSQL = " update ldm_mst_tstartuprequire set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where startuprequire_gid='" + values.startuprequire_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("STIL");

                msSQL = " insert into ldm_mst_tstartuprequireinactivelog (" +
                      " startuprequireinactivelog_gid, " +
                      " startuprequire_gid," +
                      " startuprequire_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.startuprequire_gid + "'," +
                      " '" + values.startuprequire_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Startup Require Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Startup Require Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteStartupRequire(string startuprequire_gid, string employee_gid, MdlMstStartupRequire values)
        {
            msSQL = " select startuprequire_gid from mar_trn_tmarketingcall where startuprequire_gid='" + startuprequire_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "Can't able to delete Require because it is mapped to Enquiry Call";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {

                msSQL = " select startuprequire_name from ldm_mst_tstartuprequire where enquiryrequire_gid='" + startuprequire_gid + "'";
                lsaudittype_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from ldm_mst_tstartuprequire where startuprequire_gid='" + startuprequire_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Startup Require Deleted Successfully..!";

                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }
        }


        public void DaStartupRequireInactiveLogview(string startuprequire_gid, MdlMstStartupRequire values)
        {
            try
            {
                msSQL = " SELECT a.startuprequire_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM ldm_mst_tstartuprequireinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.startuprequire_gid ='" + startuprequire_gid + "' order by a.startuprequireinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getstartuprequire_list = new List<startuprequire_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getstartuprequire_list.Add(new startuprequire_list
                        {
                            startuprequire_gid = (dr_datarow["startuprequire_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.startuprequire_list = getstartuprequire_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetStartupRequireActive(MdlMstStartupRequire values)
        {
            try
            {
                msSQL = "select startuprequire_gid,startuprequire_name from ldm_mst_tstartuprequire where status ='Y' order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getstartuprequire_list = new List<startuprequire_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getstartuprequire_list.Add(new startuprequire_list
                        {
                            startuprequire_gid = (dr_datarow["startuprequire_gid"].ToString()),
                            startuprequire_name = (dr_datarow["startuprequire_name"].ToString()),
                        });
                    }
                    values.startuprequire_list = getstartuprequire_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
    }
}