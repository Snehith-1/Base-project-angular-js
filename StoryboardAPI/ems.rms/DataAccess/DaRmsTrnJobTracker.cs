using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using ems.utilities.Models;
using ems.utilities.Functions;
using System.Data;
using System.Data.Odbc;
using ems.rms.Models;

namespace ems.rms.DataAccess
{
    public class DaRmsTrnJobTracker
    {

        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable dt_datatable, dt_datatable2;
        string msSQL;
        OdbcDataReader objODBCDataReader;
        int mnResult;
        string lscandidate_fetched, lsposition_closed, lsposition_pending;

        public bool DaGetJobTrackerSummary(string employee_gid, JobTrackerList values)
        {
            msSQL = " select distinct a.jobposition_gid,a.job_status,i.businessunit_gid,concat(a.job_code,' / ',a.job_title) as job_code,a.company_name,a.noof_position,i.businessunit2team_gid,transfer_flag," +
                " (select count(x.recruitmentadd_gid) as candidate_fetched from rms_trn_trecruitmentadd x where a.jobposition_gid=x.jobposition_gid and x.candidate_status in " +
                " ('Process','Not process','On Hold','Not Interested','Others','Call Back','Interested','Selected','Rejected','Awaiting Feedback','Rescheduled','No Show'," +
                " 'Invoice Raised-Joined','Invoice Raised','Dropped','Scheduled','Walkin','Confirmed on Joining','Join Yet to Raise')) as candidate_fetched," +
                " (noof_position-(select count(x.recruitmentadd_gid) as position_closed from" +
                " rms_trn_trecruitmentadd x where x.candidate_status='Invoice Raised-Joined' and a.jobposition_gid=x.jobposition_gid)) as position_pending," +
                " (select count(x.recruitmentadd_gid) as position_closed from rms_trn_trecruitmentadd x where x.candidate_status='Invoice Raised-Joined' and" +
                " a.jobposition_gid=x.jobposition_gid) as position_closed,date_format(a.created_date,'%d-%m-%Y') as created_date,a.spoc_name,jobcodefreeze_flag,h.businessunit_name," +
                " case when (transfer_flag = 'Y' and  i.businessunit_gid in ( select businessunit_gid from rms_trn_tbusinessunit2manager where employee_gid='" + employee_gid + "')) then ('M') " +
                " when (transfer_flag = 'Y' and i.businessunit_gid in (select businessunit_gid from rms_mst_tbusinessunit2team where employee_gid = '" + employee_gid + "')) then('T') when transfer_flag = 'Y' then('N') " +
                " when (transfer_flag = 'Y' and i.businessunit_gid in (select businessunit_gid from rms_trn_tbusinessunit2spoc where employee_gid = '" + employee_gid + "')) then('S') when transfer_flag = 'Y' then('N') else ('C') end color_flag, " +
                " a.transfer_from from rms_trn_tjobposition a" +
                " left join rms_trn_tjobposition2team i on i.jobposition_gid=a.jobposition_gid" +
                " left join hrm_mst_temployee d on i.employee_gid=d.employee_gid" +
                " left join adm_mst_tuser e on d.user_gid=e.user_gid" +
                " left join rms_trn_tbusinessunit2manager g on g.businessunit_gid=i.businessunit_gid" +
                " left join rms_mst_tbusinessunit h on i.businessunit_gid=h.businessunit_gid where " +
                " g.employee_gid='" + employee_gid + "' or i.employee_gid='" + employee_gid + "' group by  a.jobposition_gid order by a.jobposition_gid desc ";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getjobtrackersummary = new List<jobTrackersummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {

                    getjobtrackersummary.Add(new jobTrackersummary
                    {
                        jobposition_gid = dt["jobposition_gid"].ToString(),
                        job_status = dt["job_status"].ToString(),
                        businessunit_gid = dt["businessunit_gid"].ToString(),
                        company_name = dt["company_name"].ToString(),
                        job_code = dt["job_code"].ToString(),
                        spoc_name = dt["spoc_name"].ToString(),
                        noof_position = dt["noof_position"].ToString(),
                        businessunit2team_gid = dt["businessunit2team_gid"].ToString(),
                        transfer_flag = dt["transfer_flag"].ToString(),
                        created_date = dt["created_date"].ToString(),
                        position_closed = dt["position_closed"].ToString(),
                        transfer_from = dt["transfer_from"].ToString(),
                        businessunit_name = dt["businessunit_name"].ToString(),
                        jobcodefreeze_flag = dt["jobcodefreeze_flag"].ToString(),
                        candidate_fetched = dt["candidate_fetched"].ToString(),
                        position_pending = dt["position_pending"].ToString(),
                        color_flag = dt["color_flag"].ToString(),
                         
                    });


                }
                values.jobTrackersummary = getjobtrackersummary;

                values.status = true;
            }
            dt_datatable.Dispose();
            values.status = false;
            values.message = "No Records Found";
            return true;
        }

        public bool DaGetJobPositionDetails(jobPositionDetails values, string jobposition_gid)
        {

            msSQL = " select a.job_code,a.job_title,a.company_name,a.noof_position,count(recruitmentadd_gid) as Offer Accepted_candidate from rms_trn_tjobposition a" +
                    " left join rms_trn_trecruitmentadd b on a.jobposition_gid=b.jobposition_gid where a.jobposition_gid='" + jobposition_gid + "'" +
                    " group by  b.jobposition_gid ";
            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                values.jobcode = objODBCDataReader["job_code"].ToString();
                values.job_type = objODBCDataReader["job_title"].ToString();
                values.company_name = objODBCDataReader["company_name"].ToString();
                values.noof_position = objODBCDataReader["noof_position"].ToString();
                values.Accepted_candidate = objODBCDataReader["Offer Accepted_candidate"].ToString();

            }
            return true;
        }

        public bool DaGetJobCodeViewDetails(jobCodeViewDetails values, string jobposition_gid)
        {

            msSQL = "select job_code,job_title,spoc_name,noof_position,concat(experience_min ,' To ',experience_max) as experience,concat(ctcbudget_min,' To ',ctcbudget_max) as ctc_budget," +
            " education_qualification,job_description,mandatroy_skills,notice_period,domain,job_location  from rms_trn_tjobposition  " +
            " where jobposition_gid='" + jobposition_gid + "'";

            objODBCDataReader = objdbconn.GetDataReader(msSQL);
            if (objODBCDataReader.HasRows)
            {
                values.jobcode = objODBCDataReader["job_code"].ToString();
                values.job_type = objODBCDataReader["job_title"].ToString();
                values.experience = objODBCDataReader["experience"].ToString();
                values.noof_position = objODBCDataReader["noof_position"].ToString();
                values.ctc_budget = objODBCDataReader["ctc_budget"].ToString();
                values.education_qualification = objODBCDataReader["education_qualification"].ToString();
                values.job_description = objODBCDataReader["job_description"].ToString();
                values.mandatroy_skills = objODBCDataReader["mandatroy_skills"].ToString();
                values.notice_period = objODBCDataReader["notice_period"].ToString();
                values.domain = objODBCDataReader["domain"].ToString();
                values.job_location = objODBCDataReader["job_location"].ToString();

            }
            return true;
        }

        public void DaGetJobPositionUpdate(string employee_gid, jobPositionDetails values)
        {

            msSQL = " update rms_trn_tjobposition set " +
               " noof_position='" + values.noof_position + "'" +
               " where jobposition_gid = '" + values.jobposition_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                values.message = "Job Code Position Updated Successfully";
                values.status = true;

            }
            else
            {
                values.message = " Error Occured While Updating Job Code Position";
                values.status = false;

            }
        }

        public void DaGetJobCodeClose(string employee_gid, jobPositionDetails values)
        {

            msSQL = " update rms_trn_tjobposition set " +
                    " job_status='closed'  where jobposition_gid='" + values.jobposition_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult == 1)
            {
                msSQL = " update rms_trn_tjobposition set " +
                   " updated_by='" + employee_gid + "'," +
                   " updated_date ='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " job_status='closed'  where jobposition_gid='" + values.jobposition_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    values.message = "Job Code Closed successfully ";
                    values.status = true;

                }
                else
                {
                    values.message = " Error Occured While Closing Job Code ";
                    values.status = false;

                }
            }
        }

        public bool DaGetRecruiterSummary(string employee_gid, string jobposition_gid, recruiterList values)
        {
            msSQL = " select a.jobposition_gid,a.recruiter_gid,concat(d.user_code,' / ', d.user_firstname, ' ', d.user_lastname) as recruiter_name,e.businessunitteam_name," +
                       " f.businessunit_name,b.noof_position,a.freeze_flag from rms_trn_tjobposition2recruiter a" +
                       " left join rms_trn_tjobposition b on a.jobposition_gid=b.jobposition_gid" +
                       " left join hrm_mst_temployee c on a.recruiter_gid = c.employee_gid" +
                       " left join adm_mst_tuser d on c.user_gid=d.user_gid" +
                       " left join rms_mst_tbusinessunit2team e on a.businessunit2team_gid=e.businessunit2team_gid" +
                       " left join rms_mst_tbusinessunit f on f.businessunit_gid=e.businessunit_gid" +
                       " left join rms_trn_tbusinessunit2manager g on g.businessunit_gid=e.businessunit_gid where (e.employee_gid='" + employee_gid + "'" +
                       " or g.employee_gid='" + employee_gid + "') and  a.jobposition_gid='" + jobposition_gid + "' " +
                       " group by a.recruiter_gid order by a.businessunit2team_gid  asc";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getrecruiterSummary = new List<recruiterSummary>();
            if (dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    msSQL = " select (select count(x.recruitmentadd_gid) as candidate_fetched from rms_trn_trecruitmentadd x where x.candidate_status in" +
                     "   ('Process','Not process','On Hold','Not Interested','Others','Call Back','Interested','Selected','Rejected'," +
                     " 'Awaiting Feedback','Scheduled','Rescheduled','No Show','Invoice Raised-Joined','Invoice Raised','Dropped','Walkin','Confirmed on Joining','Join Yet to Raise') and " +
                     " x.recruiter_gid='" + dt["recruiter_gid"].ToString() + "'and a.jobposition_gid='" + dt["jobposition_gid"].ToString() + "' and a.jobposition_gid=x.jobposition_gid)" +
                     " as candidate_fetched,(noof_position-(select count(x.recruitmentadd_gid) from rms_trn_trecruitmentadd x where x.candidate_status ='Invoice Raised-Joined' " +
                     " and x.recruiter_gid='" + dt["recruiter_gid"].ToString() + "' and a.jobposition_gid=x.jobposition_gid) and a.jobposition_gid='" + dt["jobposition_gid"].ToString() + "' )" +
                     " as position_pending,(select count(x.recruitmentadd_gid) from rms_trn_trecruitmentadd x  where x.candidate_status ='Invoice Raised-Joined'  and " +
                     " x.recruiter_gid='" + dt["recruiter_gid"].ToString() + "' and a.jobposition_gid='" + dt["jobposition_gid"].ToString() + "'and " +
                     " a.jobposition_gid=x.jobposition_gid) as position_closed from rms_trn_trecruitmentadd  a" +
                     " left join rms_trn_tjobposition h on h.jobposition_gid = a.jobposition_gid where a.recruiter_gid='" + dt["recruiter_gid"].ToString() + "'" +
                     " and h.jobposition_gid='" + dt["jobposition_gid"].ToString() + "'group by h.jobposition_gid,recruiter_gid ";
                    objODBCDataReader = objdbconn.GetDataReader(msSQL);
                    if (objODBCDataReader.HasRows)
                    {
                        lscandidate_fetched = objODBCDataReader["candidate_fetched"].ToString();
                        lsposition_closed = objODBCDataReader["position_closed"].ToString();
                        lsposition_pending = objODBCDataReader["position_pending"].ToString();
                    }
                    else
                    {
                        lscandidate_fetched = "0";
                        lsposition_closed = "0";
                        lsposition_pending = "0";
                    }
                    getrecruiterSummary.Add(new recruiterSummary
                    {
                        jobposition_gid = dt["jobposition_gid"].ToString(),
                        recruiter_gid = dt["recruiter_gid"].ToString(),
                        businessunitteam_name = dt["businessunitteam_name"].ToString(),
                        freeze_flag = dt["freeze_flag"].ToString(),
                        businessunit_name = dt["businessunit_name"].ToString(),
                        recruiter_name = dt["recruiter_name"].ToString(),
                        noof_position = dt["noof_position"].ToString(),
                        candidate_fetched = lscandidate_fetched,
                        position_closed = lsposition_closed,
                        position_pending = lsposition_pending,

                    });
                }
                
                values.recruiterSummary = getrecruiterSummary;
                dt_datatable.Dispose();
                values.status = true;
            }

            else
            {
                values.status = false;
                values.message = "No Records Found";
            }
                
                return true;
            }



        }
    }
