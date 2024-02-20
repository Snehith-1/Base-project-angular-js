using ems.businessteam.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;


namespace ems.businessteam.DataAccess
{
    public class DaMarMstMilletRequire
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, lsaudittype_value, lslms_code, lsbureau_code, lsaudittype_code;
        int mnResult;
        public void DaGetMilletRequire(MdlMarMstmilletRequire values)
        {
            try
            {
                msSQL = " SELECT a.milletrequire_gid,a.milletrequire_name, a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM mar_mst_tmilletrequire a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.milletrequire_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmilletrequire_list = new List<milletrequire_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmilletrequire_list.Add(new milletrequire_list
                        {
                            milletrequire_gid = (dr_datarow["milletrequire_gid"].ToString()),
                            milletrequire_name = (dr_datarow["milletrequire_name"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.milletrequire_list = getmilletrequire_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaCreateMilletRequire(MdlMarMstmilletRequire values, string employee_gid)
        {
            msSQL = "select milletrequire_name from mar_mst_tmilletrequire where milletrequire_name = '" + values.milletrequire_name.Replace("'", "\\'") + "'";
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



                msGetGid = objcmnfunctions.GetMasterGID("MRNG");

                msSQL = " insert into mar_mst_tmilletrequire(" +
                        " milletrequire_gid," +
                        " milletrequire_name," +
                        " lms_code," +
                        " bureau_code," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.milletrequire_name.Replace("'", "") + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Millet Require Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }
            }
        }

        public void DaEditMilletRequire(string milletrequire_gid, MdlMarMstmilletRequire values)
        {
            try
            {
                msSQL = " SELECT milletrequire_gid,milletrequire_name,lms_code, bureau_code, status as Status FROM mar_mst_tmilletrequire where milletrequire_gid='" + milletrequire_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.milletrequire_gid = objODBCDatareader["milletrequire_gid"].ToString();
                    values.milletrequire_name = objODBCDatareader["milletrequire_name"].ToString();
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

        public void DaUpdateMilletRequire(string employee_gid, MdlMarMstmilletRequire values)
        {


            msSQL = " update mar_mst_tmilletrequire set " +
                 " milletrequire_name='" + values.milletrequire_name.Replace("'", "") + "'," +
                 " lms_code='" + values.lms_code + "'," +
                 " bureau_code='" + values.bureau_code + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where milletrequire_gid='" + values.milletrequire_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("MRGL");

                msSQL = " insert into mar_mst_tmilletrequirelog (" +
                       " milletrequirelog_gid, " +
                       " milletrequire_gid, " +
                       " milletrequire_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.milletrequire_gid + "'," +
                       " '" + values.milletrequire_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Millet Require Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaInactiveMilletRequire(MdlMarMstmilletRequire values, string employee_gid)
        {
            msSQL = " update mar_mst_tmilletrequire set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where milletrequire_gid='" + values.milletrequire_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("MRIL");

                msSQL = " insert into atm_mst_tmilletrequireinactivelog (" +
                      " milletrequireinactivelog_gid, " +
                      " milletrequire_gid," +
                      " milletrequire_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.milletrequire_gid + "'," +
                      " '" + values.milletrequire_name + "'," +
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

        public void DaDeleteMilletRequire(string milletrequire_gid, string employee_gid, MdlMarMstmilletRequire values)
        {
            msSQL = " select milletrequire_gid from mar_trn_tmarketingcall where milletrequire_gid='" + milletrequire_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "Can't able to delete Require because it is mapped to Millet Call";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {

                msSQL = " select milletrequire_name from mar_mst_tmilletrequire where milletrequire_gid='" + milletrequire_gid + "'";
                lsaudittype_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from mar_mst_tmilletrequire where milletrequire_gid='" + milletrequire_gid + "'";
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


        public void DaMilletRequireInactiveLogview(string milletrequire_gid, MdlMarMstmilletRequire values)
        {
            try
            {
                msSQL = " SELECT a.milletrequire_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM mar_mst_tmilletrequireinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.milletrequire_gid ='" + milletrequire_gid + "' order by a.milletrequireinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmilletrequire_list = new List<milletrequire_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmilletrequire_list.Add(new milletrequire_list
                        {
                            milletrequire_gid = (dr_datarow["milletrequire_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.milletrequire_list = getmilletrequire_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetMilletRequireActive(MdlMarMstmilletRequire values)
        {
            try
            {
                msSQL = "select milletrequire_gid,milletrequire_name from mar_mst_tmilletrequire where status ='Y' order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmilletrequire_list = new List<milletrequire_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmilletrequire_list.Add(new milletrequire_list
                        {
                            milletrequire_gid = (dr_datarow["milletrequire_gid"].ToString()),
                            milletrequire_name = (dr_datarow["milletrequire_name"].ToString()),
                        });
                    }
                    values.milletrequire_list = getmilletrequire_list;
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