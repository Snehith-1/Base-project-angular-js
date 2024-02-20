using ems.audit.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;

namespace ems.audit.DataAccess
{
    public class DaAtmMstAuditFrequency 
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, lsauditfrequency_value, lslms_code, lsbureau_code, lsauditfrequency_code;
        int mnResult;
        public void DaGetAuditFrequency(MdlAtmMstAuditFrequency values)
        {
            try
            {
                msSQL = " SELECT a.auditfrequency_gid,a.auditfrequency_name,a.auditfrequency_code, a.lms_code, a.bureau_code,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM atm_mst_tauditfrequency a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.auditfrequency_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditfrequency_list = new List<auditfrequency_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditfrequency_list.Add(new auditfrequency_list
                        {
                            auditfrequency_gid = (dr_datarow["auditfrequency_gid"].ToString()),
                            auditfrequency_name = (dr_datarow["auditfrequency_name"].ToString()),

                            auditfrequency_code = (dr_datarow["auditfrequency_code"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),

                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.auditfrequency_list = getauditfrequency_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaCreateAuditFrequency(MdlAtmMstAuditFrequency values, string employee_gid)
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

            if (values.auditfrequency_code == null || values.auditfrequency_code == "")
            {
                lsauditfrequency_code = "";
            }
            else
            {
                lsauditfrequency_code = values.auditfrequency_code.Replace("'", "");
            }

            msSQL = "select auditfrequency_name from atm_mst_tauditfrequency where auditfrequency_name = '" + values.auditfrequency_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Audit Frequency Already Exist";
            }
            else
            {


                msGetGid = objcmnfunctions.GetMasterGID("AUFG");
                lsauditfrequency_code = objcmnfunctions.GetMasterGID("IADAF");

                msSQL = " insert into atm_mst_tauditfrequency(" +
                        " auditfrequency_gid," +
                        " auditfrequency_name," +

                        " auditfrequency_code," +
                        " lms_code," +
                        " bureau_code," +

                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.auditfrequency_name.Replace("'", "") + "'," +

                        "'" + lsauditfrequency_code + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +

                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Audit Frequency Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }
            }
        }

        public void DaEditAuditFrequency(string auditfrequency_gid, MdlAtmMstAuditFrequency values)
        {
            try
            {
                msSQL = " SELECT auditfrequency_gid,auditfrequency_name,auditfrequency_code,lms_code, bureau_code, status as Status FROM atm_mst_tauditfrequency where auditfrequency_gid='" + auditfrequency_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.auditfrequency_gid = objODBCDatareader["auditfrequency_gid"].ToString();
                    values.auditfrequency_name = objODBCDatareader["auditfrequency_name"].ToString();

                    values.auditfrequency_code = objODBCDatareader["auditfrequency_code"].ToString();
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


        public void DaUpdateAuditFrequency(string employee_gid, MdlAtmMstAuditFrequency values)
        {


            msSQL = " update atm_mst_tauditfrequency set " +
                 " auditfrequency_name='" + values.auditfrequency_name.Replace("'", "") + "'," +

                 " auditfrequency_code='" + values.auditfrequency_code + "'," +
                 " lms_code='" + values.lms_code + "'," +
                 " bureau_code='" + values.bureau_code + "'," +

                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where auditfrequency_gid='" + values.auditfrequency_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("AUFL");

                msSQL = " insert into atm_mst_tauditfrequencylog (" +
                       " auditfrequencylog_gid, " +
                       " auditfrequency_gid, " +
                       " auditfrequency_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.auditfrequency_gid + "'," +
                       " '" + values.auditfrequency_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Audit Frequency Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaInactiveAuditFrequency(MdlAtmMstAuditFrequency values, string employee_gid)
        {
            msSQL = " update atm_mst_tauditfrequency set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where auditfrequency_gid='" + values.auditfrequency_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("AFIL");

                msSQL = " insert into atm_mst_tauditfrequencyinactivelog (" +
                      " auditfrequencyinactivelog_gid, " +
                      " auditfrequency_gid," +
                      " auditfrequency_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.auditfrequency_gid + "'," +
                      " '" + values.auditfrequency_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Audit Frequency Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Audit Frequency Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteAuditFrequency(string auditfrequency_gid, string employee_gid, MdlAtmMstAuditFrequency values)
        {


            msSQL = " select auditfrequency_name from atm_mst_tauditfrequency where auditfrequency_gid='" + auditfrequency_gid + "'";
            lsauditfrequency_value = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " delete from atm_mst_tauditfrequency where auditfrequency_gid='" + auditfrequency_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Audit Frequency Deleted Successfully..!";
                msGetGid = objcmnfunctions.GetMasterGID("AFDL");
                msSQL = " insert into atm_mst_tauditfrequencydeletelog(" +
                         "auditfrequencydeletelog_gid, " +
                         "auditfrequency_gid, "+
                         "master_name, " +
                         "master_value, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'" + auditfrequency_gid + "', " +
                         "'Entity'," +
                         "'" + lsauditfrequency_value + "'," +
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


        public void DaAuditFrequencyInactiveLogview(string auditfrequency_gid, MdlAtmMstAuditFrequency values)
        {
            try
            {
                msSQL = " SELECT a.auditfrequency_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM atm_mst_tauditfrequencyinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.auditfrequency_gid ='" + auditfrequency_gid + "' order by a.auditfrequencyinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditfrequency_list = new List<auditfrequency_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditfrequency_list.Add(new auditfrequency_list
                        {
                            auditfrequency_gid = (dr_datarow["auditfrequency_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.auditfrequency_list = getauditfrequency_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }


    }
}
