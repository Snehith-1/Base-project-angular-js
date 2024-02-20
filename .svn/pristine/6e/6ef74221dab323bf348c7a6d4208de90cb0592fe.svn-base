using ems.hrloan.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;

namespace ems.hrloan.DataAccess
{
    public class DaMstHRLoanHRMappingApprovals
    {      
            dbconn objdbconn = new dbconn();
            cmnfunctions objcmnfunctions = new cmnfunctions();
            DataTable dt_datatable;
            OdbcDataReader objODBCDatareader;
            string msSQL, msGetGid, msGethrmappingemployee_gid, lslms_code, lsbureau_code, lshrmapping_code;
            int mnResult;
            public void DaGetHRMapping(MdlMstHRLoanHRMappingApprovals values)
            {
                try
                {
                    msSQL = " SELECT a.hrmapping_gid,a.hrmapping_name,a.employee_name,a.employee_gid,a.hrmapping_code, a.lms_code, a.bureau_code, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                            " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                            " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                            " FROM hrl_mst_thrmapping a" +
                            " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.hrmapping_gid desc ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var gethrmapping_list = new List<hrmapping_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            gethrmapping_list.Add(new hrmapping_list
                            {
                                hrmapping_gid = (dr_datarow["hrmapping_gid"].ToString()),
                                hrmapping_name = (dr_datarow["hrmapping_name"].ToString()),

                                hrmapping_code = (dr_datarow["hrmapping_code"].ToString()),
                                lms_code = (dr_datarow["lms_code"].ToString()),
                                bureau_code = (dr_datarow["bureau_code"].ToString()),

                                employee_gid = (dr_datarow["employee_gid"].ToString()),
                                employee_name = (dr_datarow["employee_name"].ToString()),
                                created_by = (dr_datarow["created_by"].ToString()),
                                created_date = (dr_datarow["created_date"].ToString()),
                                status = (dr_datarow["status"].ToString()),
                            });
                        }
                        values.hrmapping_list = gethrmapping_list;
                    }
                    dt_datatable.Dispose();
                    values.status = true;
                }
                catch
                {
                    values.status = false;
                }
            }

            public void DaCreateHRMapping(MdlMstHRLoanHRMappingApprovals values, string employee_gid)
            {
                msSQL = "select hrmapping_name from hrl_mst_thrmapping where hrmapping_name = '" + values.hrmapping_name + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.status = false;
                    values.message = "HR mapping already exist";
                }
                else
                {
                    if (values.lms_code == null || values.lms_code == "")
                    {
                        lslms_code = "";
                    }
                    else
                    {
                        lslms_code = values.lms_code.Replace("'", @"\'");
                    }
                    if (values.bureau_code == null || values.bureau_code == "")
                    {
                        lsbureau_code = "";
                    }
                    else
                    {
                        lsbureau_code = values.bureau_code.Replace("'", @"\'");
                    }

                    if (values.hrmapping_code == null || values.hrmapping_code == "")
                    {
                        lshrmapping_code = "";
                    }
                    else
                    {
                        lshrmapping_code = values.hrmapping_code;
                    }

                    msGetGid = objcmnfunctions.GetMasterGID("HRMP");
                    lshrmapping_code = objcmnfunctions.GetMasterGID("HRMA");

                msSQL = " insert into hrl_mst_thrmapping(" +
                            " hrmapping_gid ," +
                            " hrmapping_name," +
                            " hrmapping_code," +
                            " lms_code," +
                            " bureau_code," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.hrmapping_name + "'," +
                            "'" + lshrmapping_code + "'," +
                            "'" + lslms_code + "'," +
                            "'" + lsbureau_code + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.hrmapping_name == "Approver")
                {
                    msGethrmappingemployee_gid = objcmnfunctions.GetMasterGID("HRME");
                    msSQL = "Insert into hrl_mst_thrmapping2employee( " +
                           " hrmapping2employee_gid, " +
                           " hrmapping_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGethrmappingemployee_gid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.employee_gid + "'," +
                           "'" + values.employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else if (values.hrmapping_name == "Manager")
                {
                    for (var i = 0; i < values.employee.Count; i++)
                    {
                        msGethrmappingemployee_gid = objcmnfunctions.GetMasterGID("HRME");
                        msSQL = "Insert into hrl_mst_thrmapping2employee( " +
                               " hrmapping2employee_gid, " +
                               " hrmapping_gid," +
                               " employee_gid," +
                               " employee_name," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGethrmappingemployee_gid + "'," +
                               "'" + msGetGid + "'," +
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
                        values.message = "HR mapping added successfully";
                    }
                    else
                    {
                        values.message = "Error occured while adding";
                        values.status = false;
                    }

                }

            }

            public void DaEditHRMapping(string hrmapping_gid, MdlMstHRLoanHRMappingApprovals values)
            {
                msSQL = " select hrmapping_gid,hrmapping_name,hrmapping_code,lms_code, bureau_code, status as Status from hrl_mst_thrmapping " +
                        " where hrmapping_gid='" + hrmapping_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.hrmapping_gid = objODBCDatareader["hrmapping_gid"].ToString();
                    values.hrmapping_name = objODBCDatareader["hrmapping_name"].ToString();
                    values.hrmapping_code = objODBCDatareader["hrmapping_code"].ToString();
                    values.lms_code = objODBCDatareader["lms_code"].ToString();
                    values.bureau_code = objODBCDatareader["bureau_code"].ToString();
                    values.Status = objODBCDatareader["Status"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = " select hrmapping2employee_gid,employee_gid,employee_name from hrl_mst_thrmapping2employee " +
                        " where hrmapping_gid='" + hrmapping_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getemployeeList = new List<employeeem_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        getemployeeList.Add(new employeeem_list
                        {
                            hrmapping2employee_gid = dt["hrmapping2employee_gid"].ToString(),
                            employee_gid = dt["employee_gid"].ToString(),
                            employee_name = dt["employee_name"].ToString(),
                        });
                        values.employeeem_list = getemployeeList;
                    }
                }
                dt_datatable.Dispose();
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                        " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                        " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_manageremployee = new List<employee>();
                if (dt_datatable.Rows.Count != 0)
                {
                    values.employee = dt_datatable.AsEnumerable().Select(row =>
                      new employee
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();

                values.status = true;


            }
            public void DaUpdateHRMapping(string employee_gid, MdlMstHRLoanHRMappingApprovals values)
            {
           
                msSQL = "select updated_by, date_format(updated_date,'%Y-%m-%d %h:%i:%s') as updated_date,hrmapping_name from hrl_mst_thrmapping where hrmapping_gid ='" + values.hrmapping_gid + "' ";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);

                if (objODBCDatareader.HasRows == true)
                {
                    string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                    string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                    if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                    {
                        msGetGid = objcmnfunctions.GetMasterGID("HRML");

                        msSQL = " insert into hrl_mst_thrmappinglog (" +
                               " hrmappinglog_gid, " +
                               " hrmapping_gid, " +
                               " hrmapping_name," +
                               " updated_by," +
                               " updated_date) " +
                               " values (" +
                               " '" + msGetGid + "'," +
                               " '" + values.hrmapping_gid + "'," +
                               " '" + values.hrmapping_name + "'," +
                               " '" + employee_gid + "'," +
                               " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                objODBCDatareader.Close();
                msSQL = " update hrl_mst_thrmapping set " +
                        " hrmapping_name='" + values.hrmapping_name + "'," +
                        " hrmapping_code='" + values.hrmapping_code + "'," +
                        " lms_code='" + values.lms_code.Replace("'", @"\'") + "'," +
                        " bureau_code='" + values.bureau_code.Replace("'", @"\'") + "'," +
                        " updated_by='" + employee_gid + "'," +
                        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                        " where hrmapping_gid='" + values.hrmapping_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from hrl_mst_thrmapping2employee where hrmapping_gid ='" + values.hrmapping_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    if (values.hrmapping_name == "Approver")
                    {
                        msGethrmappingemployee_gid = objcmnfunctions.GetMasterGID("HRME");
                        msSQL = "Insert into hrl_mst_thrmapping2employee( " +
                               " hrmapping2employee_gid, " +
                               " hrmapping_gid," +
                               " employee_gid," +
                               " employee_name," +
                               " created_by," +
                               " created_date)" +
                               " values(" +
                               "'" + msGethrmappingemployee_gid + "'," +
                               "'" + values.hrmapping_gid + "'," +
                               "'" + values.employee_gid + "'," +
                               "'" + values.employee_name + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else if (values.hrmapping_name == "Manager")
                    {
                        for (var i = 0; i < values.employee.Count; i++)
                        {
                            msGethrmappingemployee_gid = objcmnfunctions.GetMasterGID("HRME");
                            msSQL = "Insert into hrl_mst_thrmapping2employee( " +
                                   " hrmapping2employee_gid, " +
                                   " hrmapping_gid," +
                                   " employee_gid," +
                                   " employee_name," +
                                   " created_by," +
                                   " created_date)" +
                                   " values(" +
                                   "'" + msGethrmappingemployee_gid + "'," +
                                   "'" + values.hrmapping_gid + "'," +
                                   "'" + values.employee[i].employee_gid + "'," +
                                   "'" + values.employee[i].employee_name + "'," +
                                   "'" + employee_gid + "'," +
                                   "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }
                if (mnResult != 0)
                {

                    values.status = true;
                    values.message = "HR mapping updated successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error occurred while updating";
                }
            
        }


            public void DaInactiveHRMapping(MdlMstHRLoanHRMappingApprovals values, string employee_gid)
            {
                msSQL = " update hrl_mst_thrmapping set status='" + values.rbo_status + "'," +
                        " remarks='" + values.remarks.Replace("'", @"\'") + "'" +
                        " where hrmapping_gid='" + values.hrmapping_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("HRMI");

                    msSQL = " insert into hrl_mst_thrmappinginactivelog (" +
                          " hrmappinginactivelog_gid, " +
                          " hrmapping_gid," +
                          " hrmapping_name," +
                          " status," +
                          " remarks," +
                          " updated_by," +
                          " updated_date) " +
                          " values (" +
                          " '" + msGetGid + "'," +
                          " '" + values.hrmapping_gid + "'," +
                          " '" + values.hrmapping_name + "'," +
                          " '" + values.rbo_status + "'," +
                          " '" + values.remarks.Replace("'", @"\'") + "'," +
                          " '" + employee_gid + "'," +
                          " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (values.rbo_status == 'N')
                    {
                        values.status = true;
                        values.message = "HR mapping inactivated successfully";
                    }
                    else
                    {
                        values.status = true;
                        values.message = "HR mapping activated successfully";
                    }
                }
                else
                {
                    values.status = false;
                    values.message = "Error occurred";
                }
            }

            public void DaDeleteHRMapping(string hrmapping_gid, string hrmapping_name, string employee_gid, MdlMstHRLoanHRMappingApprovals values)
            {
            if (hrmapping_name == "Approver")
            {
                msSQL = " select a.hrhead_gid from hrl_trn_trequest a " +
                        " left join hrl_mst_thrmapping2employee b on b.employee_gid = a.hrhead_gid " +
                        " left join hrl_mst_thrmapping c on c.hrmapping_gid = b.hrmapping_gid " +
                        " where c.hrmapping_name = '" + hrmapping_name + "' and b.hrmapping_gid ='" + hrmapping_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.message = "This HR is mapped, so..you can't delete it..!";
                    values.status = false;
                    objODBCDatareader.Close();
                }
            }
            else if (hrmapping_name == "Manager")
            {
                msSQL = " select a.hrverify_approvedby from hrl_trn_trequest a " +
                        " left join hrl_mst_thrmapping2employee b on b.employee_gid = a.hrverify_approvedby " +
                        " left join hrl_mst_thrmapping c on c.hrmapping_gid = b.hrmapping_gid " +
                        " where  c.hrmapping_name = '" + hrmapping_name + "' and b.hrmapping_gid ='" + hrmapping_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows)
                {
                    values.message = "This HR is mapped, so..you can't delete it..!";
                    values.status = false;
                    objODBCDatareader.Close();
                }

            }            
            else
            {
                msSQL = " delete from hrl_mst_thrmapping where hrmapping_gid='" + hrmapping_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "HR mapping deleted successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error occured..!";
                }
            }         
            }


            public void DaHRMappingInactiveLogview(string hrmapping_gid, MdlMstHRLoanHRMappingApprovals values)
            {
                try
                {
                    msSQL = " SELECT a.hrmapping_gid,date_format(updated_date,'%Y-%m-%d %h:%i:%s') as updated_date, " +
                            " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                            " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                            " FROM hrl_mst_thrmappinginactivelog a" +
                            " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                            " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                            " where a.hrmapping_gid ='" + hrmapping_gid + "' order by a.hrmappinginactivelog_gid desc ";
                    dt_datatable = objdbconn.GetDataTable(msSQL);
                    var gethrmapping_list = new List<hrmapping_list>();
                    if (dt_datatable.Rows.Count != 0)
                    {
                        foreach (DataRow dr_datarow in dt_datatable.Rows)
                        {
                            gethrmapping_list.Add(new hrmapping_list
                            {
                                hrmapping_gid = (dr_datarow["hrmapping_gid"].ToString()),
                                updated_by = (dr_datarow["updated_by"].ToString()),
                                updated_date = (dr_datarow["updated_date"].ToString()),
                                status = (dr_datarow["Status"].ToString()),
                                remarks = (dr_datarow["remarks"].ToString()),
                            });
                        }
                        values.hrmapping_list = gethrmapping_list;
                    }
                    dt_datatable.Dispose();
                    values.status = true;
                }
                catch
                {
                    values.status = false;
                }
            }

        public void DaGetEmployeelist(MdlMstHRLoanHRMappingApprovals objmaster)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_assignee = new List<employeelist>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objmaster.employeelist = dt_datatable.AsEnumerable().Select(row =>
                      new employeelist
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }

        public void DaGetEmployeeName(string hrmapping_gid, hrmappingemployee values)
            {
                msSQL = " select group_concat(employee_name) as employee_name  from hrl_mst_thrmapping2employee " +
                      " where hrmapping_gid='" + hrmapping_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.employee_name = objODBCDatareader["employee_name"].ToString();
                }
                objODBCDatareader.Close();
                msSQL = " select hrmapping_name from hrl_mst_thrmapping" +
                     " where hrmapping_gid='" + hrmapping_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.hrmapping_name = objODBCDatareader["hrmapping_name"].ToString();
                }
                objODBCDatareader.Close();

            }
        public void DaGetManagerName(hrmappingemployee values)
        {
            msSQL = " select group_concat(a.employee_name) as employee_name  from hrl_mst_thrmapping2employee a"+
                    " left join hrl_mst_thrmapping b on b.hrmapping_gid = a.hrmapping_gid "+
                    " where hrmapping_name = 'Manager'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.employee_name = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();            

        }
        public void DaGetApproverName(hrmappingemployee values)
        {
            msSQL = " select a.employee_name as employee_name  from hrl_mst_thrmapping2employee a" +
                    " left join hrl_mst_thrmapping b on b.hrmapping_gid = a.hrmapping_gid " +
                    " where hrmapping_name = 'Approver'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.employee_name = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();

        }
    }
    }