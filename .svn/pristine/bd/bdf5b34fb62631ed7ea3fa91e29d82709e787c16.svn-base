using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.osd.Models;

namespace ems.osd.DataAccess
{
    public class DaOsdMstSupportTeam
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcDataReader objODBCDatareader;
        DataTable dt_datatable;
        DataTable dt_child;
        string msSQL, msGetGid, msGetDocumentGid, mspSQL, msSQL1;
        string msGetTeamCode, msGet_teamGid;
        int mnResult;
        string teamGID = string.Empty;
        private string team_name;

        public void DaPostSupportTeamAdd(supportteamdtl values, string user_gid)
        {
            msSQL = "select team_name from osd_mst_tsupportteam where team_name = '" + values.team_name.Replace("'", "\\'") + "' and department_gid ='" + values.department_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                objODBCDatareader.Close();
                values.status = false;
                values.message = "Team Name Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("SPTM");

                msGetTeamCode = objcmnfunctions.GetMasterGID("SPT");

                msSQL = " insert into osd_mst_tsupportteam(" +
                       " supportteam_gid," +
                       " department_gid," +
                       " department_name," +
                       " team_code, " +
                       " team_name, " +
                       " team_description," +
                       " created_by," +
                       " created_date)" +
                       " values(" +
                       "'" + msGetGid + "'," +
                       "'" + values.department_gid + "'," +
                       "'" + values.department_name + "'," +
                       "'" + msGetTeamCode + "', " +
                       "'" + values.team_name.Replace("'", "\\'") + "'," +
                       "'" + values.team_description.Replace("'", "\\'") + "'," +
                       "'" + user_gid + "'," +
                       "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                for (var i = 0; i < values.teammembers.Count; i++)
                {
                    msGet_teamGid = objcmnfunctions.GetMasterGID("ST2M");

                    msSQL = "Insert into osd_mst_tsupportteam2member( " +
                           " supportteam2member_gid, " +
                           " supportteam_gid," +
                           " member_gid," +
                           " member_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGet_teamGid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.teammembers[i].employee_gid + "'," +
                           "'" + values.teammembers[i].employee_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Support Team Details are Added Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }

                objODBCDatareader.Close();
            }
            
        }

        public bool DaGetSupportTeamUpdate(supportteamdtl values, string user_gid)
        {
            bool status = false;                       

            mspSQL += "(";
            for (var i = 0; i < values.teammembers.Count; i++)
            {
                mspSQL += "'" + values.teammembers[i].employee_gid + "',";
            }
            msSQL = "select supportteam2member_gid,supportteam_gid,member_gid from osd_mst_tsupportteam2member where supportteam_gid='" + values.supportteam_gid + "'";
            msSQL += " and member_gid not in " + mspSQL.TrimEnd(',') + "";
            msSQL += ")";

            dt_datatable = objdbconn.GetDataTable(msSQL);

            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    string employee_gid = dt["member_gid"].ToString(),

                     msSQL = " select request_status,activitymaster_gid,request_refno,assigned_membergid from osd_trn_tservicerequest where assigned_membergid = '" + employee_gid + "' and assigned_supportteamgid = '"+ values.supportteam_gid + "' and " +
                             " (request_status = 'Allotted' or request_status = 'Work-In-Progress')";
                    objODBCDatareader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDatareader.HasRows == true)
                    {
                        objODBCDatareader.Close();
                        values.message = "Member Name Already Assigned an Activity, Can't able to delete the Member Name";
                        values.status = false;
                        return status;
                    }
                    else
                    {
                        objODBCDatareader.Close();
                    }
                }
            }
            dt_datatable.Dispose();

            msSQL = "select supportteam_gid from osd_mst_tsupportteam where team_name='" + values.team_name.Replace("'", "\\'") + "' and department_gid ='" + values.department_gid + "'";
            teamGID = objdbconn.GetExecuteScalar(msSQL);

            msSQL = "select team_name from osd_mst_tsupportteam where team_name='" + values.team_name.Replace("'", "\\'") + "' and department_gid ='" + values.department_gid + "'";
            team_name = objdbconn.GetExecuteScalar(msSQL);

            if (teamGID != "")
            {
                if (teamGID != values.supportteam_gid)
                {
                    values.status = false;
                    values.message = "Team Name Already Exist";
                    return status;
                }
            }

            msSQL = " update osd_mst_tsupportteam set " +
              " department_gid='" + values.department_gid + "'," +
              " department_name='" + values.department_name + "'," +
              " team_name='" + values.team_name.Replace("'", "\\'") + "'," +
              " team_description='" + values.team_description.Replace("'", "\\'") + "'," +
              " updated_by='" + user_gid + "'," +
              " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
              " where supportteam_gid='" + values.supportteam_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update osd_mst_tactivitymaster set  supportteam_name ='" + values.team_name.Replace("'", "\\'")  + "'  where supportteam_gid ='" + values.supportteam_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = "update osd_trn_tservicerequest set  assigned_supportteamname ='" + values.team_name.Replace("'", "\\'") + "'  where assigned_supportteamgid ='" + values.supportteam_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from osd_mst_tsupportteam2member where supportteam_gid ='" + values.supportteam_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                for (var i = 0; i < values.teammembers.Count; i++)
                {



                    msGet_teamGid = objcmnfunctions.GetMasterGID("ST2M");

                    msSQL = "Insert into osd_mst_tsupportteam2member( " +
                           " supportteam2member_gid, " +
                           " supportteam_gid," +
                           " member_gid," +
                           " member_name," +
                           " created_by," +
                           " created_date)" +
                           " values(" +
                           "'" + msGet_teamGid + "'," +
                           "'" + values.supportteam_gid + "'," +
                           "'" + values.teammembers[i].employee_gid + "'," +
                           "'" + values.teammembers[i].employee_name + "'," +
                           "'" + user_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Support Team Details are Updated Successfully..!";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }
            return status;

        //}
    }

        public void DaGetSupportTeamDelete(string supportteam_gid, result values)
        {
            msSQL = " select activitymaster_gid,activity_code,activity_name,department_gid,supportteam_gid from osd_mst_tactivitymaster where supportteam_gid = '" + supportteam_gid + "'";

            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows)
            {
                objODBCDatareader.Close();
                values.message = "Team Members has Assigned Activity, You Cannot Delete";
                values.status = false;
            }
            else
            {
                msSQL = " delete from osd_mst_tsupportteam where supportteam_gid='" + supportteam_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = " delete from osd_mst_tsupportteam2member where supportteam_gid='" + supportteam_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Support Team Details are Deleted Successfully..!";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occured..!";
                }

                objODBCDatareader.Close();
            }

        }

        public void DaGetSupportTeamView(string supportteam_gid, supportteamviewdtl values)
        {
            string lsdepartment_gid;
            lsdepartment_gid = objdbconn.GetExecuteScalar("select department_gid from osd_mst_tsupportteam where supportteam_gid='" + supportteam_gid + "'");
            msSQL = " select team_code,team_name,team_description,department_gid,department_name from osd_mst_tsupportteam " +
                    " where supportteam_gid='" + supportteam_gid + "'";
            objODBCDatareader = objdbconn.GetDataReader(msSQL);
            if (objODBCDatareader.HasRows == true)
            {
                values.team_code = objODBCDatareader["team_code"].ToString();
                values.department_gid = objODBCDatareader["department_gid"].ToString();
                values.department_name = objODBCDatareader["department_name"].ToString();
                values.team_name = objODBCDatareader["team_name"].ToString();
                values.team_description = objODBCDatareader["team_description"].ToString();
            }
            objODBCDatareader.Close();

            msSQL = " select supportteam2member_gid,member_gid,member_name from osd_mst_tsupportteam2member " +
                    " where supportteam_gid='" + supportteam_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getteammembersList = new List<teammembers>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getteammembersList.Add(new teammembers
                    {
                        employee_gid = dt["member_gid"].ToString(),
                        employee_name = dt["member_name"].ToString(),
                        supportteam2member_gid = dt["supportteam2member_gid"].ToString(),
                    });
                    values.teammembers = getteammembersList;
                }
            }
            dt_datatable.Dispose();

            msSQL = " SELECT member_gid,member_name from osd_mst_tactivedepartment2member where department_gid ='" + lsdepartment_gid + "' order by member_name asc";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getemployeeList = new List<employeelist>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getemployeeList.Add(new employeelist
                    {
                        employee_gid = dt["member_gid"].ToString(),
                        employee_name = dt["member_name"].ToString(),
                    });
                    values.employeelist = getemployeeList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetSupportTeamSummary(supportdtllist values, string employee_gid)
        {
            if (employee_gid == "E1" || employee_gid == "SERM1907240067")
            {
                msSQL = " select supportteam_gid,team_code,team_name,team_description ,a.department_name,a.department_gid," +
                     " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                     " from osd_mst_tsupportteam a " +
                     " LEFT JOIN adm_mst_tuser b ON a.created_by=b.user_gid " +
                     " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid order by supportteam_gid desc";
            }
            else
            {
                msSQL = " select supportteam_gid,team_code,team_name,team_description ,a.department_name,a.department_gid," +
                  " concat(b.user_firstname,' ',b.user_lastname,' / ',b.user_code) as created_by,date_format(a.created_date,'%d-%m-%Y %h:%i %p') as created_date" +
                  " from osd_mst_tsupportteam a " +
                  " LEFT JOIN adm_mst_tuser b ON a.created_by=b.user_gid " +
                  " left join osd_mst_tactivedepartment e on e.department_gid = a.department_gid " +
                  " where a.department_gid in (select department_gid from osd_mst_tactivedepartment2member where member_gid='" + employee_gid + "') or " +
                  " a.department_gid in (select department_gid from osd_mst_tactivedepartment2manager where manager_gid='" + employee_gid + "') and e.department_status='Y' " +
                  " order by supportteam_gid desc";
            }
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getsupportdtlList = new List<supportdtl>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getsupportdtlList.Add(new supportdtl
                    {
                        supportteam_gid = dt["supportteam_gid"].ToString(),
                        department_gid = dt["department_gid"].ToString(),
                        department_name = dt["department_name"].ToString(),
                        team_code = dt["team_code"].ToString(),
                        team_name = dt["team_name"].ToString(),
                        team_description = dt["team_description"].ToString(),
                        created_by = dt["created_by"].ToString(),
                        created_date = dt["created_date"].ToString(),
                    });
                    values.supportdtl = getsupportdtlList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetTeamMember(string supportteam_gid, supportteamviewdtl values)
        {
            msSQL = " select supportteam2member_gid,member_gid,member_name from osd_mst_tsupportteam2member " +
                  " where supportteam_gid='" + supportteam_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getteammembersList = new List<teammembers>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getteammembersList.Add(new teammembers
                    {
                        employee_gid = dt["member_gid"].ToString(),
                        employee_name = dt["member_name"].ToString(),
                        supportteam2member_gid = dt["supportteam2member_gid"].ToString(),
                    });
                    values.teammembers = getteammembersList;
                }
            }
            dt_datatable.Dispose();
        }
        // Get team member except current user
        public void DaGetTeamMemberExcept(string supportteam_gid, supportteamviewdtl values, string employee_gid)
        {
            msSQL = " select a.supportteam2member_gid,a.member_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as member_name from osd_mst_tsupportteam2member a" +
                  " left join hrm_mst_temployee b on a.member_gid = b.employee_gid " +
                  " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                  " where supportteam_gid='" + supportteam_gid + "' and member_gid<>'" + employee_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getteammembersList = new List<teammembers>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getteammembersList.Add(new teammembers
                    {
                        employee_gid = dt["member_gid"].ToString(),
                        employee_name = dt["member_name"].ToString(),
                        supportteam2member_gid = dt["supportteam2member_gid"].ToString(),
                    });
                    values.teammembers = getteammembersList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaPostTeamMemberExceptAssigned(supportteamviewdtl values, string employee_gid)
        {
            msSQL = " select a.supportteam2member_gid,a.member_gid, concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as member_name from osd_mst_tsupportteam2member a" +
                  " left join hrm_mst_temployee b on a.member_gid = b.employee_gid " +
                  " left join adm_mst_tuser c on b.user_gid = c.user_gid " +
                  " where supportteam_gid='" + values.supportteam_gid + "' and " +
                  " member_gid<>(select assigned_membergid from osd_trn_tservicerequest where servicerequest_gid='" + values.servicerequest_gid + "')";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getteammembersList = new List<teammembers>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getteammembersList.Add(new teammembers
                    {
                        employee_gid = dt["member_gid"].ToString(),
                        employee_name = dt["member_name"].ToString(),
                        supportteam2member_gid = dt["supportteam2member_gid"].ToString(),
                    });
                    values.teammembers = getteammembersList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetAllTeamMember(supportteamviewdtl values)
        {
            msSQL = " select supportteam2member_gid,member_gid,member_name from osd_mst_tsupportteam2member group by member_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getteammembersList = new List<teammembers>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getteammembersList.Add(new teammembers
                    {
                        employee_gid = dt["member_gid"].ToString(),
                        employee_name = dt["member_name"].ToString(),
                        supportteam2member_gid = dt["supportteam2member_gid"].ToString(),
                    });
                    values.teammembers = getteammembersList;
                }
            }
            dt_datatable.Dispose();
        }

        public void DaGetDeptAllTeamMember(supportteamviewdtl values,string department_gid)
        {
            msSQL = " select a.supportteam2member_gid,a.member_gid,a.member_name from osd_mst_tsupportteam2member a " +
                    "left join osd_mst_tsupportteam b on a.supportteam_gid=b.supportteam_gid " +
                    " where b.department_gid ='" + department_gid + "'" +
                    " group by a.member_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getteammembersList = new List<teammembers>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getteammembersList.Add(new teammembers
                    {
                        employee_gid = dt["member_gid"].ToString(),
                        employee_name = dt["member_name"].ToString(),
                        supportteam2member_gid = dt["supportteam2member_gid"].ToString(),
                    });
                    values.teammembers = getteammembersList;
                }
            }
            dt_datatable.Dispose();
        }
    }
}