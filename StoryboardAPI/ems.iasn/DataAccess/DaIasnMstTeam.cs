using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Odbc;
using ems.utilities.Functions;
using ems.iasn.Models;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;

namespace ems.iasn.DataAccess
{
    public class DaIasnMstTeam
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable;
        string msSQL;
        OdbcDataReader objODBCDataReader;
        string msGetGid, msGetGidCode,msGetChildGid;
        int mnResult;

        public result DaPostTeam(MdlAddTeam values,string user_gid)
        {
            result objResult = new iasn.Models.result();
            msGetGid = objcmnfunctions.GetMasterGID("TEAM");
            msGetGidCode = objcmnfunctions.GetMasterGID("TC");
            msSQL = " INSERT INTO isn_mst_tteam(" +
                    " team_gid," +
                    " team_code," +
                    " team_name,"+
                    " description,"+
                    " zone_name,"+
                    " team_mailid,"+
                    " created_by,"+
                    " created_date)"+
                    " values("+
                    "'"+ msGetGid+"',"+
                    "'"+ msGetGidCode + "',"+
                    "'"+values.team_name +"',"+
                    "'"+values.description .Replace("'","")+"',"+
                    "'"+values.zonal_name.Replace("'","\\'") +"',"+
                    "'"+values.team_mailid .Replace("'","")+"',"+
                    "'"+user_gid +"',"+
                    "current_timestamp)";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (values .MdlRmList .Count != 0)
            {
                for (int i = 0; i < values.MdlRmList.Count; i++)
                {
                    msSQL = " select employeelist_gid from isn_mst_temployeelist where employee_gid='" + values.MdlRmList[i].employee_gid + "' and employee_type='RM'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL );
                    if (objODBCDataReader .HasRows ==false)
                    {
                        objODBCDataReader.Close();
                        msGetChildGid = objcmnfunctions.GetMasterGID("EMPL");
                        msSQL = " INSERT INTO isn_mst_temployeelist(" +
                                " employeelist_gid," +
                                " team_gid ," +
                                " employee_gid ," +
                                " employee_name ," +
                                " employee_emailid ," +
                                " employee_type ," +
                                " created_by ," +
                                " created_date )" +
                                " VALUES(" +
                                "'" + msGetChildGid + "'," +
                                "'" + msGetGid + "'," +
                                "'" + values.MdlRmList[i].employee_gid + "'," +
                                "'" + values.MdlRmList[i].employee_name + "'," +
                                 "(select employee_emailid from hrm_mst_temployee where employee_gid='" + values.MdlRmList[i].employee_gid + "')," +
                                "'RM'," +
                                "'" + user_gid + "'," +
                                "current_timestamp)";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                    else
                    {
                        objODBCDataReader.Close();
                    }
                 
                }
            }
            if (values.MdlCheckerList .Count != 0)
            {
                for (int i = 0; i < values.MdlCheckerList.Count; i++)
                {
                    msGetChildGid = objcmnfunctions.GetMasterGID("EMPL");
                    msSQL = " INSERT INTO isn_mst_temployeelist(" +
                            " employeelist_gid," +
                            " team_gid ," +
                            " employee_gid ," +
                            " employee_name ," +
                            " employee_emailid ," +
                            " employee_type ," +
                            " created_by ," +
                            " created_date )" +
                            " VALUES(" +
                            "'" + msGetChildGid + "'," +
                            "'" + msGetGid + "'," +
                            "'" + values.MdlCheckerList [i].employee_gid + "'," +
                            "'" + values.MdlCheckerList [i].employee_name + "'," +
                            "(select employee_emailid from hrm_mst_temployee where employee_gid='"+ values.MdlCheckerList[i].employee_gid  + "'),"+
                            "'Checker'," +
                            "'" + user_gid + "'," +
                            "current_timestamp)";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            if (mnResult ==1)
            {
                objResult.status = true;
                objResult.message = "Team Added Successfully";
                return objResult;
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
                return objResult;
            }
        }

        public void DaGetTeam(MdlTeamSummaryList values)
        {
            msSQL = " SELECT a.team_gid, a.team_code,a.team_name,a.team_mailid,a.zone_name,a.description ," +
                    " CONCAT(b.user_code, ' /', b.user_firstname, b.user_lastname) as created_by,date_format(a.created_date, '%d-%m-%Y') as created_date" +
                    " FROM isn_mst_tteam a" +
                    " LEFT JOIN adm_mst_tuser b on a.created_by = b.user_gid" +
                    " WHERE 1 = 1 ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if(dt_datatable .Rows .Count!=0)
            {
                values .MdlTeamSummary =dt_datatable .AsEnumerable().Select(row=>new MdlTeamSummary
                {
                    team_gid =row["team_gid"].ToString (),
                    team_code=row["team_code"].ToString (),
                    team_name=row["team_name"].ToString (),
                    team_mailid=row["team_mailid"].ToString (),
                    zone_name=row["zone_name"].ToString (),
                    description=row["description"].ToString (),
                    created_by=row["created_by"].ToString (),
                    created_date=row["created_date"].ToString ()

                }).ToList();
                dt_datatable.Dispose();
                values.status = true;
                values.message = "Success";
            }
            else
            {
                values.status = false;
                values.message = "No Record Found";
            }

        }

        public void DaGetTeamEdit(string lsteam_gid, MdlAddTeam values)
        {
          
            msSQL = " SELECT a.team_code, a.team_name,a.team_mailid,a.zone_name,a.description " +
                    " FROM isn_mst_tteam a" +
                    " WHERE a.team_gid='" + lsteam_gid + "'";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader .HasRows ==true)
            {
                objODBCDataReader.Read();
                values.team_code = objODBCDataReader["team_code"].ToString();
                values.team_name = objODBCDataReader["team_name"].ToString();
                values.team_mailid = objODBCDataReader["team_mailid"].ToString();
                values.zonal_name = objODBCDataReader["zone_name"].ToString();
                values.description = objODBCDataReader["description"].ToString();
            }
            objODBCDataReader.Close();
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,'/',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employee_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    values.employee_list = dt_datatable.AsEnumerable().Select(row =>
                      new employee_list
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
              
            }
            catch (Exception ex)
            {
               
            }


            msSQL = " select employee_gid ,employee_name " +
                    " from isn_mst_temployeelist" +
                    " where team_gid='" + lsteam_gid + "' and employee_type ='RM'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlRmList  = dt_datatable.AsEnumerable().Select(row => new MdlRm 
                {
                    employee_gid =row["employee_gid"].ToString (),
                    employee_name =row["employee_name"].ToString ()

                }).ToList();
               
               
            }
            dt_datatable.Dispose();

            msSQL = " select employee_gid ,employee_name " +
                  " from isn_mst_temployeelist" +
                  " where team_gid='" + lsteam_gid + "' and employee_type ='Checker'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            if (dt_datatable.Rows.Count != 0)
            {
                values.MdlCheckerList  = dt_datatable.AsEnumerable().Select(row => new MdlChecker 
                {
                    employee_gid = row["employee_gid"].ToString(),
                    employee_name = row["employee_name"].ToString()

                }).ToList();


            }
            dt_datatable.Dispose();

        }

        public result   DaPostUpdateTeam(MdlAddTeam values,string user_gid)
        {
            result objResult = new iasn.Models.result();
         
            msSQL = " UPDATE isn_mst_tsteam SET" +
                    " team_name='"+ values.team_name  +"'," +
                    " description='"+ values.description .Replace ("'","")+"'," +
                    " zone_name='"+ values .zonal_name.Replace("'","\\'") +"'," +
                    " team_mailid='"+ values .team_mailid +"'," +
                    " updated_by='"+ user_gid + "'," +
                    " updated_date=current_timestamp" +
                   " WHERE team_gid='"+ values.team_gid+"'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " DELETE FROM isn_mst_temployeelist" +
                    " WHERE team_gid='" + values.team_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (values.MdlRmList.Count != 0)
            {
               
                for (int i = 0; i < values.MdlRmList.Count; i++)
                {
                    msSQL = " select employeelist_gid from isn_mst_temployeelist where employee_gid='" + values.MdlRmList[i].employee_gid + "' and employee_type='RM'";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows == false)
                    {
                        objODBCDataReader.Close();
                        msGetChildGid = objcmnfunctions.GetMasterGID("EMPL");
                        msSQL = " INSERT INTO isn_mst_temployeelist(" +
                                " employeelist_gid," +
                                " team_gid ," +
                                " employee_gid ," +
                                " employee_name ," +
                                " employee_emailid ," +
                                " employee_type ," +
                                " created_by ," +
                                " created_date )" +
                                " VALUES(" +
                                "'" + msGetChildGid + "'," +
                                "'" + values.team_gid + "'," +
                                "'" + values.MdlRmList[i].employee_gid + "'," +
                                "'" + values.MdlRmList[i].employee_name + "'," +
                                "(select employee_emailid from hrm_mst_temployee where employee_gid='" + values.MdlRmList[i].employee_gid + "')," +
                                "'RM'," +
                                "'" + user_gid + "'," +
                                "current_timestamp)";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                    else
                    {
                        objODBCDataReader.Close();
                    }
                  
                }
            }
            if (values.MdlCheckerList.Count != 0)
            {
                for (int i = 0; i < values.MdlCheckerList.Count; i++)
                {
                      msGetChildGid = objcmnfunctions.GetMasterGID("EMPL");
                        msSQL = " INSERT INTO isn_mst_temployeelist(" +
                                " employeelist_gid," +
                                " team_gid ," +
                                " employee_gid ," +
                                " employee_name ," +
                                " employee_emailid ," +
                                " employee_type ," +
                                " created_by ," +
                                " created_date )" +
                                " VALUES(" +
                                "'" + msGetChildGid + "'," +
                                "'" + values.team_gid + "'," +
                                "'" + values.MdlCheckerList [i].employee_gid + "'," +
                                "'" + values.MdlCheckerList [i].employee_name + "'," +
                                "(select employee_emailid from hrm_mst_temployee where employee_gid='" + values.MdlCheckerList[i].employee_gid + "')," +
                                "'Checker'," +
                                "'" + user_gid + "'," +
                                "current_timestamp)";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                  }
            }
            if (mnResult == 1)
            {
                objResult.status = true;
                objResult.message = "Team Updated Successfully";
                return objResult;
            }
            else
            {
                objResult.status = false;
                objResult.message = "Error Occured";
                return objResult;
            }
        }
    }
}