using ems.audit.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;


namespace ems.audit.DataAccess
{
    public class DaAtmMstAuditType
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, lsaudittype_value, lslms_code, lsbureau_code, lsaudittype_code;
        int mnResult;
        public void DaGetAuditType(MdlAtmMstAuditType values)
        {
            try
            {
                msSQL = " SELECT a.audittype_gid,a.audittype_name, a.audittype_code, a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM atm_mst_taudittype a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.audittype_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getaudittype_list = new List<audittype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getaudittype_list.Add(new audittype_list
                        {
                            audittype_gid = (dr_datarow["audittype_gid"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),

                            audittype_code = (dr_datarow["audittype_code"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),

                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.audittype_list = getaudittype_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaCreateAuditType(MdlAtmMstAuditType values, string employee_gid)
        {
            msSQL = "select audittype_name from atm_mst_taudittype where audittype_name = '" + values.audittype_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Audit Type Already Exist";
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


  
                msGetGid = objcmnfunctions.GetMasterGID("AUTY");
                lsaudittype_code = objcmnfunctions.GetMasterGID("IADTY");

                msSQL = " insert into atm_mst_taudittype(" +
                        " audittype_gid," +
                        " audittype_name," +
                        " audittype_code," +
                        " lms_code," +
                        " bureau_code," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.audittype_name.Replace("'", "") + "'," +
                        "'" + lsaudittype_code + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);                

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Audit Type Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }
            }
        }

        public void DaEditAuditType(string audittype_gid, MdlAtmMstAuditType values)
        {
            try
            {
                msSQL = " SELECT audittype_gid,audittype_name,audittype_code,lms_code, bureau_code, status as Status FROM atm_mst_taudittype where audittype_gid='" + audittype_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.audittype_gid = objODBCDatareader["audittype_gid"].ToString();
                    values.audittype_name = objODBCDatareader["audittype_name"].ToString();

                    values.audittype_code = objODBCDatareader["audittype_code"].ToString();
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

        public void DaUpdateAuditType(string employee_gid, MdlAtmMstAuditType values)
        {


            msSQL = " update atm_mst_taudittype set " +
                 " audittype_name='" + values.audittype_name.Replace("'", "") + "'," +

                 " audittype_code='" + values.audittype_code + "'," +
                 " lms_code='" + values.lms_code + "'," +
                 " bureau_code='" + values.bureau_code + "'," +

                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where audittype_gid='" + values.audittype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("AUTL");

                msSQL = " insert into atm_mst_taudittypelog (" +
                       " audittypelog_gid, " +
                       " audittype_gid, " +
                       " audittype_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.audittype_gid + "'," +
                       " '" + values.audittype_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Audit Type Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }

        public void DaInactiveAuditType(MdlAtmMstAuditType values, string employee_gid)
        {
            msSQL = " update atm_mst_taudittype set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where audittype_gid='" + values.audittype_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ATIL");

                msSQL = " insert into atm_mst_taudittypeinactivelog (" +
                      " audittypeinactivelog_gid, " +
                      " audittype_gid," +
                      " audittype_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.audittype_gid + "'," +
                      " '" + values.audittype_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Audit Type Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Audit Type Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteAuditType(string audittype_gid, string employee_gid, MdlAtmMstAuditType values)
        {
            msSQL = " select audittype_gid from atm_mst_tchecklistmaster where audittype_gid='" + audittype_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "Can't able to delete Audit Type because it is mapped to Audit creation";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {

                msSQL = " select audittype_name from atm_mst_taudittype where audittype_gid='" + audittype_gid + "'";
                lsaudittype_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from atm_mst_taudittype where audittype_gid='" + audittype_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Audit Type Deleted Successfully..!";
                    msGetGid = objcmnfunctions.GetMasterGID("ATDL");
                    msSQL = " insert into atm_mst_taudittypedeletelog(" +
                             "audittypedeletelog_gid, " +
                             "audittype_gid, " +
                             "master_name, " +
                             "master_value, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'" + audittype_gid + "', " +
                             "'Audit Type'," +
                             "'" + lsaudittype_value + "'," +
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
        

        public void DaAuditTypeInactiveLogview(string audittype_gid, MdlAtmMstAuditType values)
        {
            try
            {
                msSQL = " SELECT a.audittype_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM atm_mst_taudittypeinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.audittype_gid ='" + audittype_gid + "' order by a.audittypeinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getaudittype_list = new List<audittype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getaudittype_list.Add(new audittype_list
                        {
                            audittype_gid = (dr_datarow["audittype_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.audittype_list = getaudittype_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetAuditTypeActive(MdlAtmMstAuditType values)
        {
            try
            {
                msSQL = "select audittype_gid,audittype_name from atm_mst_taudittype where status ='Y' order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getaudittype_list = new List<audittype_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getaudittype_list.Add(new audittype_list
                        {
                            audittype_gid = (dr_datarow["audittype_gid"].ToString()),
                            audittype_name = (dr_datarow["audittype_name"].ToString()),
                        });
                    }
                    values.audittype_list = getaudittype_list;
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