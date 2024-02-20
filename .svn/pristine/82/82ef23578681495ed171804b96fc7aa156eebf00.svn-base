using ems.audit.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;

namespace ems.audit.DataAccess
{
    public class DaAtmMstAuditPriority
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, lsauditpriority_value, lslms_code, lsbureau_code, lsauditpriority_code;
        int mnResult;


        public void DaGetAuditPriority(MdlAtmMstAuditPriority values)

        {
            try
            {
                msSQL = " SELECT a.auditpriority_gid,a.auditpriority_name, lms_code, bureau_code, auditpriority_code,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM atm_mst_tauditpriority a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.auditpriority_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditpriority_list = new List<auditpriority_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditpriority_list.Add(new auditpriority_list
                        {
                            auditpriority_gid = (dr_datarow["auditpriority_gid"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),

                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),
                            auditpriority_code = (dr_datarow["auditpriority_code"].ToString()),

                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.auditpriority_list = getauditpriority_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }


        public void DaCreateAuditPriority(MdlAtmMstAuditPriority values, string employee_gid)
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

            if (values.auditpriority_code == null || values.auditpriority_code == "")
            {
                lsauditpriority_code = "";
            }
            else
            {
                lsauditpriority_code = values.auditpriority_code.Replace("'", "");
            }

            msSQL = "select auditpriority_name from atm_mst_tauditpriority where auditpriority_name = '" + values.auditpriority_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Audit Priority Already Exist";
            }
            else
            {

                msGetGid = objcmnfunctions.GetMasterGID("AUPG");
                lsauditpriority_code = objcmnfunctions.GetMasterGID("IADAP");

                msSQL = " insert into atm_mst_tauditpriority(" +
                        " auditpriority_gid," +
                        " auditpriority_name," +

                        " auditpriority_code," +
                        " lms_code," +
                        " bureau_code," +

                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.auditpriority_name.Replace("'", "") + "'," +

                        "'" + lsauditpriority_code + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +

                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Audit Priority Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }
            }
        }

        public void DaEditAuditPriority(string auditpriority_gid, MdlAtmMstAuditPriority values)
        {
            try
            {
                msSQL = " SELECT auditpriority_gid,auditpriority_name,lms_code, bureau_code, auditpriority_code, status as Status FROM atm_mst_tauditpriority where auditpriority_gid='" + auditpriority_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.auditpriority_gid = objODBCDatareader["auditpriority_gid"].ToString();
                    values.auditpriority_name = objODBCDatareader["auditpriority_name"].ToString();

                    values.auditpriority_code = objODBCDatareader["auditpriority_code"].ToString();
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

        public void DaUpdateAuditPriority(string employee_gid, MdlAtmMstAuditPriority values)
        {


            msSQL = " update atm_mst_tauditpriority set " +
                 " auditpriority_name='" + values.auditpriority_name.Replace("'", "") + "'," +
                 " auditpriority_code='" + values.auditpriority_code + "'," +
                 " lms_code='" + values.lms_code + "'," +
                 " bureau_code='" + values.bureau_code + "'," +

                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where auditpriority_gid='" + values.auditpriority_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("AUTL");

                msSQL = " insert into atm_mst_tauditprioritylog (" +
                       " auditprioritylog_gid, " +
                       " auditpriority_gid, " +
                       " auditpriority_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.auditpriority_gid + "'," +
                       " '" + values.auditpriority_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Audit Priority Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaInactiveAuditPriority(MdlAtmMstAuditPriority values, string employee_gid)
        {
            msSQL = " update atm_mst_tauditpriority set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where auditpriority_gid='" + values.auditpriority_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ATIL");

                msSQL = " insert into atm_mst_tauditpriorityinactivelog (" +
                      " auditpriorityinactivelog_gid, " +
                      " auditpriority_gid," +
                      " auditpriority_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.auditpriority_gid + "'," +
                      " '" + values.auditpriority_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Audit Priority Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Audit Priority Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteAuditPriority(string auditpriority_gid, string employee_gid, MdlAtmMstAuditPriority values)
        {
            msSQL = " select auditpriority_gid from atm_trn_tauditcreation where auditpriority_gid='" + auditpriority_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "Can't able to delete Audit priority because it is mapped to Initiate audit";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {

                msSQL = " select auditpriority_name from atm_mst_tauditpriority where auditpriority_gid='" + auditpriority_gid + "'";
                lsauditpriority_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from atm_mst_tauditpriority where auditpriority_gid='" + auditpriority_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Audit Priority Deleted Successfully..!";
                    msGetGid = objcmnfunctions.GetMasterGID("ATDL");
                    msSQL = " insert into atm_mst_tauditprioritydeletelog(" +
                             "auditprioritydeletelog_gid, " +
                             "auditpriority_gid," +
                             "master_name, " +
                             "master_value, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'" + auditpriority_gid + "', " +
                             "'Audit Priority'," +
                             "'" + lsauditpriority_value + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }
        }

        public void DaAuditPriorityInactiveLogview(string auditpriority_gid, MdlAtmMstAuditPriority values)
        {
            try
            {
                msSQL = " SELECT a.auditpriority_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM atm_mst_tauditpriorityinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.auditpriority_gid ='" + auditpriority_gid + "' order by a.auditpriorityinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditpriority_list = new List<auditpriority_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditpriority_list.Add(new auditpriority_list
                        {
                            auditpriority_gid = (dr_datarow["auditpriority_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.auditpriority_list = getauditpriority_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetAuditPriorityActive(MdlAtmMstAuditPriority values)
        {
            try
            {
                msSQL = "select auditpriority_gid,auditpriority_name from atm_mst_tauditpriority where status ='Y' order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditpriority_list = new List<auditpriority_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditpriority_list.Add(new auditpriority_list
                        {
                            auditpriority_gid = (dr_datarow["auditpriority_gid"].ToString()),
                            auditpriority_name = (dr_datarow["auditpriority_name"].ToString()),
                        });
                    }
                    values.auditpriority_list = getauditpriority_list;
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
