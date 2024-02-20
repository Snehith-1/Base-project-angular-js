using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.osd.Models;

namespace ems.osd.DataAccess
{
    public class DaOsdMstDepartmentManagement
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        string msSQL, msGetGid, msGetBusinessUnitCode;
        string msGetActivityCode;
        int mnResult;
        string activityGID = string.Empty;

        public bool DaGetDepartment(mdldepartment values)
        {
            try
            {
                msSQL = "select businessunit_gid,businessunit_name from osd_mst_tbusinessunit " +
                        " where businessunit_gid not in (select department_gid from  osd_mst_tactivedepartment) order by businessunit_name";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_department_list = new List<department>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_row in dt_datatable.Rows)
                    {
                        get_department_list.Add(new department
                        {
                            department_gid = dr_row["businessunit_gid"].ToString(),
                            department_name = dr_row["businessunit_name"].ToString()
                        });
                    }
                    values.department = get_department_list;

                    dt_datatable.Dispose();
                    values.status = true;
                    return true;
                }
                else
                {
                    dt_datatable.Dispose();
                    values.status = false;
                    return false;
                }
            }
            catch
            {
                values.status = false;
                return false;
            }
        }

        public bool DaGetDepartmentEdit(mdldepartment values,string activedepartment_gid)
        {
            try
            {
                string lsdepartment_gid;
                lsdepartment_gid = objdbconn.GetExecuteScalar("select department_gid from  osd_mst_tactivedepartment where activedepartment_gid ='" + activedepartment_gid + "'");
                msSQL = "select businessunit_gid,businessunit_name from osd_mst_tbusinessunit " +
                        " where businessunit_gid not in (select department_gid from  osd_mst_tactivedepartment)" +
                        " or businessunit_gid ='" + lsdepartment_gid + "'order by businessunit_name";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_department_list = new List<department>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_row in dt_datatable.Rows)
                    {
                        get_department_list.Add(new department
                        {
                            department_gid = dr_row["businessunit_gid"].ToString(),
                            department_name = dr_row["businessunit_name"].ToString()
                        });
                    }
                    values.department = get_department_list;

                    dt_datatable.Dispose();
                    values.status = true;
                    return true;
                    
                }
                else
                {
                    dt_datatable.Dispose();
                    values.status = false;
                    return false;
                }
            }
            catch
            {
                values.status = false;
                return false;
            }
        }


        public void DaGetDeptActivityList(actvitydtllist values,string department_gid)
        {
            msSQL = " select activitymaster_gid,activity_code,activity_name from osd_mst_tactivitymaster where department_gid='" + department_gid +"'" +
                    " order by activitymaster_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getActivityList = new List<activitydtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getActivityList.Add(new activitydtl
                    {
                        activity_name = dt["activity_name"].ToString(),
                        activitymaster_gid = dt["activitymaster_gid"].ToString(),
                        activity_code = dt["activity_code"].ToString(),
                    });
                    values.activitydtl = getActivityList;
                }
            }
            dt_datatable.Dispose();
        }


        public void DaGetActivatedeptSummary(activatedeptlist values)
        {
            msSQL = " select activedepartment_gid,department_gid,department_code,department_name," +
                   "  case when department_status='N' then 'Inactive' else 'Active' end as department_status," +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by," +
                   " date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from osd_mst_tactivedepartment a " +
                    "LEFT JOIN adm_mst_tuser b ON a.created_by=b.user_gid " +
                    " order by activedepartment_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdeptList = new List<acivatedept>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdeptList.Add(new acivatedept
                    {
                        activedepartment_gid = dt["activedepartment_gid"].ToString(),
                        department_gid = dt["department_gid"].ToString(),
                        department_code = dt["department_code"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        department_status = dt["department_status"].ToString(),
                    });
                    values.acivatedept = getdeptList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetActivatedept(activatedeptlist values,string employee_gid)
        {
            msSQL = " select activedepartment_gid,department_gid,department_code,department_name" +
                     " from osd_mst_tactivedepartment  where department_status <> 'N'" +
                    " and (department_gid in (select department_gid from osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                    " department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "'))" +
                    " order by activedepartment_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdeptList = new List<deptlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdeptList.Add(new deptlist
                    {
                        department_gid = dt["department_gid"].ToString(),
                        department_name = dt["department_name"].ToString(),
                    });
                    values.deptlist = getdeptList;
                }
                if (dt_datatable.Rows.Count == 1)
                {
                    foreach (DataRow dt in dt_datatable.Rows)
                    {
                        values.department_gid = dt["department_gid"].ToString();
                        values.department_name = dt["department_name"].ToString();
                    }
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetActivaterequestdept(activatedeptlist values)
        {
            msSQL = " select activedepartment_gid,department_gid,department_code,department_name" +
                     " from osd_mst_tactivedepartment  where department_status <> 'N' and department_gid in (select department_gid from osd_mst_tactivitymaster  )" +
                    " order by activedepartment_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getdeptList = new List<deptlist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getdeptList.Add(new deptlist
                    {
                        department_gid = dt["department_gid"].ToString(),
                        department_name = dt["department_name"].ToString(),
                    });
                    values.deptlist = getdeptList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostActivatedeptAdd(acivatedept values, string user_gid)
        {
                    msGetGid = objcmnfunctions.GetMasterGID("ACDP");
            
                    msGetActivityCode = objcmnfunctions.GetMasterGID("ADP");

                    msSQL = " insert into osd_mst_tactivedepartment(" +
                     " activedepartment_gid," +
                     " department_code, " +
                     " department_gid," +
                     " department_name," +
                     " created_by," +
                     " created_date)" +
                     " values(" +
                     "'" + msGetGid + "'," +
                     "'" + msGetActivityCode + "', " +
                     "'" + values.department_gid + "'," +
                     "'" + values.department_name + "'," +
                     "'" + user_gid + "'," +
                     "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        values.status = true;
                        values.message = "Business Unit Activated Successfully..!";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured..!";
                    }
                
          
        }


        public void DaGetActivatedeptUpdate(acivatedept values, string user_gid)
        {

            msSQL = "select updated_by, updated_date,department_name,department_gid from osd_mst_tactivedepartment where activedepartment_gid = '" + values.activedepartment_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("DPHL");
                    msSQL = " insert into ocs_mst_tactivedepartmenthistory(" +
                              " activedepartmenthistory_gid," +
                              " activedepartment_gid," +
                              " department_gid," +
                              " department_name, " +
                              " lastupdated_by, " +
                              " lastupdated_date, " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.activedepartment_gid + "'," +
                              "'" + objODBCDatareader["department_gid"].ToString() + "'," +
                              "'" + objODBCDatareader["department_name"].ToString() + "'," +
                              "'" + objODBCDatareader["updated_by"].ToString() + "'," +
                              "'" + objODBCDatareader["updated_date"].ToString() + "'," +
                              "'" + user_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();
            msSQL = " update osd_mst_tactivedepartment set " +
                    " department_gid='" + values.department_gid+ "'," +
                    " department_name='" + values.department_name + "'," +
                    " updated_by='" + user_gid + "'," +
                    " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                    " where activedepartment_gid='" + values.activedepartment_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Business Unit Activation Updated Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }

            
        }

        public bool DaGetActivatedeptDelete(string activedepartment_gid, result values)
        {
            string lsdepartment_gid;
            lsdepartment_gid=objdbconn.GetExecuteScalar("select department_gid from osd_mst_tactivedepartment where activedepartment_gid = '" + activedepartment_gid + "'");

            msSQL = "select department_gid from osd_mst_tsupportteam where department_gid='" + lsdepartment_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                
                values.status = false;
                values.message = "Support Team is assigned to this Business Unit So..you can't delete it..!";
                return false;
            }
            objODBCDatareader.Close();

            msSQL = "select department_gid from osd_mst_tactivitymaster where department_gid='" + lsdepartment_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                
                values.status = false;
                values.message = "Activity is assigned to this Business Unit So..you can't delete it..!";
                return false;
            }
            objODBCDatareader.Close();

            msSQL = "select department_gid from osd_mst_tactivedepartment2manager where activedepartment_gid='" + activedepartment_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                
                values.status = false;
                values.message = "Manager is assigned to this Business Unit So..you can't delete it..!";
                return false;
            }
            objODBCDatareader.Close();

            msSQL = " delete from osd_mst_tactivedepartment where activedepartment_gid='" + activedepartment_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                  
                values.status = true;
                values.message = "Business Unit  Deleted Successfully..!";
                    return true;
                }
            else
            {
                  
                values.status = false;
                values.message = "Error Occured..!";
                    return false;
                }
           
        }

        public void DaGetActivatedeptView(string activedepartment_gid, acivatedept values)
        {
            msSQL = " select activedepartment_gid,department_code,department_gid,department_name,department_status from osd_mst_tactivedepartment " +
                    " where activedepartment_gid='" + activedepartment_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.activedepartment_gid = objODBCDatareader["activedepartment_gid"].ToString();
                values.department_code = objODBCDatareader["department_code"].ToString();
                values.department_gid = objODBCDatareader["department_gid"].ToString();
                values.department_name = objODBCDatareader["department_name"].ToString();
                values.department_status = objODBCDatareader["department_status"].ToString();
            }
            objODBCDatareader.Close();
        }

        public bool DaPostdepartmentstatusupdate(acivatedept values, string employee_gid)
        {

            string lsdepartment_gid;
            lsdepartment_gid = objdbconn.GetExecuteScalar("select department_gid from osd_mst_tactivedepartment where activedepartment_gid = '" + values.activedepartment_gid + "'");
            if(values.departmentstatus == 'N')
            { 
            msSQL = "select department_gid from osd_mst_tsupportteam where department_gid='" + lsdepartment_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                
                values.status = false;
                values.message = "Support Team is assigned to this Business Unit So..you can't deactivate it..!";
                return false;
            }
                objODBCDatareader.Close();

                msSQL = "select department_gid from osd_mst_tactivitymaster where department_gid='" + lsdepartment_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                
                values.status = false;
                values.message = "Activity is assigned to this Business Unit So..you can't deactivate it..!";
                return false;
            }
                objODBCDatareader.Close();

                msSQL = "select department_gid from osd_mst_tactivedepartment2manager where activedepartment_gid='" + values.activedepartment_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                
                values.status = false;
                values.message = "Manager is assigned to this Business Unit So..you can't deactivate it..!";
                return false;
            }
                objODBCDatareader.Close();
            }
            msSQL = " update osd_mst_tactivedepartment set department_status='" + values.departmentstatus + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where activedepartment_gid='" + values.activedepartment_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msGetGid = objcmnfunctions.GetMasterGID("ADSL");

                msSQL = " insert into ocs_mst_tactivedepartmentstatushistory (" +
                      " activedepartmentstatushistory_gid, " +
                      " activedepartment_gid," +
                      " department_gid, " +
                      " department_name," +
                      " department_status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.activedepartment_gid + "'," +
                      " '" + values.department_gid + "'," +
                      " '" + values.department_name + "', " +
                      " '" + values.departmentstatus + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.departmentstatus == 'N')
                {
                    values.status = true;
                    values.message = "Business Unit Inactivated Successfully";
                        return true;
                    }
                else
                {
                    values.status = true;
                    values.message = "Business Unit Activated Successfully";
                        return true;
                    }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
                    return false;
                }
        
        }

        public void DaDepartmentstatusHistory(departmentstatusHistory values, string activedepartment_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.department_status='N' then 'Inactive' else 'Active' end as department_status" +
                " from ocs_mst_tactivedepartmentstatushistory a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.activedepartment_gid='" + activedepartment_gid + "' order by a.activedepartmentstatushistory_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getinactivehistory_list = new List<departmentstatusehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getinactivehistory_list.Add(new departmentstatusehistory_list
                        {
                            departmentstatus = (dr_datarow["department_status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    values.departmentstatusehistory_list = getinactivehistory_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }



        public void DaGetEmployee(MdlEmployeeassign objemployee,string activedepartment_gid)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' and b.employee_gid not in (select manager_gid from osd_mst_tactivedepartment2manager where activedepartment_gid='" + activedepartment_gid + "') " +
                   " order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employeeasssign_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objemployee.employeeasssign_list = dt_datatable.AsEnumerable().Select(row =>
                      new employeeasssign_list
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objemployee.status = true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
            }


        }


        public void DaMemberEmployee(MdlEmployeeassign objemployee, string activedepartment_gid)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N'" +
                  " and b.employee_gid not in (select member_gid from osd_mst_tactivedepartment2member where activedepartment_gid='" + activedepartment_gid + "')" +
                   " order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employeeasssign_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objemployee.employeeasssign_list = dt_datatable.AsEnumerable().Select(row =>
                      new employeeasssign_list
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objemployee.status = true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
            }


        }


        public void DaGetDeptEmployee(MdlEmployeeassign objemployee, string department_gid)
        {
            try
            {
                msSQL = " SELECT member_gid,member_name from osd_mst_tactivedepartment2member where department_gid ='" + department_gid + "' order by member_name asc";
                
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employeeasssign_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objemployee.employeeasssign_list = dt_datatable.AsEnumerable().Select(row =>
                      new employeeasssign_list
                      {
                          employee_gid = row["member_gid"].ToString(),
                          employee_name = row["member_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objemployee.status = true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
            }


        }


        public void DaAssignedemplyee(MdlEmployeeassign objemployee, string activedepartment_gid)
        {
            try
            {
                msSQL = " select a.manager_gid,a.manager_name,a.activedepartment2manager_gid,c.department_name from osd_mst_tactivedepartment2manager a " +
                        " left join hrm_mst_temployee b on a.manager_gid = b.employee_gid " +
                        " left join hrm_mst_tdepartment c on b.department_gid=c.department_gid " +  
                        " where a.activedepartment_gid='" + activedepartment_gid + "' order by manager_name asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<managereasssigned_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objemployee.managereasssigned_list = dt_datatable.AsEnumerable().Select(row =>
                      new managereasssigned_list
                      {
                          activedepartment2manager_gid = row["activedepartment2manager_gid"].ToString(),
                          employee_gid = row["manager_gid"].ToString(),
                          employee_name = row["manager_name"].ToString(),
                          department_name = row["department_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objemployee.status = true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
            }


        }

        public void DaAssignedmemberemplyee(MdlEmployeeassign objemployee, string activedepartment_gid)
        {
            try
            {
                msSQL = " select a.member_gid,a.member_name,a.activedepartment2member_gid,c.department_name from osd_mst_tactivedepartment2member a " +
                        " left join hrm_mst_temployee b on a.member_gid=b.employee_gid " +
                        " left join hrm_mst_tdepartment c on b.department_gid=c.department_gid " +
                       " where a.activedepartment_gid='" + activedepartment_gid + "' order by a.member_name asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<membereasssigned_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    objemployee.membereasssigned_list = dt_datatable.AsEnumerable().Select(row =>
                      new membereasssigned_list
                      {
                          activedepartment2member_gid = row["activedepartment2member_gid"].ToString(),
                          employee_gid = row["member_gid"].ToString(),
                          employee_name = row["member_name"].ToString(),
                          department_name = row["department_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objemployee.status = true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
            }


        }


        public void DaPostAssignDeptmanager(Mdlassignmanager values, string user_gid)
        {
            foreach (string i in values.employeelist_gid)
            {
                string lsemployee_name, lsdepartment_gid, lsdepartment_name;
                msSQL = "SELECT concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name FROM hrm_mst_temployee a left join adm_mst_tuser b " +
                        " on a.user_gid=b.user_gid " +
                        "WHERE a.employee_gid='" + i + "'";
                lsemployee_name = objdbconn.GetExecuteScalar(msSQL);
                msGetGid = objcmnfunctions.GetMasterGID("ACDM");


                lsdepartment_gid = objdbconn.GetExecuteScalar("select department_gid from osd_mst_tactivedepartment where activedepartment_gid='" + values.activedepartment_gid + "'");
                lsdepartment_name = objdbconn.GetExecuteScalar("select department_name from osd_mst_tactivedepartment where activedepartment_gid='" + values.activedepartment_gid + "'");

                msSQL = " INSERT INTO osd_mst_tactivedepartment2manager(" +
                        " activedepartment2manager_gid," +
                        " activedepartment_gid," +
                        " department_gid ," +
                        " department_name," +
                        " manager_gid," +
                        " manager_name," +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetGid + "'," +
                        "'" + values.activedepartment_gid + "'," +
                        "'" + lsdepartment_gid + "'," +
                        "'" + lsdepartment_name + "'," +
                        "'" + i + "'," +
                        "'" + lsemployee_name + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"'," +
                        "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                

            }

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Business Unit Manager Assigned Successfully...";

            }
            else
            {
                values.status = true;
                values.message = "Error Occured";

            }
        }


        public void DaPostAssignDeptmember(Mdlassignmanager values, string user_gid)
        {
            foreach (string i in values.employeelist_gid)
            {
                string lsemployee_name, lsdepartment_gid, lsdepartment_name;
                msSQL = "SELECT concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as employee_name FROM hrm_mst_temployee a left join adm_mst_tuser b " +
                        " on a.user_gid=b.user_gid " +
                        "WHERE a.employee_gid='" + i + "'";
                lsemployee_name = objdbconn.GetExecuteScalar(msSQL);
                msGetGid = objcmnfunctions.GetMasterGID("ACDU");


                lsdepartment_gid = objdbconn.GetExecuteScalar("select department_gid from osd_mst_tactivedepartment where activedepartment_gid='" + values.activedepartment_gid + "'");
                lsdepartment_name = objdbconn.GetExecuteScalar("select department_name from osd_mst_tactivedepartment where activedepartment_gid='" + values.activedepartment_gid + "'");

                msSQL = " INSERT INTO osd_mst_tactivedepartment2member(" +
                        " activedepartment2member_gid," +
                        " activedepartment_gid," +
                        " department_gid ," +
                        " department_name," +
                        " member_gid," +
                        " member_name," +
                        " created_date," +
                        " created_by)" +
                        " VALUES(" +
                        "'" + msGetGid + "'," +
                        "'" + values.activedepartment_gid + "'," +
                        "'" + lsdepartment_gid + "'," +
                        "'" + lsdepartment_name + "'," +
                        "'" + i + "'," +
                        "'" + lsemployee_name + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'" + user_gid + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);



            }

            if (mnResult == 1)
            {
                values.status = true;
                values.message = "Business Unit Member Assigned Successfully...";

            }
            else
            {
                values.status = true;
                values.message = "Error Occured";

            }
        }


        public void DaGetAssignmangerDelete(string activedepartment2manager_gid, result values)
        {
            string lsmanager_gid = objdbconn.GetExecuteScalar("select manager_gid from osd_mst_tactivedepartment2manager  where activedepartment2manager_gid='" + activedepartment2manager_gid + "'");

            string lsdepartment_gid = objdbconn.GetExecuteScalar("select department_gid from osd_mst_tactivedepartment2manager  where activedepartment2manager_gid='" + activedepartment2manager_gid + "'");

            msSQL = " select a.allocate_managergid from osd_trn_tmanagertransfer a " +
                    " left join osd_trn_tservicerequest b on b.servicerequest_gid = a.servicerequest_gid " +
                    "  left join osd_mst_tactivedepartment2manager c on c.department_gid = b.department_gid  " +
                    " where a.allocate_managergid='" + lsmanager_gid + "' and b.assigned_status='Self Allocated' and c.department_gid='"+ lsdepartment_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Member is Tagged to the Support Team  So..you can't UnAssign the Manager..!";

            }
            else
            {

                msSQL = " delete from osd_mst_tactivedepartment2manager where activedepartment2manager_gid='" + activedepartment2manager_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Manager Un Assigned Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }
            }
        }

        public void DaGetAssignmemberDelete(string activedepartment2member_gid, result values)
        {
            string lsmember_gid = objdbconn.GetExecuteScalar("select member_gid from osd_mst_tactivedepartment2member  where activedepartment2member_gid='" + activedepartment2member_gid + "'");

            string lsdepartment_gid = objdbconn.GetExecuteScalar("select department_gid from osd_mst_tactivedepartment2member  where activedepartment2member_gid='" + activedepartment2member_gid + "'");
            msSQL = "select a.member_gid from osd_mst_tsupportteam2member a left join osd_mst_tsupportteam b on a.supportteam_gid=b.supportteam_gid  where a.member_gid='" + lsmember_gid + "' and b.department_gid='"+ lsdepartment_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Member is Tagged to the Support Team  So..you can't UnAssign the Member..!";
                
            }
            else
            {
                msSQL = " delete from osd_mst_tactivedepartment2member where activedepartment2member_gid='" + activedepartment2member_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Member Un Assigned Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }

                objODBCDatareader.Close();
            }
            
           

        }

        public void DaGetBusinessUnitSummary(activatedeptlist values)
        {
            msSQL = " select businessunit_gid, businessunit_prefix, businessunit_code, businessunit_name, businessunit_emailaddress," +
                    " case when businessunit_status='N' then 'Inactive' else 'Active' end as businessunit_status, " +
                    " concat(b.user_firstname, ' ', b.user_lastname, ' / ', b.user_code) as created_by, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                    " from osd_mst_tbusinessunit a " +
                    " LEFT JOIN adm_mst_tuser b ON a.created_by=b.user_gid " +
                    " order by businessunit_gid desc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbusinessunit_list = new List<businessunit_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getbusinessunit_list.Add(new businessunit_list
                    {
                        businessunit_gid = dt["businessunit_gid"].ToString(),
                        businessunit_prefix = dt["businessunit_prefix"].ToString(),
                        businessunit_code = dt["businessunit_code"].ToString(),
                        businessunit_name = dt["businessunit_name"].ToString(),
                        businessunit_emailaddress = dt["businessunit_emailaddress"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        businessunit_status = dt["businessunit_status"].ToString(),
                    });
                    values.businessunit_list = getbusinessunit_list;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostBusinessUnit(businessunit_list values, string user_gid)
        {
            string lsfinyear = objdbconn.GetExecuteScalar("select year(fyear_start) as fyear_start from adm_mst_tyearendactivities where fyear_end is null");
            string lscode = values.businessunit_prefix + "RRN";
            string lssequencecode = objdbconn.GetExecuteScalar("select sequence_code from adm_mst_tsequence where sequence_code='" + lscode + "' and finyear = '" + lsfinyear + "' ");
            if (lssequencecode == null || lssequencecode == string.Empty || lssequencecode == "")
            {
                msGetGid = objcmnfunctions.GetMasterGID("MBUG");

                msGetBusinessUnitCode = objcmnfunctions.GetMasterGID("BUC");

                msSQL = " insert into osd_mst_tbusinessunit(" +
                 " businessunit_gid," +
                 " businessunit_code, " +
                 " businessunit_prefix," +
                 " businessunit_name," +
                 " businessunit_emailaddress," +
                 " created_by," +
                 " created_date)" +
                 " values(" +
                 "'" + msGetGid + "'," +
                 "'" + msGetBusinessUnitCode + "', " +
                 "'" + values.businessunit_prefix.Replace("'", "") + "'," +
                 "'" + values.businessunit_name.Replace("'", "") + "'," +
                 "'" + values.businessunit_emailaddress.Replace("'", "\\'") + "'," +
                 "'" + user_gid + "'," +
                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                msSQL = " update osd_mst_tbusinessstatus set businessunit_gid = '" + msGetGid + "'" +
                  " where businessunit_gid='" + user_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                string lsnewseq = values.businessunit_prefix.Replace("'", "") + "RRN";
                msGetGid = objcmnfunctions.GetMasterGID("OSD");
                msSQL = "insert into adm_mst_tsequence(" +
                    "sequence_gid, " +
                    "sequence_code, " +
                    " sequence_name, " +
                    " sequence_format,  " +
                    "sequence_curval,  " +
                    "sequence_flag, " +
                    " branch_flag, " +
                    " department_flag, " +
                    " year_flag, " +
                    " month_flag, " +
                    " location_flag, " +
                    " company_code, " +
                    " delimeter,  " +
                    "runningno_prefix, " +
                    " finyear, " +
                    " carry_forward, " +
                    " created_by, " +
                    " created_date) " +
                    "values( " +
                    "'" + msGetGid + "'," +
                    "'" + lsnewseq + "'," +
                    "'" + values.businessunit_name.Replace("'", "") + "'," +
                    "'4', " +
                    " '0', " +
                    " 'N',  " +
                    "'N',  " +
                    "'N', " +
                    " 'N', " +
                    " 'N', " +
                    " 'N', " +
                    " '',  " +
                    "'', " +
                    " '', " +
                    "'" + lsfinyear + "'," +
                    "'N',  " +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Business Unit Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured";
                }
            }
            else
            {
                values.status = false;
                values.message = "Business Prefix Already Exist";
            }
        }


        public bool DaPostBusinessUnitStatus(businessunit_list values, string user_gid)
        {

            
            msGetGid = objcmnfunctions.GetMasterGID("B2MN");
            msSQL = " insert into osd_mst_tbusinessstatus(" +
                    " businessstatus_gid," +
                    " businessunit_gid," +
                    " business_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + user_gid + "'," +
                    "'" + values.business_status.Replace("'", "") + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Business Unit Status Added Successfully";
                objdbconn.CloseConn();
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Business Unit Status";
                objdbconn.CloseConn();
                return false;
            }
        }
        public bool DaPostBusinessUnitStatusEdit(businessunit_list values, string user_gid)
        {


            msGetGid = objcmnfunctions.GetMasterGID("B2MN");
            msSQL = " insert into osd_mst_tbusinessstatus(" +
                    " businessstatus_gid," +
                    " businessunit_gid," +
                    " business_status," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + values.businessunit_gid + "'," +
                    "'" + values.business_status.Replace("'", "") + "'," +
                    "'" + user_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Business Unit Status Added Successfully";
                objdbconn.CloseConn();
                return true;
            }
            else
            {
                values.status = true;
                values.message = "Error Occured While Adding Business Unit Status";
                objdbconn.CloseConn();
                return false;
            }
        }

        public void DaGetBusinessstatusList(activatedeptlist values, string employee_gid)
        {
            string lsuser_gid;
            msSQL = " select user_gid from hrm_mst_temployee where employee_gid='" + employee_gid + "'";

            lsuser_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "select businessstatus_gid,business_status,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_mst_tbusinessstatus a left join adm_mst_tuser b on a.created_by = b.user_gid where " +
              " businessunit_gid='" + lsuser_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbusinessunit_list = new List<businessstatusunit_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbusinessunit_list.Add(new businessstatusunit_list
                    {
                        businessstatus_gid = (dr_datarow["businessstatus_gid"].ToString()),
                        business_status = (dr_datarow["business_status"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                    });
                }
                values.businessstatusunit_list = getbusinessunit_list;


            }
            dt_datatable.Dispose();
        }
        public bool DaDeleteBusinessUnit(string businessunit_gid, result values)
        {
            msSQL = "select department_gid from osd_mst_tactivedepartment where department_gid='" + businessunit_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {

                values.status = false;
                values.message = "This Business Unit is activated, So..you can't delete it..!";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = " delete from osd_mst_tbusinessunit where businessunit_gid='" + businessunit_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Business Unit Deleted Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured";
                return false;
            }
        }
        public void DaBusinessUnitView(string businessunit_gid, businessunit_list values)
        {
            msSQL = " select businessunit_gid, businessunit_prefix, businessunit_code, businessunit_name, businessunit_emailaddress, businessunit_status from osd_mst_tbusinessunit " +
                    " where businessunit_gid='" + businessunit_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.businessunit_prefix = objODBCDatareader["businessunit_prefix"].ToString();
                values.businessunit_code = objODBCDatareader["businessunit_code"].ToString();
                values.businessunit_name = objODBCDatareader["businessunit_name"].ToString();
                values.businessunit_emailaddress = objODBCDatareader["businessunit_emailaddress"].ToString();
                values.businessunit_status = objODBCDatareader["businessunit_status"].ToString();
            }
            objODBCDatareader.Close();
            msSQL = "select department_gid from osd_trn_tservicerequest where department_gid ='" + businessunit_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.sequence_flag = "Y";
            }
            else
            {
                objODBCDatareader.Close();
                values.sequence_flag = "N";
            }
            
        }

        public void DaGetBusinessstatusEdit(string businessunit_gid, activatedeptlist values)
        {
            msSQL = "select businessstatus_gid,business_status,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by from osd_mst_tbusinessstatus a left join adm_mst_tuser b on a.created_by = b.user_gid where " +
          " businessunit_gid='" + businessunit_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getbusinessunitedit_list = new List<businessstatusEdit_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getbusinessunitedit_list.Add(new businessstatusEdit_list
                    {
                        businessstatus_gid = (dr_datarow["businessstatus_gid"].ToString()),
                        business_status = (dr_datarow["business_status"].ToString()),
                        created_date = (dr_datarow["created_date"].ToString()),
                        created_by = (dr_datarow["created_by"].ToString()),
                    });
                }
                values.businessstatusEdit_list = getbusinessunitedit_list;
                }
            dt_datatable.Dispose();

            }
        public void DaUpdateBusinessUnit(businessunit_list values, string user_gid)
        {
            //string lsbusinessunit_prefix=objdbconn.GetExecuteScalar("select businessunit_prefix from  osd_mst_tbusinessunit where businessunit_gid = '" + values.businessunit_gid + "'");

            //if (values.businessunit_prefix.Replace("'", "") != lsbusinessunit_prefix)
            //{
                //string lsfinyear = objdbconn.GetExecuteScalar("select year(fyear_start) as fyear_start from adm_mst_tyearendactivities where fyear_end is null");
                //string lscode1 = values.businessunit_prefix + "RRN";
                //string lssequencecode = objdbconn.GetExecuteScalar("select sequence_code from adm_mst_tsequence where sequence_code='" + lscode1 + "' and finyear = '" + lsfinyear + "' ");
                //if (lssequencecode == null || lssequencecode == string.Empty || lssequencecode == "")
                //{
                    msSQL = " select updated_by, date_format(updated_date,'%Y-%m-%d %h:%i:%s') as updated_date, businessunit_prefix, businessunit_code, businessunit_name, businessunit_emailaddress" +
                        " from osd_mst_tbusinessunit where businessunit_gid = '" + values.businessunit_gid + "'";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);

                    if (objODBCDatareader.HasRows == true)
                    {

                        msGetGid = objcmnfunctions.GetMasterGID("BUGH");
                        msSQL = " insert into osd_mst_tbusinessunithistory(" +
                                  " businessunithistory_gid," +
                                  " businessunit_gid," +
                                  " businessunit_code," +
                                  " businessunit_prefix, " +
                                  " businessunit_name, " +
                                  " businessunit_emailaddress," +
                                  " created_by, " +
                                  " created_date) " +
                                  " values(" +
                                  "'" + msGetGid + "'," +
                                  "'" + values.businessunit_gid + "'," +
                                  "'" + objODBCDatareader["businessunit_code"].ToString() + "'," +
                                  "'" + objODBCDatareader["businessunit_prefix"].ToString() + "'," +
                                  "'" + objODBCDatareader["businessunit_name"].ToString().Replace("'", "") + "'," +
                                  "'" + objODBCDatareader["businessunit_emailaddress"].ToString().Replace("'", "\\'") + "'," +
                                  "'" + user_gid + "'," +
                                  "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        string lscode = values.businessunit_prefix.Replace("'", "") + "RRN";
                        string lsoldcode = objODBCDatareader["businessunit_prefix"].ToString() + "RRN";
                        msSQL = " update adm_mst_tsequence set " +
                            " sequence_code='" + lscode + "'," +
                            " sequence_name='" + values.businessunit_name.Replace("'", "") + "'," +
                            " updated_by='" + user_gid + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           " where sequence_code='" + lsoldcode + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                    objODBCDatareader.Close();
                    msSQL = " update osd_mst_tbusinessstatus set businessunit_gid = '" + values.businessunit_gid + "'" +
                        " where businessunit_gid='" + user_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    msSQL = " update osd_mst_tbusinessunit set " +
                                " businessunit_prefix='" + values.businessunit_prefix.Replace("'", "") + "'," +
                                " businessunit_name='" + values.businessunit_name.Replace("'", "") + "'," +
                                " businessunit_emailaddress='" + values.businessunit_emailaddress.Replace("'", "\\'") + "'," +
                                " updated_by='" + user_gid + "'," +
                                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                " where businessunit_gid='" + values.businessunit_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        msSQL = " update osd_mst_tactivedepartment set " +
                            " department_name='" + values.businessunit_name.Replace("'", "") + "'," +
                            " updated_by='" + user_gid + "'," +
                            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                            " where department_gid='" + values.businessunit_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update osd_mst_tsupportteam set " +
                           " department_name='" + values.businessunit_name.Replace("'", "") + "'," +
                           " updated_by='" + user_gid + "'," +
                           " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                           " where department_gid='" + values.businessunit_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update osd_mst_tactivitymaster set " +
                          " department_name='" + values.businessunit_name.Replace("'", "") + "'," +
                          " updated_by='" + user_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where department_gid='" + values.businessunit_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " update osd_trn_tservicerequest set " +
                          " department_name='" + values.businessunit_name.Replace("'", "") + "'," +
                          " updated_by='" + user_gid + "'," +
                          " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                          " where department_gid='" + values.businessunit_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        values.status = true;
                        values.message = "Business Unit Updated Successfully";
                    }
                    else
                    {
                        values.status = false;
                        values.message = "Error Occured";
                    }
                //}
            //    else
            //    {
            //        values.status = false;
            //        values.message = "Business Prefix Already Exist";
            //    }
            //}
            }

        public bool DaDeleteBusinessstatus(string businessstatus_gid, result values)
        {
            //msSQL = "select department_gid from osd_mst_tactivedepartment where department_gid='" + businessstatus_gid + "'";
            //objODBCDatareader = objdbconn.GetDataReader(msSQL);
           
            msSQL = " delete from osd_mst_tbusinessstatus where businessstatus_gid='" + businessstatus_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Business Unit Status Deleted Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Deleting Business Unit Status";
                return false;
            }
        }
        public void DaGetBusinessStatusTempClear(activatedeptlist values, string employee_gid)
        {
            string lsuser_gid;
            msSQL = " select user_gid from hrm_mst_temployee where employee_gid='" + employee_gid + "'";

            lsuser_gid = objdbconn.GetExecuteScalar(msSQL);
            msSQL = "delete from osd_mst_tbusinessstatus where businessunit_gid='" + lsuser_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

  
            values.status = true;
        }

        public bool DaPostbusinessunitstatusupdate(businessunit_list values, string employee_gid)
        {
            msSQL = "select department_gid from osd_mst_tactivedepartment where department_gid='" + values.businessunit_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                
                values.status = false;
                values.message = "This Business Unit is activated, So..you can't inactivate it..!";
                return false;
            }
            objODBCDatareader.Close();
            msSQL = " update osd_mst_tbusinessunit set businessunit_status='" + values.businessunitstatus + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where businessunit_gid='" + values.businessunit_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("BUIS");

                msSQL = " insert into osd_mst_tbusinessunitstatushistory (" +
                      " businessunitstatushistory_gid, " +
                      " businessunit_gid," +
                      " businessunit_name," +
                      " businessunit_status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.businessunit_gid + "'," +
                      " '" + values.businessunit_name.Replace("'", "") + "'," +
                      " '" + values.businessunitstatus + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.businessunitstatus == 'N')
                {
                    values.status = true;
                    values.message = "Business Unit Inactivated Successfully";
                    return true;
                }
                else
                {
                    values.status = true;
                    values.message = "Business Unit Activated Successfully";
                    return true;
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
                return false;
            }

        }

        public void DaBusinessunitstatusHistory(businessunitstatusHistory values, string businessunit_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.businessunit_status='N' then 'Inactive' else 'Active' end as businessunit_status" +
                " from osd_mst_tbusinessunitstatushistory a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.businessunit_gid='" + businessunit_gid + "' order by a.businessunitstatushistory_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getinactivehistory_list = new List<businessunitstatusHistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getinactivehistory_list.Add(new businessunitstatusHistory_list
                        {
                            businessunit_status = (dr_datarow["businessunit_status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    values.businessunitstatusHistory_list = getinactivehistory_list;
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