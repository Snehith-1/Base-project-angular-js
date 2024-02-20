using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Functions;
using ems.rms.Models;
using System.Data.Odbc;
using System.Data;

namespace ems.rms.DataAccess
{
    public class Dateam2Recruiter
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcConnection objODBCconnection;
  
        DataTable dt_datatable;
        string msSQL;
        public bool Dagetteam2recruiter(string businessunit2team_gid, mdlteam2recruiter values)
        {
          

            msSQL = " select concat(e.user_firstname,' ',e.user_lastname) as recruiter,b.businessunitteam_name,count(a.recruitmentadd_gid) as candidate_sourced," +
                    " count(c.candidateconfirm_gid) as candidate_joined from rms_trn_trecruitmentadd a " +
                    " left join rms_mst_tbusinessunit2team b on b.businessunit2team_gid = a.businessunit2team_gid " +
                    " left join rms_trn_tcandidateconfirm c on c.recruitmentadd_gid =a.recruitmentadd_gid " +
                    " left join hrm_mst_temployee d on d.employee_gid = a.recruiter_gid " +
                    " left join adm_mst_tuser e on e.user_gid = d.user_gid " +
                    " where a.businessunit2team_gid='"+businessunit2team_gid+ "' group by a.recruiter_gid";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getteam2recruiter = new List<team2recruiter_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {
                    getteam2recruiter.Add(new team2recruiter_list
                    {

                        recruitername = (dr_datarow["recruiter"].ToString()),
                        businessunitteam = (dr_datarow["businessunitteam_name"].ToString()),
                        candidate_sourced = (dr_datarow["candidate_sourced"].ToString()),
                        candidate_joined = (dr_datarow["candidate_joined"].ToString())

                    });
                }
                values.team2recruiter_list = getteam2recruiter;
            }
            dt_datatable.Dispose();
            
            return true;
        }
    }
}