using ems.audit.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;


namespace ems.audit.DataAccess
{
    public class DaAtmMstAuditMapping
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGetauditmappingemployee_gid, lsauditmapping_value, lslms_code, lsbureau_code, lsauditmapping_code;
        int mnResult;
        public void DaGetAuditMapping(MdlAtmMstAuditMapping values)
        {
            try
            {
                msSQL = " SELECT a.auditmapping_gid,a.auditmapping_name,a.employee_name,a.employee_gid,a.auditmapping_code, a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM atm_mst_tauditmapping a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.auditmapping_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditmapping_list = new List<auditmapping_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditmapping_list.Add(new auditmapping_list
                        {
                            auditmapping_gid = (dr_datarow["auditmapping_gid"].ToString()),
                            auditmapping_name = (dr_datarow["auditmapping_name"].ToString()),

                            auditmapping_code = (dr_datarow["auditmapping_code"].ToString()),
                            lms_code = (dr_datarow["lms_code"].ToString()),
                            bureau_code = (dr_datarow["bureau_code"].ToString()),

                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    values.auditmapping_list = getauditmapping_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaCreateAuditMapping(MdlAtmMstAuditMapping values, string employee_gid)
        {
            msSQL = "select auditmapping_name from atm_mst_tauditmapping where auditmapping_name = '" + values.auditmapping_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Auditor Mapping Name Already Exist";
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

                if (values.auditmapping_code == null || values.auditmapping_code == "")
                {
                    lsauditmapping_code = "";
                }
                else
                {
                    lsauditmapping_code = values.auditmapping_code.Replace("'", "");
                }

                msGetGid = objcmnfunctions.GetMasterGID("AUDI");
                lsauditmapping_code = objcmnfunctions.GetMasterGID("IADAM");

                msSQL = " insert into atm_mst_tauditmapping(" +
                        " auditmapping_gid ," +
                        " auditmapping_name," +
                        " auditmapping_code," +
                        " lms_code," +
                        " bureau_code," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.auditmapping_name.Replace("'", "") + "'," +
                        "'" + lsauditmapping_code + "'," +
                        "'" + lslms_code + "'," +
                        "'" + lsbureau_code + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                for (var i = 0; i < values.employee.Count; i++)
                {
                    msGetauditmappingemployee_gid = objcmnfunctions.GetMasterGID("AU2E");
                    msSQL = "Insert into atm_mst_tauditmapping2employee( " +
                           " auditmapping2employee_gid, " +
                           " auditmapping_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetauditmappingemployee_gid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.employee[i].employee_gid + "'," +
                           "'" + values.employee[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Auditor Mapping Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }

            }

        }
        
        public void DaEditAuditMapping(string auditmapping_gid, MdlAtmMstAuditMapping values)
        {
            msSQL = " select auditmapping_gid,auditmapping_name,auditmapping_code,lms_code, bureau_code, status as Status from atm_mst_tauditmapping " +
                    " where auditmapping_gid='" + auditmapping_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.auditmapping_gid = objODBCDatareader["auditmapping_gid"].ToString();
                values.auditmapping_name = objODBCDatareader["auditmapping_name"].ToString();
                values.auditmapping_code = objODBCDatareader["auditmapping_code"].ToString();
                values.lms_code = objODBCDatareader["lms_code"].ToString();
                values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                values.Status = objODBCDatareader["Status"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " select auditmapping2employee_gid,employee_gid,employee_name from atm_mst_tauditmapping2employee " +
                 " where auditmapping_gid='" + auditmapping_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getemployeeList = new List<employee>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getemployeeList.Add(new employee
                    {
                        auditmapping2employee_gid = dt["auditmapping2employee_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    values.employee = getemployeeList;
                }
            }
            dt_datatable.Dispose();
            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_manageremployee = new List<employeeem_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                values.employeeem_list = dt_datatable.AsEnumerable().Select(row =>
                  new employeeem_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();

            values.status = true;


        }
        public void DaUpdateAuditMapping(string employee_gid, MdlAtmMstAuditMapping values)
        {

            msSQL = "select updated_by, updated_date,auditmapping_name from atm_mst_tauditmapping where auditmapping_gid ='" + values.auditmapping_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("AUDL");

                    msSQL = " insert into atm_mst_tauditmappinglog (" +
                           " auditmappinglog_gid, " +
                           " auditmapping_gid, " +
                           " auditmapping_name," +
                           " updated_by," +
                           " updated_date) " +
                           " values (" +
                           " '" + msGetGid + "'," +
                           " '" + values.auditmapping_gid + "'," +
                           " '" + values.auditmapping_name.Replace("'", "") + "'," +
                           " '" + employee_gid + "'," +
                           " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update atm_mst_tauditmapping set " +
                 " auditmapping_name='" + values.auditmapping_name.Replace("'", "") + "'," +              
                 " auditmapping_code='" + values.auditmapping_code + "'," +
                 " lms_code='" + values.lms_code + "'," +
                 " bureau_code='" + values.bureau_code + "'," +
                 " updated_by='" + employee_gid + "'," +
                 " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                 " where auditmapping_gid='" + values.auditmapping_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from atm_mst_tauditmapping2employee where auditmapping_gid ='" + values.auditmapping_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.employee.Count; i++)
                {
                    msGetauditmappingemployee_gid = objcmnfunctions.GetMasterGID("AU2E");
                    msSQL = "Insert into atm_mst_tauditmapping2employee( " +
                           " auditmapping2employee_gid, " +
                           " auditmapping_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetauditmappingemployee_gid + "'," +
                           "'" + values.auditmapping_gid + "'," +
                           "'" + values.employee[i].employee_gid + "'," +
                           "'" + values.employee[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
                if (mnResult != 0)
                {
                   
                    values.status = true;
                    values.message = "Auditor Mapping Updated Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Updating";
                }
            }
        

        public void DaInactiveAuditMapping(MdlAtmMstAuditMapping values, string employee_gid)
        {
            msSQL = " update atm_mst_tauditmapping set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where auditmapping_gid='" + values.auditmapping_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("ATIL");

                msSQL = " insert into atm_mst_tauditmappinginactivelog (" +
                      " auditmappinginactivelog_gid, " +
                      " auditmapping_gid," +
                      " auditmapping_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.auditmapping_gid + "'," +
                      " '" + values.auditmapping_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Auditor Mapping Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Auditor Mapping Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteAuditMapping(string auditmapping_gid, string employee_gid, MdlAtmMstAuditMapping values)
        {
           
                msSQL = " select auditmapping_name from atm_mst_tauditmapping where auditmapping_gid='" + auditmapping_gid + "'";
                lsauditmapping_value = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " delete from atm_mst_tauditmapping where auditmapping_gid='" + auditmapping_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Audit Mapping Deleted Successfully..!";
                    msGetGid = objcmnfunctions.GetMasterGID("AUDL");
                    msSQL = " insert into atm_mst_tauditmappingdeletelog(" +
                             "auditmappingdeletelog_gid, " +
                             "auditmapping_gid, "+
                             "master_name, " +
                             "master_value, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'" + auditmapping_gid + "', " +
                             "'Audit Mapping'," +
                             "'" + lsauditmapping_value + "'," +
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
        

        public void DaAuditMappingInactiveLogview(string auditmapping_gid, MdlAtmMstAuditMapping values)
        {
            try
            {
                msSQL = " SELECT a.auditmapping_gid,date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM atm_mst_tauditmappinginactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.auditmapping_gid ='" + auditmapping_gid + "' order by a.auditmappinginactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditmapping_list = new List<auditmapping_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditmapping_list.Add(new auditmapping_list
                        {
                            auditmapping_gid = (dr_datarow["auditmapping_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.auditmapping_list = getauditmapping_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetAuditMappingMaker(MdlAtmMstAuditMapping values)
        {
            try
            {

                msSQL = "select a.employee_name,a.employee_gid from atm_mst_tauditmapping2employee a " +
                   " left join atm_mst_tauditmapping b on a.auditmapping_gid = b.auditmapping_gid where b.auditmapping_name ='Maker'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditmapping_list = new List<auditmappingmaker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditmapping_list.Add(new auditmappingmaker_list
                        {
                            employee_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                        });
                    }
                    values.auditmappingmaker_list = getauditmapping_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetAuditMappingChecker(MdlAtmMstAuditMapping values)
        {
            try
            {
                msSQL = "select a.employee_name,a.employee_gid from atm_mst_tauditmapping2employee a " +
                   " left join atm_mst_tauditmapping b on a.auditmapping_gid = b.auditmapping_gid where b.auditmapping_name ='Checker'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditmapping_list = new List<auditmappingchecker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditmapping_list.Add(new auditmappingchecker_list
                        {
                            auditmapping_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                        });
                    }
                    values.auditmappingchecker_list = getauditmapping_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetAuditMappingApprover(MdlAtmMstAuditMapping values)
        {
            try
            {
                msSQL = "select a.employee_name,a.employee_gid from atm_mst_tauditmapping2employee a " +
                                 " left join atm_mst_tauditmapping b on a.auditmapping_gid = b.auditmapping_gid where b.auditmapping_name ='Approver'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditmappingapprover_list = new List<auditmappingapprover_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditmappingapprover_list.Add(new auditmappingapprover_list
                        {
                            auditmapping2employee_gid = (dr_datarow["employee_gid"].ToString()),
                            approver_name = (dr_datarow["employee_name"].ToString()),
                        });
                    }
                    values.auditmappingapprover_list = getauditmappingapprover_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }
        public void DaGetAuditChecker(string checklistmaster_gid, MdlAtmMstAuditMapping values)
        {
            try
            {
                msSQL = "select (a.auditchecker_name )as employee_name,(a.auditmapping_gid) as employee_gid from atm_trn_tauditcreation a " +
                        " left join atm_mst_tchecklistmaster b on a.checklistmaster_gid = b.checklistmaster_gid where a.checklistmaster_gid = '" + checklistmaster_gid + "'" +
                         " union " +
                        " select (b.auditchecker_name) as employee_name,(b.auditmapping_gid) as employee_gid from atm_trn_tauditcreation a " +
                        " left join atm_mst_tchecklistmaster b on a.checklistmaster_gid = b.checklistmaster_gid where a.checklistmaster_gid = '" + checklistmaster_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getauditorchecker_list = new List<auditorchecker_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getauditorchecker_list.Add(new auditorchecker_list
                        {
                            auditmapping_gid = (dr_datarow["employee_gid"].ToString()),
                            employee_name = (dr_datarow["employee_name"].ToString()),
                        });
                    }
                    values.auditorchecker_list = getauditorchecker_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch (Exception ex)
            {
                values.status = false;
            }
        }

        public void DaGetEmployeeName(string auditmapping_gid, Auditmappingemployee values)
        {
            msSQL = " select group_concat(employee_name) as employee_name  from atm_mst_tauditmapping2employee " +
                  " where auditmapping_gid='" + auditmapping_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.employee_name = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = " select auditmapping_name from atm_mst_tauditmapping" +
                 " where auditmapping_gid='" + auditmapping_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.auditmapping_name = objODBCDatareader["auditmapping_name"].ToString();
            }
            objODBCDatareader.Close();

        }
    }
}