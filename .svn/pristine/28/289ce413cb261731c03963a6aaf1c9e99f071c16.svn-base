using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Functions;
using ems.rms.Models;
using System.Data.Odbc;
using System.Data;
using ems.rms.DataAccess;

namespace ems.rms.DataAccess
{
    public class DabusinessunitTeam
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        OdbcConnection objODBCconnection;
        DataTable dt_datatable;
        string msSQL,lsteamcandidate_sourced,lsteamcandidate_joined,lsemployee_photo,lsphoto;
        string lsclosedjob_count, lsopeningjob_count, lsjobfreeze_count;

        public bool DagetbusinessunitTeam_Name(string businessunit_gid, mdlbusinessunitteam values)
        {
          
            msSQL = "select a.businessunitteam_name,a.businessunit2team_gid,a.employee_gid,concat(c.user_firstname,' ',c.user_lastname) as user_name" +
                    " from rms_mst_tbusinessunit2team a, hrm_mst_temployee b, adm_mst_tuser c" +
                    " where  a.employee_gid = b.employee_gid and b.user_gid = c.user_gid and a.businessunit_gid='" + businessunit_gid+"'";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            var get_businessunitteam = new List<businessunitteamname_list>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    msSQL = "select employee_photo from hrm_mst_temployee where employee_gid='" + dt["employee_gid"] + "'";
                    lsemployee_photo = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select count(recruitmentadd_gid) as candidate_sourced from rms_trn_trecruitmentadd" +
                       " where businessunit2team_gid='" + dt["businessunit2team_gid"].ToString() + "'";
                    lsteamcandidate_sourced = objdbconn.GetExecuteScalar(msSQL);

                    msSQL = "select count(recruitmentadd_gid)  as candidate_joined from rms_trn_trecruitmentadd "+
                          " where businessunit2team_gid='" + dt["businessunit2team_gid"].ToString() + "'"+
                          " and candidate_status='Invoice Raised-Joined'";

                    lsteamcandidate_joined = objdbconn.GetExecuteScalar(msSQL);
                    if ((lsemployee_photo.ToString()) == "")
                    {
                        lsphoto = "N";
                    }
                    else
                    {
                        lsphoto = lsemployee_photo;
                    }
                    get_businessunitteam.Add(new businessunitteamname_list
                    {     
                        employee_photo= lsphoto,
                        teamcandidate_sourced = lsteamcandidate_sourced,
                        teamcandidate_joined = lsteamcandidate_joined,
                        businessunitteam_name = dt["businessunitteam_name"].ToString(),
                        teamleader_name= dt["user_name"].ToString(),
                       businessunit2team_gid=dt["businessunit2team_gid"].ToString()
                });
                    
                }
                values.businessunitteamname_list = get_businessunitteam;
                msSQL = " select businessunit_name from rms_mst_tbusinessunit where businessunit_gid ='" + businessunit_gid + "'";
                values.businessunit_name = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select count(a.jobposition_gid) from rms_trn_tjobposition a, rms_trn_tjobposition2unit b where job_status='closed' and" +
                         " a.jobposition_gid=b.jobposition_gid and  businessunit_gid='" + businessunit_gid + "' and "+
                         " (a.transfer_from is null or a.transfer_from='"+businessunit_gid+"')";
                lsclosedjob_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select count(a.jobposition_gid) from rms_trn_tjobposition a, rms_trn_tjobposition2unit b where job_status='opening' and " +
                    " a.jobposition_gid=b.jobposition_gid and businessunit_gid='" + businessunit_gid + "' and "+
                    " (transfer_from='" + businessunit_gid + "'  or transfer_from is null)";
                lsopeningjob_count = objdbconn.GetExecuteScalar(msSQL);

                msSQL = "select count(a.jobposition_gid) from rms_trn_tjobposition  a, rms_trn_tjobposition2unit b  where jobcodefreeze_flag='Y' and "+
                     " a.jobposition_gid=b.jobposition_gid and businessunit_gid='" + businessunit_gid + "'";
                lsjobfreeze_count = objdbconn.GetExecuteScalar(msSQL);

                values.openingjob_count = lsopeningjob_count;
                values.closedjob_count = lsclosedjob_count;
                values.jobfreeze_count = lsjobfreeze_count;
            }
            dt_datatable.Dispose();
           
            return true;
        }

    }
}