using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.master.Models;

/// <summary>
/// (It's used for pages in CADGroup)CADGroup DataAccess Class accessed by API methods from related Controller class and is returning relevant response to client.
/// </summary>
/// <remarks>Written by Sumala,Logapriya and Abilash</remarks>

namespace ems.master.DataAccess
{
    public class DaCADGroup
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader;
        string msSQL, msGetGid, msGetcadgroupmanager_gid, msGetcadgroupmembers_gid, msGetcadgroupmanagerlog_gid, msGetcadgroupmemberslog_gid, msGetAPICode;
        int mnResult;               
        string lsmaster_value, lsgroupgid;

        // Add

        public void DaPostCADGroup(MdlCadGroup values, string employee_gid)
        {
            msSQL = "select cadgroup_name from ocs_mst_tcadgroup where cadgroup_name = '" + values.cadgroup_name.Replace("'", "\\'") + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.status = false;
                values.message = "CAD Group Name Already Exist";
            }           
            else
            {
                msGetAPICode = objcmnfunctions.GetApiMasterGID("CGSA");
                msGetGid = objcmnfunctions.GetMasterGID("CDGR");
                msSQL = " insert into ocs_mst_tcadgroup(" +
                        " cadgroup_gid ," +
                        " cadgroup_name," +
                        " api_code," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.cadgroup_name.Replace("'", "") + "'," +
                        "'" + msGetAPICode + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                for (var i = 0; i < values.cadmanager.Count; i++)
                {
                    msGetcadgroupmanager_gid = objcmnfunctions.GetMasterGID("CDMA");
                    msSQL = "Insert into ocs_mst_tcadgroupmanager( " +
                           " cadgroupmanager_gid, " +
                           " cadgroup_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetcadgroupmanager_gid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.cadmanager[i].employee_gid + "'," +
                           "'" + values.cadmanager[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                for (var i = 0; i < values.cadmembers.Count; i++)
                {
                    msGetcadgroupmembers_gid = objcmnfunctions.GetMasterGID("CDME");
                    msSQL = "Insert into ocs_mst_tcadgroupmembers( " +
                           " cadgroupmembers_gid, " +
                           " cadgroup_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetcadgroupmembers_gid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.cadmembers[i].employee_gid + "'," +
                           "'" + values.cadmembers[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "CAD Group Added Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
        }

        public void DaGetCADGroup(MdlCadGroup objmaster)
        {
            try
            {
                msSQL = " SELECT a.cadgroup_gid ,a.cadgroup_name, date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, api_code " +
                        " FROM ocs_mst_tcadgroup a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getcadgroup_list = new List<cadgroup>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getcadgroup_list.Add(new cadgroup
                        {
                            cadgroup_gid = (dr_datarow["cadgroup_gid"].ToString()),
                            cadgroup_name = (dr_datarow["cadgroup_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objmaster.cadgroup = getcadgroup_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }

        //Edit

        public void DaGetCADGroupEdit(string cadgroup_gid, MdlCadGroup objmaster)
        {
            msSQL = " select cadgroup_gid,cadgroup_name  from ocs_mst_tcadgroup " +
                    " where cadgroup_gid='" + cadgroup_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objmaster.cadgroup_gid = objODBCDatareader["cadgroup_gid"].ToString();
                objmaster.cadgroup_name = objODBCDatareader["cadgroup_name"].ToString();

            }
            objODBCDatareader.Close();
            msSQL = " select cadgroupmanager_gid,employee_gid,employee_name from ocs_mst_tcadgroupmanager " +
                  " where cadgroup_gid='" + cadgroup_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getcadmanagerList = new List<cadmanager>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getcadmanagerList.Add(new cadmanager
                    {
                        cadgroupmanager_gid = dt["cadgroupmanager_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    objmaster.cadmanager = getcadmanagerList;
                }
            }
            dt_datatable.Dispose();
            msSQL = " select cadgroupmembers_gid,employee_gid,employee_name from ocs_mst_tcadgroupmembers " +
                " where cadgroup_gid='" + cadgroup_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getCadmembersList = new List<cadmembers>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getCadmembersList.Add(new cadmembers
                    {
                        cadgroupmembers_gid = dt["cadgroupmembers_gid"].ToString(),
                        employee_gid = dt["employee_gid"].ToString(),
                        employee_name = dt["employee_name"].ToString(),
                    });
                    objmaster.cadmembers = getCadmembersList;
                }
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
               " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
               " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_cadmanageremployee = new List<cadmanager_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.cadmanager_list = dt_datatable.AsEnumerable().Select(row =>
                  new cadmanager_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();

            msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
               " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
               " where user_status<>'N' order by a.user_firstname asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_cadmemberssemployee = new List<cadmembers_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                objmaster.cadmembers_list = dt_datatable.AsEnumerable().Select(row =>
                  new cadmembers_list
                  {
                      employee_gid = row["employee_gid"].ToString(),
                      employee_name = row["employee_name"].ToString()
                  }
                ).ToList();
            }
            dt_datatable.Dispose();
        }

        public bool DaCADGroupUpdate(string employee_gid, MdlCadGroup values)
        {
            msSQL = "select cadgroup_gid from ocs_mst_tcadgroup where cadgroup_name = '" + values.cadgroup_name.Replace("'", "\\'") + "'";
            lsgroupgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsgroupgid != "")
            {
                if (lsgroupgid != values.cadgroup_gid)
                {
                    values.status = false;
                    values.message = "CAD Group Name Already Exist";
                    return false;
                }
            }

            msSQL = "select updated_by, updated_date,cadgroup_name from ocs_mst_tcadgroup where cadgroup_gid ='" + values.cadgroup_gid + "' ";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);

            if (objODBCDatareader.HasRows == true)
            {
                string lsUpdatedBy = objODBCDatareader["updated_by"].ToString();
                string lsUpdatedDate = objODBCDatareader["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("RGNL");
                    msSQL = " insert into ocs_mst_tcadgrouplog(" +
                              " cadgrouplog_gid," +
                              " cadgroup_gid," +
                              " cadgroup_name , " +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.cadgroup_gid + "'," +
                              "'" + objODBCDatareader["cadgroup_name"].ToString().Replace("'", "") + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            objODBCDatareader.Close();

            msSQL = "update ocs_mst_tcadgroup set cadgroup_name='" + values.cadgroup_name.Replace("'", "") + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where cadgroup_gid='" + values.cadgroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from ocs_mst_tcadgroupmanager where cadgroup_gid ='" + values.cadgroup_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.cadmanager.Count; i++)
                {
                    msGetcadgroupmanager_gid = objcmnfunctions.GetMasterGID("CDMA");
                    msSQL = "Insert into ocs_mst_tcadgroupmanager( " +
                           " cadgroupmanager_gid, " +
                           " cadgroup_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetcadgroupmanager_gid + "'," +
                           "'" + values.cadgroup_gid + "'," +
                           "'" + values.cadmanager[i].employee_gid + "'," +
                           "'" + values.cadmanager[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            msSQL = " delete from ocs_mst_tcadgroupmembers where cadgroup_gid ='" + values.cadgroup_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.cadmembers.Count; i++)
                {
                    msGetcadgroupmembers_gid = objcmnfunctions.GetMasterGID("CDME");
                    msSQL = "Insert into ocs_mst_tcadgroupmembers( " +
                           " cadgroupmembers_gid, " +
                           " cadgroup_gid," +
                           " employee_gid," +
                           " employee_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGetcadgroupmembers_gid + "'," +
                           "'" + values.cadgroup_gid + "'," +
                           "'" + values.cadmembers[i].employee_gid + "'," +
                           "'" + values.cadmembers[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "CAD Group Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating CAD Group";
                return false;
            }

        }

        public void DaGetCADGroupEmployee(string cadgroup_gid, cadgrouphead values)
        {
            msSQL = " select group_concat(employee_name) as employee_name  from ocs_mst_tcadgroupmanager " +
                  " where cadgroup_gid='" + cadgroup_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.cadgroupmanager = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select group_concat(employee_name) as employee_name  from ocs_mst_tcadgroupmembers " +
                 " where cadgroup_gid='" + cadgroup_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.cadgroupmember = objODBCDatareader["employee_name"].ToString();
            }
            objODBCDatareader.Close();
        }
       
        //Delete

        public void DaDeleteCADGroup(string cadgroup_gid, string employee_gid, result values)
        {
            msSQL = " select cadgroupassign_gid from ocs_mst_tcadgroupassignment where cadgroup_gid = '" + cadgroup_gid + "' and delete_flag = 'N'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.message = "CAD Group has Assigned for CAD Group Assignment, You Cannot Delete";
                values.status = false;
            }
            else
            {
                msSQL = " select cadgroup_name from ocs_mst_tcadgroup where cadgroup_gid='" + cadgroup_gid + "'";
                lsmaster_value = objdbconn.GetExecuteScalar(msSQL);               

                msSQL = " select group_concat(distinct (a.employee_gid)) as manager_gid," +
                        " group_concat(distinct(a.employee_name)) as manager_name, " +
                        " group_concat(distinct (b.employee_gid)) as member_gid," +
                        " group_concat(distinct(b.employee_name)) as member_name " +
                        " from ocs_mst_tcadgroupmanager a" +
                        " left join ocs_mst_tcadgroupmembers b on b.cadgroup_gid=a.cadgroup_gid   " +
                        " where a.cadgroup_gid='" + cadgroup_gid + "'";
                objODBCDatareader = objdbconn.GetDataReader(msSQL);
                if (objODBCDatareader.HasRows == true)
                {
                    values.lsmanager_gid = objODBCDatareader["manager_gid"].ToString();
                    values.lsmanager_name = objODBCDatareader["manager_name"].ToString();
                    values.lsmember_gid = objODBCDatareader["member_gid"].ToString();
                    values.lsmember_name = objODBCDatareader["member_name"].ToString();
                }
                objODBCDatareader.Close();
                msGetcadgroupmanagerlog_gid = objcmnfunctions.GetMasterGID("CGML");
                msSQL = "Insert into ocs_mst_tcadgroupmanagerlog( " +
                       " cadgroupmanagerlog_gid, " +
                       " cadgroup_gid," +
                       " employee_gid," +
                       " employee_name," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetcadgroupmanagerlog_gid + "'," +
                       "'" + cadgroup_gid + "'," +
                       "'" + values.lsmanager_gid + "'," +
                       "'" + values.lsmanager_name + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msGetcadgroupmemberslog_gid = objcmnfunctions.GetMasterGID("CMML");
                msSQL = " Insert into ocs_mst_tcadgroupmemberslog( " +
                       " cadgroupmemberslog_gid, " +
                       " cadgroup_gid," +
                       " employee_gid," +
                       " employee_name," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetcadgroupmemberslog_gid + "'," +
                       "'" + cadgroup_gid + "'," +
                       "'" + values.lsmember_gid + "'," +
                       "'" + values.lsmember_name + "'," +
                       "'" + employee_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from ocs_mst_tcadgroup where cadgroup_gid='" + cadgroup_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msSQL = " delete from ocs_mst_tcadgroupmanager where cadgroup_gid='" + cadgroup_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msSQL = " delete from ocs_mst_tcadgroupmembers where cadgroup_gid='" + cadgroup_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                    msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                             "master_gid, " +
                             "master_name, " +
                             "master_value, " +
                             "deleted_by, " +
                             "deleted_date) " +
                             " values(" +
                             "'" + msGetGid + "'," +
                             "'CAD Group'," +
                             "'" + lsmaster_value + "'," +
                             "'" + employee_gid + "'," +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    values.status = true;
                    values.message = "CAD Group Deleted Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }

            }
        }
    }
}