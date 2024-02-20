using ems.audit.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;


namespace ems.audit.DataAccess
{
    public class DaAtmMstAuditDepartment
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1, objODBCDatareader2;
        string msSQL, msGetGid, lsauditdepartment_value, lslms_code, lsbureau_code;
        string msGetdepartment_code;
        int mnResult;




        public void DaGetAuditDepartment(MdlAuditDepartment values)
        {
            try
            {
                msSQL = " SELECT a.auditdepartment_gid,a.department_code,a.auditdepartment_name, a.lms_code, a.bureau_code,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM atm_mst_tauditdepartment a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditdepartment_list = new List<auditdepartment_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditdepartment_list.Add(new auditdepartment_list
                        {
                            auditdepartment_gid = (dr_datarow["auditdepartment_gid"].ToString()),
                            department_code = (dr_datarow["department_code"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),

                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),

                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.auditdepartment_list = getauditdepartment_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaAddAuditDepartment(MdlAuditDepartment values, string employee_gid)
        {
            msGetGid = objcmnfunctions.GetMasterGID("AUDM");

            msGetdepartment_code = objcmnfunctions.GetMasterGID("IADDC");

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

            msSQL = "select auditdepartment_name from atm_mst_tauditdepartment where auditdepartment_name = '" + values.auditdepartment_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Audit Department Already Exist";
            }
            else
            {


                msSQL = " insert into atm_mst_tauditdepartment(" +
                    " auditdepartment_gid," +
                    " department_code ," +
                    " auditdepartment_name," +
                    " lms_code," +
                    " bureau_code," +

                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetdepartment_code + "', " +
                    "'" + values.auditdepartment_name.Replace("'", "") + "'," +
                    "'" + lslms_code + "'," +
                    "'" + lsbureau_code + "'," +

                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Audit Department Added Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
        }
        public void DaEditAuditDepartment(string auditdepartment_gid, MdlAuditDepartment values)
        {
            try
            {
                msSQL = " SELECT auditdepartment_gid,department_code,auditdepartment_name,lms_code, bureau_code,type, status as Status FROM atm_mst_tauditdepartment " +
                        " where auditdepartment_gid='" + auditdepartment_gid + "' ";

                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.auditdepartment_gid = objODBCDatareader["auditdepartment_gid"].ToString();
                    values.department_code = objODBCDatareader["department_code"].ToString();
                    values.auditdepartment_name = objODBCDatareader["auditdepartment_name"].ToString();

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

        public void DaUpdateAuditDepartment(string employee_gid, MdlAuditDepartment values)
        {
            msSQL = "update atm_mst_tauditdepartment set " +
                    " auditdepartment_name='" + values.auditdepartment_name.Replace("'", "") + "'," +
                    " lms_code='" + values.lms_code + "'," +
                    " bureau_code='" + values.bureau_code + "'," +

                    " updated_by='" + employee_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where auditdepartment_gid='" + values.auditdepartment_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ADLM");
                    msSQL = " insert into atm_mst_tauditdepartmentlog(" +
                              " auditdepartmentlog_gid ," +
                              " auditdepartment_gid," +
                              " department_code," +
                              " auditdepartment_name, " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.auditdepartment_gid + "'," +
                               "'" + values.auditdepartment_gid + "'," +
                              " '" + values.auditdepartment_name.Replace("'", "") + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                values.status = true;
                values.message = "Audit Department Updated Successfully";

            }      

           
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating";
            }
        }

        public void DaInactiveAuditDepartment(MdlAuditDepartment values, string employee_gid)
        {
            msSQL = " update atm_mst_tauditdepartment set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where auditdepartment_gid='" + values.auditdepartment_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ADIL");

                msSQL = " insert into  atm_mst_auditdepartmentinactivelog (" +
                      " auditdepartmentinactivelog_gid , " +
                      " auditdepartment_gid," +
                      " auditdepartment_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.auditdepartment_gid + "'," +
                      " '" + values.auditdepartment_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Audit Department Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Audit Department Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteAuditDepartment(string auditdepartment_gid, string employee_gid, result values)
        {
            msSQL = " select auditdepartment_gid from atm_mst_tchecklistmaster where auditdepartment_gid='" + auditdepartment_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                values.message = "Can't able to delete Audit department because it is mapped to Audit creation";
                values.status = false;
                objODBCDatareader.Close();
            }
            else
            {
                msSQL = " select auditdepartment_name from atm_mst_tauditdepartment where auditdepartment_gid ='" + auditdepartment_gid + "'";
                lsauditdepartment_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from atm_mst_tauditdepartment where auditdepartment_gid='" + auditdepartment_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Audit Department Deleted Successfully..!";
                    msGetGid = objcmnfunctions.GetMasterGID("ADDL");
                    msSQL = " insert into  atm_mst_tauditdepartmentdeletelog(" +
                             "auditdepartmentlogdeletelog_gid, " +
                             "auditdepartment_gid, " +
                             "type, " +
                             "auditdepartment_name, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'" + auditdepartment_gid + "', " +
                             "'Audit Department'," +
                             "'" + lsauditdepartment_value + "'," +
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

        public void DaAuditDepartmentInactiveLogview(string auditdepartment_gid, MdlAuditDepartment values)
        {
            try
            {
                msSQL = " SELECT a.auditdepartment_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM atm_mst_auditdepartmentinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.auditdepartment_gid ='" + auditdepartment_gid + "' order by a.auditdepartmentinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditdepartment_list = new List<auditdepartment_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditdepartment_list.Add(new auditdepartment_list
                        {
                            auditdepartment_gid = (dr_datarow["auditdepartment_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.auditdepartment_list = getauditdepartment_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }
        public void DaGetMappingDepartment(MdlAuditDepartment values)
        {
            try
            {
                msSQL = " SELECT a.auditdepartment_gid,a.auditdepartment_name,a.audit_name, " +
                        " date_format(a.created_date, '%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as created_by " +
                        " FROM atm_mst_tchecklistmaster a " +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid group by a.auditdepartment_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditmappingdepartment_list = new List<auditmappingdepartment_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditmappingdepartment_list.Add(new auditmappingdepartment_list
                        {
                            auditdepartment_gid = (dr_datarow["auditdepartment_gid"].ToString()),                            
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                            audit_name = (dr_datarow["audit_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString())                          
                        });
                    }
                    values.auditmappingdepartment_list = getauditmappingdepartment_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetAuditDepartmentActive(MdlAuditDepartment values)
        {
            try
            {
                msSQL = "select auditdepartment_gid,auditdepartment_name from atm_mst_tauditdepartment where status ='Y' order by created_date desc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditdepartment_list = new List<auditdepartment_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditdepartment_list.Add(new auditdepartment_list
                        {
                            auditdepartment_gid = (dr_datarow["auditdepartment_gid"].ToString()),
                            auditdepartment_name = (dr_datarow["auditdepartment_name"].ToString()),
                        });
                    }
                    values.auditdepartment_list = getauditdepartment_list;
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

