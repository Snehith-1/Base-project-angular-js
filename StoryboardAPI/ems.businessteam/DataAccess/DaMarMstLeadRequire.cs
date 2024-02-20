using ems.businessteam.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;


namespace ems.businessteam.DataAccess
{
    public class DaMarMstLeadRequire
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, lsaudittype_value, lslms_code, lsbureau_code, lsaudittype_code;
        int mnResult;
        public void DaGetLeadRequire(MdlMarMstLeadRequire values)
        {
            try
            {
                msSQL = " SELECT a.leadrequire_gid,a.leadrequire_name, a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM mar_mst_tleadrequire a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.leadrequire_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleadrequire_list = new List<leadrequire_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getleadrequire_list.Add(new leadrequire_list
                        {
                            leadrequire_gid = (dr_datarow["leadrequire_gid"].ToString()),
                            leadrequire_name = (dr_datarow["leadrequire_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.leadrequire_list = getleadrequire_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaCreateLeadRequire(MdlMarMstLeadRequire values, string employee_gid)
        {
            msSQL = "select leadrequire_name from mar_mst_tleadrequire where leadrequire_name = '" + values.leadrequire_name.Replace("'", "\\'") + "'";
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



                msGetGid = objcmnfunctions.GetMasterGID("REQU");

                msSQL = " insert into mar_mst_tleadrequire(" +
                        " leadrequire_gid," +
                        " leadrequire_name," +
                        " lms_code," +
                        " bureau_code," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.leadrequire_name.Replace("'", "") + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Lead Require Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }
            }
        }

        public void DaEditLeadRequire(string leadrequire_gid, MdlMarMstLeadRequire values)
        {
            try
            {
                msSQL = " SELECT leadrequire_gid,leadrequire_name,lms_code, bureau_code, status as Status FROM mar_mst_tleadrequire where leadrequire_gid='" + leadrequire_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.leadrequire_gid = objODBCDatareader["leadrequire_gid"].ToString();
                    values.leadrequire_name = objODBCDatareader["leadrequire_name"].ToString();
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

        public void DaUpdateLeadRequire(string employee_gid, MdlMarMstLeadRequire values)
        {


            msSQL = " update mar_mst_tleadrequire set " +
                 " leadrequire_name='" + values.leadrequire_name.Replace("'", "") + "'," +
                 " lms_code='" + values.lms_code + "'," +
                 " bureau_code='" + values.bureau_code + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where leadrequire_gid='" + values.leadrequire_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("REQL");

                msSQL = " insert into mar_mst_tleadrequirelog (" +
                       " leadrequirelog_gid, " +
                       " leadrequire_gid, " +
                       " leadrequire_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.leadrequire_gid + "'," +
                       " '" + values.leadrequire_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Lead Require Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaInactiveLeadRequire(MdlMarMstLeadRequire values, string employee_gid)
        {
            msSQL = " update mar_mst_tleadrequire set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where leadrequire_gid='" + values.leadrequire_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("LRIL");

                msSQL = " insert into atm_mst_tleadrequireinactivelog (" +
                      " leadrequireinactivelog_gid, " +
                      " leadrequire_gid," +
                      " leadrequire_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.leadrequire_gid + "'," +
                      " '" + values.leadrequire_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Require Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Require Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteLeadRequire(string leadrequire_gid, string employee_gid, MdlMarMstLeadRequire values)
        {
            msSQL = " select leadrequire_gid from mar_trn_tmarketingcall where leadrequire_gid='" + leadrequire_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "Can't able to delete Require because it is mapped to Lead Call";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {

                msSQL = " select leadrequire_name from mar_mst_tleadrequire where leadrequire_gid='" + leadrequire_gid + "'";
                lsaudittype_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from mar_mst_tleadrequire where leadrequire_gid='" + leadrequire_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Require Deleted Successfully..!";
                  
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }
        }


        public void DaLeadRequireInactiveLogview(string leadrequire_gid, MdlMarMstLeadRequire values)
        {
            try
            {
                msSQL = " SELECT a.leadrequire_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM mar_mst_tleadrequireinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.leadrequire_gid ='" + leadrequire_gid + "' order by a.leadrequireinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleadrequire_list = new List<leadrequire_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getleadrequire_list.Add(new leadrequire_list
                        {
                            leadrequire_gid = (dr_datarow["leadrequire_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.leadrequire_list = getleadrequire_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetLeadRequireActive(MdlMarMstLeadRequire values)
        {
            try
            {
                msSQL = "select leadrequire_gid,leadrequire_name from mar_mst_tleadrequire where status ='Y' order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getleadrequire_list = new List<leadrequire_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getleadrequire_list.Add(new leadrequire_list
                        {
                            leadrequire_gid = (dr_datarow["leadrequire_gid"].ToString()),
                            leadrequire_name = (dr_datarow["leadrequire_name"].ToString()),
                        });
                    }
                    values.leadrequire_list = getleadrequire_list;
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