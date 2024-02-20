using ems.master.Models;
using ems.utilities.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;

namespace ems.master.DataAccess
{
    public class DaGroupWaiver
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGetGidREF, msGetassignmember_gid, msGetAPICode;
        int mnResult;      
        string lsmaster_value, lsgroupwaiver_gid;
    
        //Add

        public void DaPostGroupWaiver(MdlMstGroupWaiver values, string employee_gid)
        {
            msSQL = "select groupwaiver_name from ocs_mst_tgroupwaiver where groupwaiver_name = '" + values.groupwaiver_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "Group Waiver Name Already Exist";

            }
            else
            {
                msGetAPICode = objcmnfunctions.GetApiMasterGID("GWSC");
                msGetGid = objcmnfunctions.GetMasterGID("GROW");
                msGetGidREF = objcmnfunctions.GetMasterGID("GWC");
                msSQL = " insert into ocs_mst_tgroupwaiver(" +
                       " groupwaiver_gid ," +
                       " groupwaiver_code ," +
                       " api_code ," +
                       " groupwaiver_name," +
                       " description," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + msGetGidREF + "'," +
                       "'" + msGetAPICode + "'," +
                       "'" + values.groupwaiver_name.Replace("'", "") + "',";
                if (values.description == null || values.description == "")
                {
                    msSQL += "'',";
                }
                else
                {
                    msSQL += "'" + values.description.Replace("'", "") + "',";
                }
                msSQL += "'" + employee_gid + "'," +
                "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                for (var i = 0; i < values.assignmember.Count; i++)
                {
                    msGetassignmember_gid = objcmnfunctions.GetMasterGID("GRAM");
                    msSQL = "Insert into ocs_mst_tassignmember( " +
                           " assignmember_gid, " +
                           " groupwaiver_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetassignmember_gid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.assignmember[i].employee_gid + "'," +
                           "'" + values.assignmember[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Group Waiver Added Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
        }

        public void DaGetGroupWaiver(MdlMstGroupWaiver objmaster)
        {
            try
            {
                msSQL = "select a.groupwaiver_gid ,a.groupwaiver_code,a.groupwaiver_name,a.description, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date," +
                    " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,api_code," +
                    "  case when a.status='N' then 'Inactive' else 'Active' end as Status " +
                    " from ocs_mst_tgroupwaiver a " +
                    " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getgroupwaiver_list = new List<groupwaiver>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getgroupwaiver_list.Add(new groupwaiver
                        {
                            groupwaiver_gid = (dr_datarow["groupwaiver_gid"].ToString()),
                            groupwaiver_code = (dr_datarow["groupwaiver_code"].ToString()),
                            groupwaiver_name = (dr_datarow["groupwaiver_name"].ToString()),
                            description = (dr_datarow["description"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            Status = (dr_datarow["Status"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objmaster.groupwaiver = getgroupwaiver_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }

        public void DaGetAssignMember(string groupwaiver_gid, groupassignmember values)
        {
            msSQL = " select group_concat(employee_name) as employee_name  from ocs_mst_tassignmember " +
                  " where groupwaiver_gid='" + groupwaiver_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.groupassignmembers = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();
        }

        //Edit

        public void DaGetGroupWaiverEdit(string groupwaiver_gid, MdlMstGroupWaiver objmaster)
        {
            msSQL = " select groupwaiver_gid,groupwaiver_name,groupwaiver_code,description,status as Status  from ocs_mst_tgroupwaiver " +
                    " where groupwaiver_gid='" + groupwaiver_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.groupwaiver_gid = objODBCDatareader["groupwaiver_gid"].ToString();
                objmaster.groupwaiver_code = objODBCDatareader["groupwaiver_code"].ToString();
                objmaster.groupwaiver_name = objODBCDatareader["groupwaiver_name"].ToString();
                objmaster.description = objODBCDatareader["description"].ToString();
                objmaster.Status = objODBCDatareader["Status"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select assignmember_gid,employee_gid,employee_name from ocs_mst_tassignmember " +
               " where groupwaiver_gid='" + groupwaiver_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getassignmemberList = new List<assignmember>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getassignmemberList.Add(new assignmember
                    {
                        assignmember_gid = dt["assignmember_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    objmaster.assignmember = getassignmemberList;
                }
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
            " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
            " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_assignmemberemployee = new List<assignmember_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.assignmember_list = dt_datatable.AsEnumerable().Select(row =>
                  new assignmember_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();
        }

        public bool DaUpdateGroupWaiver(string employee_gid, MdlMstGroupWaiver values)
        {
            msSQL = "select groupwaiver_gid from ocs_mst_tgroupwaiver where groupwaiver_name = '" + values.groupwaiver_name.Replace("'", "\\'") + "'";
            lsgroupwaiver_gid = objdbconn.GetExecuteScalar(msSQL);
            if (lsgroupwaiver_gid != "")
            {
                if (lsgroupwaiver_gid != values.groupwaiver_gid)
                {
                    values.message = "Group Waiver Name Already Exist";
                    values.status = false;
                    return false;
                }
            }

            msSQL = "select updated_by, updated_date,groupwaiver_name,groupwaiver_code from ocs_mst_tgroupwaiver where groupwaiver_gid ='" + values.groupwaiver_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("GRWL");
                    msSQL = " insert into ocs_mst_tgroupwaiverlog(" +
                            " groupwaiverlog_gid," +
                            " groupwaiver_gid," +
                            " groupwaiver_code , " +
                            " groupwaiver_name," +
                            " updated_by, " +
                            " updated_date) " +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + values.groupwaiver_gid + "'," +
                            "'" + objODBCDatareader["groupwaiver_code"].ToString().Replace("'", "") + "'," +
                            "'" + objODBCDatareader["groupwaiver_name"].ToString().Replace("'", "") + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = "update ocs_mst_tgroupwaiver set groupwaiver_name='" + values.groupwaiver_name.Replace("'", "") + "'," +
                    " groupwaiver_code='" + values.groupwaiver_code.Replace("'", "") + "',";
            if (values.description == null || values.description == "")
            {
                msSQL += "description='',";
            }
            else
            {
                msSQL += "description='" + values.description.Replace("'", "") + "',";

            }
            msSQL += " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where groupwaiver_gid='" + values.groupwaiver_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from ocs_mst_tassignmember where groupwaiver_gid ='" + values.groupwaiver_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.assignmember.Count; i++)
                {
                    msGetassignmember_gid = objcmnfunctions.GetMasterGID("GRAM");
                    msSQL = "Insert into ocs_mst_tassignmember( " +
                           " assignmember_gid, " +
                           " groupwaiver_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetassignmember_gid + "'," +
                           "'" + values.groupwaiver_gid + "'," +
                           "'" + values.assignmember[i].employee_gid + "'," +
                           "'" + values.assignmember[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Group  Waiver Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating Group Waiver";
                return false;
            }

        }

        // Status

        public void DaInactiveGroupWaiver(MdlMstGroupWaiver values, string employee_gid)
        {
            msSQL = " update ocs_mst_tgroupwaiver set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where groupwaiver_gid='" + values.groupwaiver_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("GRIL");

                msSQL = " insert into ocs_mst_tgroupwaiverinactivelog (" +
                      " groupwaiverinactivelog_gid, " +
                      " groupwaiver_gid," +
                      " groupwaiver_code," +
                      " groupwaiver_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.groupwaiver_gid + "'," +
                      " '" + values.groupwaiver_code + "'," +
                       " '" + values.groupwaiver_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Group Waiver Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Group Waiver Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaInactiveGroupWaiverHistory(MdlMstGroupWaiver objmaster, string groupwaiver_gid)
        {
            try
            {
                msSQL = " select a.remarks, date_format(a.updated_date,'%d-%m-%Y %h:%i %p') as updated_date, " +
                " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                " from ocs_mst_tgroupwaiverinactivelog a " +
                " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                " where a.groupwaiver_gid='" + groupwaiver_gid + "' order by a.groupwaiverinactivelog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getgroupwaiverinactivehistory_list = new List<groupwaiverinactivehistory_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getgroupwaiverinactivehistory_list.Add(new groupwaiverinactivehistory_list
                        {
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString())
                        });
                    }
                    objmaster.groupwaiverinactivehistory_list = getgroupwaiverinactivehistory_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        //Delete

        public void DaDeleteGroupWaiver(string groupwaiver_gid, string employee_gid, result values)
        {

            msSQL = " select groupwaiver_name from ocs_mst_tgroupwaiver where groupwaiver_gid='" + groupwaiver_gid + "'";
            lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " delete from ocs_mst_tgroupwaiver where groupwaiver_gid='" + groupwaiver_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                         "master_gid, " +
                         "master_name, " +
                         "master_value, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'Group Waive'," +
                         "'" + lsmaster_value + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                values.message = "Group Waiver Deleted Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }

        }

    }
}